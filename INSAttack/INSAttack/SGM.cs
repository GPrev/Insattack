using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MapDataModel;

namespace INSAttack
{
    public class SGM : Department
    {
        /// <summary>
        /// </summary>
        
        public SGM(Player player) : base(player)
        {
        }

        private static Department m_instance;

        public static Department Instance
        {
            get { return SGM.m_instance; }
            set { SGM.m_instance = value; }
        }

        public override Unit make()
        {
            return new Unit(m_player, (Dept)this);
        }

        // Overload the conversion from Department to Dept:
        public static implicit operator Dept(SGM x)
        {
            return Dept.SGM;
        }
    }
}
