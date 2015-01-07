using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Soap;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MapDataModel;

namespace INSAttack
{
    [Serializable()]
    public class Game
    {
        private Player m_activePlayer;

        public Player ActivePlayer
        {
            get { return m_activePlayer; }
            set { m_activePlayer = value; }
        }

        private int m_placeActivePlayer;

        private int m_nbPlayers;

        public int NbPlayer
        {
            get { return m_nbPlayers; }
        }


        private Board m_board;

        public Board Board
        {
            get { return m_board; }
            set { m_board = value; }
        }
        
        private List<Player> m_players;

        public List<Player> Players
        {
            get { return m_players; }
            set { m_players = value; }
        }


        
        public Game(int nbPlayers)
        {
            m_nbPlayers = nbPlayers;
            m_players = new List<Player>();
            m_placeActivePlayer = 0;
        }


        public bool move(Unit u, Coord dest)
        {

            //check the existence of the unit
            Coord coord = m_board.find(u);
            if (!coord.exists()) return false;
            if (coord.Equals(dest)) return true;

            //checks if the unit belongs to current player
            if (!u.Player.Equals(m_activePlayer))
                return false;

            //Look for the presence of unit on the destination
            List<Unit> UnitsAtDest;
            try
            {
                UnitsAtDest = m_board.UnitTable[dest];
            }
            catch (KeyNotFoundException)
            {
                UnitsAtDest = null;
            }

            //check if the unit can teleport onto the case or not
            if(teleport(u, coord, dest))
            {
                if (UnitsAtDest == null || !UnitsAtDest.Any() || u.isAlly(UnitsAtDest.First()))
                {
                    return executeMove(u, dest, 0);
                }
            }

            //check the validity of the move
            int costDisplacement = m_board.Map.TileTable[coord].getcost(u.Dept);
            if (!Coord.areAdjacent(coord, dest)) return false;
            if (costDisplacement > u.Movement) return false;

            
            //Move the unit if there is no unit on its destination or if the tile is possess by unit's ally
            if (UnitsAtDest == null || !UnitsAtDest.Any() ||u.isAlly(UnitsAtDest.First()))
            {
                return executeMove(u, dest, costDisplacement);
            }

            //Resolve the battle if there is enemy's unit
            Unit target = choseTarget(UnitsAtDest);
            if (u.tryAndUseMovement(costDisplacement))
            {
                if (confront(u, target))
                {
                    if (UnitsAtDest.Count == 0)
                    {
                        return m_board.moveUnit(u, dest);
                    }
                    return false;
                    
                }
                else return false;
            }
            return false;

        }

        public bool passTurn(Unit u)
        {
            //checks the existence of the unit
            Coord coord = m_board.find(u);
            if (!coord.exists())
                return false;

            //checks if the unit belongs to current player
            if (!u.Player.Equals(m_activePlayer))
                return false;

            u.useAllMovement();

            List<Unit> unitList = getPlayerUnits(u.Player);
            bool finishedTurn = true;
            foreach (var unit in unitList)
            {
                if(unit.Movement !=0)
                    finishedTurn = false;
            }
            if (finishedTurn) return endOfTurn();
            return true;
        }

        
        //return true if the gamed is finished
        public bool endOfTurn()
        {
            //reset the pm of all the units
            foreach (var l in m_board.UnitTable)
            {
                foreach (var u in l.Value)
                {
                    u.resetMovement();
                }
            }
            //Apply the effects of special cases (no one for the moment)

            //Check if the game is finished
            if (m_nbPlayers <= 1) return true;
            if (m_placeActivePlayer == (NbPlayer-1)) m_board.NbTurns--;
            if (m_board.NbTurns == 0) return true;

            //change the active player
            m_placeActivePlayer = (m_placeActivePlayer + 1) % m_nbPlayers;
            m_activePlayer = m_players[m_placeActivePlayer];
            return false;
        }

        //Delete one of the player, for the momet we considere that there is only 2 players
        private void removePlayer(Player player)
        {
            m_players.Remove(player);
            m_nbPlayers--;
        }

        private bool executeMove(Unit unit, Coord dest, int costDisplacement)
        {
            if (unit.tryAndUseMovement(costDisplacement))
            {
                return m_board.moveUnit(unit, dest);
            }
            else return false;
        }

