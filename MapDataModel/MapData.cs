using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapDataModel
{
    public class MapData
    {
        private Dictionary<Coord, Tile> m_tileTable;
        private int m_size;
        private List<Coord> m_startingPos;

        public List<Coord> StartingPos
        {
            get { return m_startingPos; }
            set { m_startingPos = value; }
        }

        public int Size
        {
            get { return m_size; }
            set { m_size = value; }
        }

        public Dictionary<Coord, Tile> TileTable
        {
            get { return m_tileTable; }
            set { m_tileTable = value; }
        }

        public MapData()
        {
            m_tileTable = new Dictionary<Coord, Tile>();
            m_startingPos = new List<Coord>();
        }
    }
}
