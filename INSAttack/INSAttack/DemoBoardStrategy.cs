using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace INSAttack
{
    public class DemoBoardStrategy : BoardStrategy
    {
        public DemoBoardStrategy(List<Department> departments) : base(departments)
        {
            m_nbUnits = 4;
            m_nbTurns = 5;
            m_boardSize = 6;
            m_nbSimpleTiles = 6;
            
        }
    }
}
