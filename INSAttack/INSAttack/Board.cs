using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MapDataModel;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Soap;
using System.Runtime.Serialization.Formatters.Binary;

namespace INSAttack
{
    [Serializable()]
    public class Board
    {
        private MapDataModel.MapData m_map;

        public MapDataModel.MapData Map
        {
            get { return m_map; }
            set { m_map = value; }
        }

        public Board()
        {
            m_map = new MapData();
            m_unitTable = new Dictionary<Coord,List<Unit>>();
        }

        public Board(MapData map)
        {
            m_map = map;
            m_unitTable = new Dictionary<Coord, List<Unit>>();
        }

        private int m_nbTurns;
        public int NbTurns
        {
            get { return m_nbTurns; }
            set { m_nbTurns = value; }
        }

        private Dictionary<Coord, List<INSAttack.Unit>> m_unitTable;

        public Dictionary<Coord, List<INSAttack.Unit>> UnitTable
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

        public override bool Equals(object obj)
        {
            return obj is Board && Equals((Board)obj);
        }

        public bool Equals(Board board)
        {
            if (!m_map.Equals(board.m_map)) return false;
            if (!m_nbTurns.Equals(board.m_nbTurns)) return false;
            if (m_unitTable.Count != board.m_unitTable.Count) return false;
            foreach (var ul in m_unitTable)
            {
                foreach (var u in ul.Value)
                {
                    if (! board.m_unitTable[ul.Key].Contains(u)) return false;
                }
                if (ul.Value.Count != board.m_unitTable[ul.Key].Count) return false;
            }
            return true;
        }


    }
}
