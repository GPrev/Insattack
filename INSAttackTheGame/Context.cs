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

        public static void changeGame(GameBuilder builder)
        {
            m_game = builder.make();
        }

        public static bool isGameValid()
        {
            return Game != null;
        }
    }
}
