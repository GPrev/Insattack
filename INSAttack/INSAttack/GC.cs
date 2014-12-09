using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MapDataModel;

namespace INSAttack
{
    public class GC : Department
    {
        /// <summary>
        /// </summary>
        
        public GC(Player player) : base(player)
        {
        }

        private static Department m_instance;

        public static Department Instance
        {
            get { return GC.m_instance; }
            set { GC.m_instance = value; }
        }

        public override Unit make()
        {
            return new Unit(m_player, (Dept)this);
        }

        // Overload the conversion from Department to Dept:
        public static implicit operator Dept(GC x)
        {
            return Dept.GC;
        }
    }
}
