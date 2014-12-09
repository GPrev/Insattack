using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MapDataModel;

namespace INSAttack
{
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
            //set { m_nbPlayers = value; }
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

            //check the validity of the move
            int costDisplacement = m_board.Map.TileTable[coord].getcost(u.Dept);
            if (!Coord.areAdjacent(coord, dest)) return false;
            if (costDisplacement > u.Movement) return false;

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

            //Move the unit if there is no unit on its destination or if the tile is possess by unit's ally
            if (UnitsAtDest == null || !UnitsAtDest.Any() ||u.isAlly(UnitsAtDest.First()))
            {
                if (u.tryAndUseMovement(costDisplacement))
                {
                    return m_board.moveUnit(u, dest);
                }
                else return false;
            }

            //Resolve the battle if there is enemy's unit
            Unit target = choseTarget(UnitsAtDest);
            if (u.tryAndUseMovement(costDisplacement))
            {
                if (confront(u, target))
                {
                    return m_board.moveUnit(u, dest);
                }
                else return false;
            }
            return false;

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
            m_board.NbTurns--;
            if (m_board.NbTurns == 0) return true;

            //change the active player
            int nextPlayer = (m_placeActivePlayer + 1) % m_nbPlayers;
            m_activePlayer = m_players[nextPlayer];
            return false;
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
            }

            if (target.isDead()) return true;
           return false;
        }
    }
}
