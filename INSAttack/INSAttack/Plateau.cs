using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace INSAttack
{
    public class Board
    {
        private Dictionary<INSAttack.Coord, INSAttack.Tile> m_tileTable;

        public Dictionary<Coord, Tile> TileTable
        {
            get { return m_tileTable; }
            set { m_tileTable = value; }
        }
        private Dictionary<INSAttack.Coord, INSAttack.Unit> m_unitTable;

        public Dictionary<INSAttack.Coord, INSAttack.Unit> UnitTable
        {
            get { return m_unitTable; }
            set { m_unitTable = value; }
        }
    }
}
