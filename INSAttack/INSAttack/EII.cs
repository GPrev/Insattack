using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MapDataModel;

namespace INSAttack
{
    public class EII : Department
    {
        /// <summary>
        /// </summary>
        
        public EII(Player player) : base(player)
        {
        }

        private static Department m_instance;

        public static Department Instance
        {
            get { return EII.m_instance; }
            set { EII.m_instance = value; }
        }

        public override Unit make()
        {
            return new Unit(m_player, (Dept)this);
        }

        // Overload the conversion from Department to Dept:
        public static implicit operator Dept(EII x)
        {
            return Dept.EII;
        }
    }
}
