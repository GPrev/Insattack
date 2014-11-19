using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace INSAttack
{
    public class EII : Department
    {
        /// <summary>
        /// </summary>
        private static Department m_instance;

        public static Department Instance
        {
            get { return EII.m_instance; }
            set { EII.m_instance = value; }
        }

        // Overload the conversion from Department to Dept:
        public static implicit operator Dept(EII x)
        {
            return Dept.EII;
        }
    }
}
