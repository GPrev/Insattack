using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;



namespace INSAttack
{
    public class NormalBoardStrategy : BoardStrategy
    {
        public NormalBoardStrategy(List<Department> departments)
            : base(departments)
        {
            m_nbUnits = 8;
            m_nbTurns = 30;
            m_boardSize = 14;
            m_nbSimpleTiles = 30;
        }

    }
}