using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using INSAttack;
using MapDataModel;

namespace INSAttackTheGame
{
    class Context
    {

        private static Game m_game;

        public static Game Game
        {
            get { return Context.m_game; }
        }
        public static Board Board
        {
            get { return m_game.Board; }
        }

        public static MapData Map
        {
            get { return Board.Map; }
        }

        private static Unit m_selectedUnit;

        public static Unit SelectedUnit
        {
            get { return Context.m_selectedUnit; }
            set { m_selectedUnit = value; }
        }

        private static Coord m_cursorPos;

        public static Coord CursorPos
        {
            get { return m_cursorPos; }
            set
            {
                if (Context.Map.isValid(value))
                    m_cursorPos = value;
                else
                    unselect();
            }
        }

        public static List<Unit> SelectedUnitsList
        {
            get
            {
                if (Context.Board.UnitTable.ContainsKey(Context.CursorPos))
                    return Context.Board.UnitTable[Context.CursorPos];
                //else
                return new List<Unit>();
            }
        }


        public static void changeGame(GameBuilder builder)
        {
            m_game = builder.make();
        }


        public static bool isGameValid()
        {
            return Game != null;
        }

        public static void unselect() //unselects selected tile if any
        {
            m_cursorPos = Coord.nowhere;
            m_selectedUnit = null;
        }

        public static bool isGameOver()
        {
            var winners = Game.winner();
            return winners.Count > 0;
        }

        //returns the message to display when the game is over
        public static string getWinMessage()
        {
            var winners = Game.winner();
            if(winners == null || winners.Count == 0)
                return null;
            //else
            if(winners.Count == 1)
                return winners[0].Name + " gagne.";
            //else
            string res = "Égalité entre " + winners[0].Name;
            winners.RemoveAt(0);
            foreach(Player p in winners)
            {
                res += " et " + p.Name;
            }
            return res+".";
        }
    }
}
