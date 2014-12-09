using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MapDataModel;

namespace INSAttack
{
    public class SRC : Department
    {
        /// <summary>
        /// </summary>
        
        public SRC(Player player) : base(player)
        {
        }

        private static Department m_instance;

        public static Department Instance
        {
            get { return SRC.m_instance; }
            set { SRC.m_instance = value; }
        }

        public override Unit make()
        {
            return new Unit(m_player, (Dept)this);
        }

        // Overload the conversion from Department to Dept:
        public static implicit operator Dept(SRC x)
        {
            return Dept.SRC;
        }
    }
}
