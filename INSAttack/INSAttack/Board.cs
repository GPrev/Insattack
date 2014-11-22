using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace INSAttack
{
    public class Board
    {
        private Dictionary<INSAttack.Coord, INSAttack.Tile> m_tileTable;

        public Board()
        {
            m_tileTable = new Dictionary<Coord, Tile>();
            m_unitTable = new Dictionary<Coord,List<Unit>>();
        }

        public Dictionary<Coord, Tile> TileTable
        {
            get { return m_tileTable; }
            set { m_tileTable = value; }
        }
        private Dictionary<INSAttack.Coord, List<INSAttack.Unit>> m_unitTable;

        public Dictionary<INSAttack.Coord, List<INSAttack.Unit>> UnitTable
        {
            get { return m_unitTable; }
            set { m_unitTable = value; }
        }

        public bool addUnit(Coord c, Unit u)
        {
            if (!UnitTable.ContainsKey(c))
                UnitTable.Add(c, new List<Unit>());
            UnitTable[c].Add(u);
            return true;
        }

        public bool removeUnit(Unit u)
        {
            Coord c = find(u);
            if (!c.exists()) //could not find unit to move
                return false;
            //else
            return removeUnit(u, c);
        }
        public bool removeUnit(Unit u, Coord c)
        {
            return UnitTable[c].Remove(u);
        }

        public bool moveUnit(Unit u, Coord dest)
        {
            if(removeUnit(u))
            {
                return addUnit(dest, u);
            }
            return false;
        }

        public Coord find(Unit u)
        {
            foreach(var l in UnitTable)
            {
                if (l.Value.Exists(x => x == u))
                    return l.Key;
            }
            return Coord.nowhere;
        }
    }
}
