﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace INSAttack
{
    public class TDTile : Tile
    {
        public TDTile()
        {
            m_costs = new Dictionary<Dept, int>();
            m_costs.Add(Dept.INFO, 1);
            m_costs.Add(Dept.EII, 1);
            m_costs.Add(Dept.SRC, 1);
            m_costs.Add(Dept.SGM, 1);
            m_costs.Add(Dept.GMA, 1);
            m_costs.Add(Dept.GC, 1);
        }
    }
}
