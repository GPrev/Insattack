using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MapDataModel;

namespace INSAttack
{
    public class NewGameBuilder : GameBuilder
    {
        private List<Department> m_departments;
        public List<Department> Departments
        {
            get { return m_departments; }
            set { m_departments = value; }
        }

        private int m_nbPlayers;
        private BoardStrategy m_boardCreator;
        public BoardStrategy BoardCreator
        {
            get { return m_boardCreator; }
            set { m_boardCreator = value; }
        }


        public NewGameBuilder()
        {
            m_departments = new List<Department>();
            m_boardCreator = new NormalBoardStrategy(m_departments);
        }

        public new Game make()
        {
            m_nbPlayers = m_departments.Count;
            m_boardCreator.Departments = m_departments;
            Game game = new Game(m_nbPlayers);

            //Players' creation
            for (int i = 0; i < m_nbPlayers; i++)
            {
                game.Players.Add(m_departments[i].Player);

            }

            //Creation of the board
            Board board = m_boardCreator.make();
            game.Board = board;

            //Initialisation of units
            foreach (var l in game.Board.UnitTable)
            {
                foreach (var u in l.Value)
                {
                    u.init(2, 4, 4, 3);
                }
            }

            //Initialisation of the first turn
            game.ActivePlayer = game.Players.First();
            
            return game;
        }
    }
}
