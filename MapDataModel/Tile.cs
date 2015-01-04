using System;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Soap;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Soap;
using System.Runtime.Serialization.Formatters.Binary;

namespace MapDataModel
{
    public enum Dept { SGM, EII, GC, INFO, SRC, GMA, NB_DEPT }
    [Serializable()]
    public abstract class Tile
    {
        protected Dictionary<Dept, int> m_costs;

        public int getcost(Dept department){
            return m_costs[department];
        }

        public override bool Equals(object obj)
        {
            return obj is Tile;
        }
    }
}
