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
        private static Department m_instance;

        public static Department Instance
        {
            get { return GC.m_instance; }
            set { GC.m_instance = value; }
        }

        // Overload the conversion from Department to Dept:
        public static implicit operator Dept(GC x)
        {
            return Dept.GC;
        }
    }
}
