using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapDataModel
{
    public class MapData
    {
        private Dictionary<Coord, Tile> m_unitTable;

        public Dictionary<Coord, Tile> UnitTable
        {
            get { return m_unitTable; }
            set { m_unitTable = value; }
        }
    }
}
