using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace INSAttack
{
    public class SmallBoardStrategy : BoardStrategy
    {
        public SmallBoardStrategy(List<Department> departments) : base(departments)
        {
            m_nbUnits = 6;
            m_nbTurns = 20;
            m_boardSize = 10;
            m_nbSimpleTiles = 12;
            nb_restaurantsTile = 2;
        }

    }
}
