using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wrapper;
using MapDataModel;

namespace INSAttack
{
    public abstract class BoardStrategy : INSAttack.Factory<Board>
    {
        protected int m_boardSize;

        public int BoardSize
        {
            get { return m_boardSize; }
            set { m_boardSize = value; }
        }

        protected int m_nbSimpleTiles; //Number of simpels tiles : AmphiTiles, TDTiles, INFOTiles

        public int NbSimpleTiles
        {
            get { return m_nbSimpleTiles; }
            //set { m_nbSimpleTiles = value; }
        }

        protected int nb_restaurantsTile; //Number of RestaurantTile

        public int NBRestaurantTiles
        {
            get { return nb_restaurantsTile; }
            //set { m_nbSimpleTiles = value; }
        }

        private int m_nbPlayers;

        public int NbPlayers
        {
            get { return m_nbPlayers; }
            set { m_nbPlayers = value; }
        }
        protected int m_nbTurns;

        public int NbTurns
        {
            get { return m_nbTurns; }
            //set { m_nbTurns = value; }
        }
        protected int m_nbUnits;

        public int NbUnits
        {
            get { return m_nbUnits; }
            set { m_nbUnits = value; }
        }

        protected List<Department> m_departments;

        public List<Department> Departments
        {
            get { return m_departments; }
            set { m_departments = value; }
        }
        
        public BoardStrategy(List<Department> departments)
        {
            m_departments = departments;
            m_nbPlayers = departments.Count;
        }

        public Board make()
        {
            WrapperMapGenerator mapGenerator = new WrapperMapGenerator();
            MapData map = mapGenerator.makeMap(m_boardSize, m_nbPlayers, m_nbSimpleTiles, m_nbSimpleTiles, m_nbSimpleTiles, nb_restaurantsTile);
            Board board = new Board(map);

            board.NbTurns = m_nbTurns;

            //Generation of the units
            for (int i = 0; i < board.Map.StartingPos.Count(); i++)
            {
                for (int j = 0; j < m_nbUnits; j++)
                {
                    board.addUnit(board.Map.StartingPos[i], m_departments[i].make());
                }
            }

            return board;
        }

       
    }
}
