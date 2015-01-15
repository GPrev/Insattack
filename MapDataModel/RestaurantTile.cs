using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapDataModel
{
    class RestaurantTile : Tile
    {public RestaurantTile()
        {
            m_costs = new Dictionary<Dept, float>();
            m_costs.Add(Dept.INFO, 2);
            m_costs.Add(Dept.EII, 2);
            m_costs.Add(Dept.SRC, 2);
            m_costs.Add(Dept.SGM, 2);
            m_costs.Add(Dept.GMA, 2);
            m_costs.Add(Dept.GC, 2);
        }

        public override bool Equals(object obj)
        {
            return obj is RestaurantTile;
        }

        public override int GetHashCode()
        {
            return (int)TileHash.RestaurantTile;
        }

        public override string ToString()
        {
            return "Self de l'INSA, attention si vous êtes ici à la fin du tour ce sera à vos risques et périls.";
        }
    }
}
