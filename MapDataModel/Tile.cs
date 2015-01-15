using System;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Soap;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace MapDataModel
{
    public enum Dept { SGM, EII, GC, INFO, SRC, GMA, NB_DEPT }
    [Serializable()]
    public abstract class Tile
    {
        protected Dictionary<Dept, float> m_costs;
        protected enum TileHash { Tile = -1, OutdoorTile, AmphiTile, INFOTile, TDTile }

        public float getcost(Dept department)
        {
            return m_costs[department];
        }

        public override bool Equals(object obj)
        {
            return obj is Tile;
        }

        public override int GetHashCode()
        {
            return (int)TileHash.Tile;
        }

        public override string ToString()
        {
            return "Case abstraite";
        }
    }
}
