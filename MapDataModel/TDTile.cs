﻿using System;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Soap;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MapDataModel
{
    [Serializable()]
    public class TDTile : Tile
    {
        public TDTile()
        {
            m_costs = new Dictionary<Dept, float>();
            m_costs.Add(Dept.INFO, 1);
            m_costs.Add(Dept.EII, 0.5F);
            m_costs.Add(Dept.SRC, 1);
            m_costs.Add(Dept.SGM, 1);
            m_costs.Add(Dept.GMA, 1);
            m_costs.Add(Dept.GC, 2);
        }

        public override bool Equals(object obj)
        {
            return obj is TDTile;
        }

        public override int GetHashCode()
        {
            return (int) TileHash.TDTile;
        }

        public override string ToString()
        {
            return "Salle de TD";
        }
    }
}
