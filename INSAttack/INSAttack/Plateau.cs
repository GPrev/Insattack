using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace INSAttack
{
    public class Board
    {
        private INSAttack.Tile[][] m_tileTable;

        public INSAttack.Tile[][] TileTable
        {
            get { return m_tileTable; }
            set { m_tileTable = value; }
        }
        private System.Collections.Generic.List<INSAttack.Unit> m_unitTable;

        public System.Collections.Generic.List<INSAttack.Unit> UnitTable
        {
            get { return m_unitTable; }
            set { m_unitTable = value; }
        }
    }
}
