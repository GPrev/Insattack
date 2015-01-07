using System;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Soap;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MapDataModel
{
    [Serializable()]
    public class INFOTile : Tile
    {
        public INFOTile()
        {
            m_costs = new Dictionary<Dept, int>();
            m_costs.Add(Dept.INFO, 1);
            m_costs.Add(Dept.EII, 1);
            m_costs.Add(Dept.SRC, 1);
            m_costs.Add(Dept.SGM, 1);
            m_costs.Add(Dept.GMA, 1);
            m_costs.Add(Dept.GC, 1);
        }

        public override bool Equals(object obj)
        {
            return obj is INFOTile;
        }

        public override int GetHashCode()
        {
            return (int) TileHash.INFOTile;
        }

        public override string ToString()
        {
            return "Salle info :\n"+"Les unités du département info peuvent se déplacer instantanément entre les salles info.";
        }
    }
}
