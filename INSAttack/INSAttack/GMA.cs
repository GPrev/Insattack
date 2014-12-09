using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MapDataModel;

namespace INSAttack
{
    public class GMA : Department
    {
        /// <summary>
        /// </summary>
        

        public GMA(Player player) : base(player)
        {
        }

       
        private static Department m_instance;

        public static Department Instance
        {
            get { return GMA.m_instance; }
            set { GMA.m_instance = value; }
        }

        public override Unit make()
        {
            return new Unit(m_player, (Dept)this);
        }

        // Overload the conversion from Department to Dept:
        public static implicit operator Dept(GMA x)
        {
            return Dept.GMA;
        }
    }
}