        private bool teleport(Unit unit, Coord startingPoint, Coord dest)
        {
            return ((unit.Dept == Dept.INFO) &&
                   (m_board.Map.TileTable[startingPoint] == TileFactory.Instance.InfoTile) &&
                   (m_board.Map.TileTable[dest] == TileFactory.Instance.InfoTile));
        }

        //Chose one of the unit with the more HP in the list
        private Unit choseTarget(List<Unit> targets)
        {
            Unit target = targets.First();
            foreach (var u in targets)
            {
                if(u.Defense > target.Defense) target = u;
            }
            return target;
        }

        //return true if the target is dead
        private bool confront(Unit u, Unit target)
        {
            //we calculate the number of turns of the battle
            int maxHP = Math.Max(u.Life, target.Life);
            Random rand = new Random();
            int nbTurns = rand.Next() % (maxHP + 2);

            float attack;
            float defense;
            int chancesOfLose; //Chance of the attacker to lose the turn
            for (int i = 0; i < nbTurns; i++)
            {
                //resolve the battle
                attack = (float)u.Attack * u.getHealthRatio();
                defense = (float)target.Defense * target.getHealthRatio();
                chancesOfLose = 50 + (int)((attack / defense) * 50);
                if (rand.Next() % 100 <= chancesOfLose)
                {
                    u.takeHit(1);
                }
                else
                {
                    target.takeHit(1);
                }

                //check if the one of the units involved is dead 
                if (u.isDead())
                {
                    m_board.removeUnit(u);
                    killer(target);
                    if (countUnits(u.Player) == 0) removePlayer(u.Player);
                }
                if (target.isDead())
                {
                    m_board.removeUnit(target);
                    killer(u);
                    if (countUnits(target.Player) == 0) removePlayer(target.Player);
                    return true;
                }
            }
           return false;
        }

        private void killer(Unit unit)
        {
            if (unit.Dept == Dept.EII) unit.Points--;
        }

        public int points(Player player)
        {
            int res = 0;
            bool isHere = false;
            foreach (var ul in m_board.UnitTable)
            {
                if (ul.Value.Count > 0 && ul.Value.First().Player == player)
                {
                    isHere = false;
                        foreach (var u in ul.Value)
                    {
                        if (u.Player.Equals(player))
                        {
                            res += u.Points;
                            isHere = true;
                        }
                    }
                    if (isHere) res++;
                }
            }
            return res;
        }

        public int countUnits(Player player)
        {
            int res = 0;
            foreach (var ul in m_board.UnitTable)
            {
                foreach (var u in ul.Value)
                {
                    if (u.Player.Equals(player))
                    {
                        res += 1;
                    }
                }
            }
            return res;
        }

        public Player winner()
        {
            if (m_nbPlayers == 1) return m_players.First();
            if (m_nbPlayers > 1)
            {
                Player player;
                int maxPoints;
                player = m_players.First();
                maxPoints = points(player);
                foreach (Player p in m_players)
                {
                    if (points(p) > maxPoints)
                    {
                        maxPoints = points(p);
                        player = p;
                    }
                }
                return player;
            }
            throw new Exception();
        }

        public void save(String name = "gameSave.xml")
        {
            

            //Opens a file and serializes the object into it in binary format.
            Stream stream = File.Open(name, FileMode.Create);
            //SoapFormatter formatter = new SoapFormatter();
            BinaryFormatter formatter = new BinaryFormatter();

            formatter.Serialize(stream, this);
            formatter.Serialize(stream, Unit.Count);
            formatter.Serialize(stream, Player.Count);
            stream.Close();
        }

        
        public override bool Equals(object obj)
        {
            return obj is Game && Equals((Game)obj);
        }

        public bool Equals(Game game)
        {
            if (!m_activePlayer.Equals(game.m_activePlayer)) return false;
            if (!m_placeActivePlayer.Equals(game.m_placeActivePlayer)) return false;
            if (!m_nbPlayers.Equals(game.m_nbPlayers)) return false;
            if (!m_board.Equals(game.m_board)) return false;
            if (!m_players.SequenceEqual(game.m_players)) return false;

            return true;
        }

        public List<Unit> getPlayerUnits(Player player)
        {
            List<Unit> playerUnits = new List<Unit>();
            foreach (var unitList in m_board.UnitTable)
            {
                foreach (var unit in unitList.Value)
                {
                    if (unit.Player.Equals(player))
                    {
                        playerUnits.Add(unit);
                    }
                }
            }
            return playerUnits;
        }


    }
}
