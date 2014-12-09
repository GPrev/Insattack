using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MapDataModel;

namespace INSAttack
{
    public class INFO : Department
    {
        /// <summary>
        /// </summary>
        
        public INFO(Player player) : base(player)
        {
        }

        private static Department m_instance;

        public static Department Instance
        {
            get { return INFO.m_instance; }
            set { INFO.m_instance = value; }
        }

        public override Unit make()
        {
            return new Unit(m_player, (Dept)this);
        }

        // Overload the conversion from Department to Dept:
        public static implicit operator Dept(INFO x)
        {
            return Dept.INFO;
        }
    }
}
