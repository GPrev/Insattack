using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MapDataModel
{
    public enum Dept { SGM, EII, GC, INFO, SRC, GMA, NB_DEPT }
    public abstract class Tile
    {
        protected Dictionary<Dept, int> m_costs;

        public int getcost(Dept department){
            return m_costs[department];
        }
    }
}
