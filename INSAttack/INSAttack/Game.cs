using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Soap;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MapDataModel;
using Wrapper;

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


        public bool move(Unit unit, Coord dest)
        {

            //check that the game is not finished
            if (isGamefinished()) return false;

            //check the existence of the unit and the validity of the case
            Coord coord = m_board.find(unit);
            if (!coord.exists()) return false;
            if (!dest.exists()) return false;
            if (coord.Equals(dest)) return true;

            //checks if the unit belongs to current player
            if (!unit.Player.Equals(m_activePlayer))
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
            if(teleport(unit, coord, dest))
            {
                 if(UnitsAtDest == null || !UnitsAtDest.Any() || unit.isAlly(UnitsAtDest.First()))
                {
                    return executeMove(unit, dest, 0);
                }
            }
            //check the validity of the move
            float costDisplacement = m_board.Map.TileTable[coord].getcost(unit.Dept);
            if (!Coord.areAdjacent(coord, dest)) return false;
            if (costDisplacement > unit.Movement) return false;

            
            //Move the unit if there is no unit on its destination or if the tile is possess by unit's ally
            if (UnitsAtDest == null || !UnitsAtDest.Any() ||unit.isAlly(UnitsAtDest.First()))
            {
                return executeMove(unit, dest, costDisplacement);
            }

            //Resolve the battle if there is enemy's unit
            Unit target = choseTarget(UnitsAtDest);
            if (unit.tryAndUseMovement(costDisplacement))
            {
                if (confront(unit, target))
                {
                    if (UnitsAtDest.Count == 0)
                    {
                        return m_board.moveUnit(unit, dest);
                    }
                }
            }
            return false;

        }


        //Delete one of the player, for the momet we considere that there is only 2 players
        public void removePlayer(Player player)
        {
            m_players.Remove(player);
            m_nbPlayers--;
        }

        private bool executeMove(Unit unit, Coord dest, float costDisplacement)
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
                   (m_board.Map.TileTable[startingPoint].Equals(TileFactory.Instance.InfoTile)) &&
                   (m_board.Map.TileTable[dest].Equals(TileFactory.Instance.InfoTile)));
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
        private bool confront(Unit unit, Unit target)
        {

            //we calculate the number of turns of the battle
            int maxHP = Math.Max(unit.Life, target.Life);
            Random rand = new Random();
            int nbTurns = (rand.Next() % maxHP)+2;

            float attack;
            float defense;
            int chancesOfLose; //Chance of the attacker to lose the turn
            for (int i = 0; i < nbTurns; i++)
            {
                //resolve the battle
                attack = (float)unit.Attack * unit.getHealthRatio();
                defense = (float)target.Defense * target.getHealthRatio();
                chancesOfLose = getChancesOfLose(attack, defense);
                if ( (rand.Next() % 100) <= chancesOfLose)
                {
                    unit.takeHit(1);
                }
                else
                {
                    target.takeHit(1);
                }

                //check if the one of the units involved is dead
                if (unit.isDead())
                {
                    if (escape(unit))
                    {
                        unit.Life = 1;
                        return false;
                    }
                    m_board.removeUnit(unit);
                    killer(target);
                    if (countUnits(unit.Player) == 0) removePlayer(unit.Player);
                    return false;
                }
                if (target.isDead())
                {
                    if (escape(target))
                    {
                        unit.Life = 1;
                        return false;
                    }
                    m_board.removeUnit(target);
                    killer(unit);
                    if (countUnits(target.Player) == 0) removePlayer(target.Player);
                    return true;
                }
            }
           return false;
        }


        private int getChancesOfLose(float attack, float defense)
        {
            if (attack > defense) return (50 - (int) ((1 - (defense / attack)) * 50));
            if (attack < defense) return (50 + (int) ((1 - (attack / defense)) * 50));
            //if (attack == defense) return 50;
            return 50;
        }

        //Add points to the unit if it possess the abilities to gain point by killing others units
        private void killer(Unit unit)
        {
            if (canGainPointOnKill(unit))
            {
                unit.Points = unit.Points + 1;
            }
        }

        //Return true if the unit can gain point by killing another unit
        public bool canGainPointOnKill(Unit unit)
        {
            return unit.Dept == Dept.EII;
        }

        //return true if the unit do an escape (that mean it avoid the death
        private bool escape(Unit unit)
        {
            if (unit.Dept == Dept.SRC)
            {
                Random rand = new Random();
                int chance = rand.Next() % 100;
                if (chance > 50) return true;
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
                if (unit.Movement != 0)
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
            //Apply the effects of the restaurants
            foreach (var tile in m_board.Map.TileTable)
            {
                executeSpecialEffets(tile.Value, m_board.UnitTable[tile.Key]);
            }

            //Check if the game is finished
            //if (m_nbPlayers <= 1) return true;
            if (m_placeActivePlayer == (NbPlayer - 1)) m_board.NbTurns--;
            //if (m_board.NbTurns == 0) return true;
            if (isGamefinished()) return true;

            //change the active player
            m_placeActivePlayer = (m_placeActivePlayer + 1) % m_nbPlayers;
            m_activePlayer = m_players[m_placeActivePlayer];
            return false;
        }

        private void executeSpecialEffets(Tile tile, List<Unit> units)
        {
            if (units != null)
            {
                if (tile.Equals(TileFactory.Instance.RestaurantTile))
                {
                    
                }
            }
        } 


        public int getPoints(Player player)
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

        
        public List<Player> winner()
        {
            List<Player> winners = new List<Player>();

            if (!isGamefinished()) return winners;
            
            if (m_nbPlayers == 1) return m_players;
            if (m_nbPlayers > 1)
            {
                winners.Add(m_players.First());
                int maxPoints = -42;//getPoints(winners.First());
                int points;
                foreach (Player p in m_players)
                {
                    points = getPoints(p);
                    if (points == maxPoints)
                    {
                        winners.Add(p);
                    }

                    if (points > maxPoints)
                    {
                        maxPoints = points;
                        winners.Clear();
                        winners.Add(p);
                    }
                }
            }

            return winners;
        }

        public bool isGamefinished()
        {
            if (m_nbPlayers == 1) return true;
            if (m_board.NbTurns == 0) return true;
            return false;
        }



        public void save(String name = "gameSave.xml")
        {
            String _name = name;
            try 
            {
                if(!_name.EndsWith(".xml"))
                {
                    _name = String.Concat(name, ".xml");
                }
                //Opens a file and serializes the object into it in binary format.
                Stream stream = File.Open(_name, FileMode.Create);
                BinaryFormatter formatter = new BinaryFormatter();

                formatter.Serialize(stream, this);
                formatter.Serialize(stream, Unit.Count);
                formatter.Serialize(stream, Player.Count);
                stream.Close();
            }
            catch (SerializationException e)
            {
                Console.Error.WriteLine(e.Message);
            }
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

        public List<Coord> suggest(Unit u)
        {
            return suggest(Board.find(u));
        }
        public List<Coord> suggest(Coord pos)
        {
            //if there is no ally unit at the given position taht is able to move, no hint to be given
            bool hasMovement = false;
            if (Board.UnitTable.ContainsKey(pos) && Board.UnitTable[pos].Count > 0 && Board.UnitTable[pos][0].Player == m_activePlayer)
            {
                foreach (var unit in Board.UnitTable[pos])
                {
                    if (unit.Movement > 0)
                    {
                        hasMovement = true;
                        break;
                    }
                }
            }
            if(!hasMovement)
                return new List<Coord>();


            //Collects data on adjacent squares
            List<Coord> adjList = pos.adjacentCoords();
            List<Tuple<Tile, bool, int>> data = new List<Tuple<Tile,bool,int>>();
            List<Coord> toDeleteList = new List<Coord>();
            foreach(Coord c in adjList)
            {
                if (Board.Map.isValid(c))
                {
                    bool isAlly = Board.UnitTable.ContainsKey(c) && Board.UnitTable[c].Count > 0 && Board.UnitTable[c][0].Player == m_activePlayer;
                    int enemyHP = 0;
                    if (!isAlly && Board.UnitTable.ContainsKey(c) && Board.UnitTable[c].Count > 0)
                    {
                        foreach (Unit u in Board.UnitTable[c])
                            enemyHP += u.Life;
                    }
                    data.Add(new Tuple<Tile, bool, int>(Board.Map.TileTable[c], isAlly, enemyHP));
                }
                else
                    toDeleteList.Add(c);
            }
            //removes invalid coordinates
            foreach (Coord c in toDeleteList)
                adjList.Remove(c);

            WrapperHintGiver hintGiver = new WrapperHintGiver(); //calls the c++ hint giver

            //translates back to Coords
            List<int> indexList = hintGiver.giveHint(data);
            List<Coord> res = new List<Coord>();
            foreach(int i in indexList)
            {
                res.Add(adjList[i]);
            }
            return res;
        }
    }
}
