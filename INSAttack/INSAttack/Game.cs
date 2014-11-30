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

        
        public Game()
        {
            
            //throw new System.NotImplementedException();

        }


        public bool move(Unit u, Coord dest)
        {
            throw new NotImplementedException();

            //vérifier l'existence de l'unité
            Coord coord = m_board.find(u);
            if (coord == Coord.nowhere) return false;

            //vérifier les pm / validité du mouvement
            int costDiplacement = m_board.Map.TileTable[coord].getcost(u.Dept);
            if (!Coord.areAdjacent(coord, dest)) return false;
            if (costDiplacement > u.Movement) return false;

            //tester la présence d'unité ennemie sur la case de destination
            List<Unit> UnitsAtDest = m_board.UnitTable[dest];

            //Move the unit if there is no unit on its destination or if the tile is possess by uni's ally
            if (UnitsAtDest == null || u.isAlly(UnitsAtDest.First()))
            {
                return m_board.moveUnit(u, dest);
            }
            //Resolve the confrontation
            
            //rend vrai si le mouvement est effectué
            return false;
        }

        public void endOfTurn()
        {
            throw new System.NotImplementedException();
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

            //change the active player
            m_activePlayer = m_players[m_activePlayer.Id];
        }
    }
}
