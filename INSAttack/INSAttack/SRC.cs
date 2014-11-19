﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace INSAttack
{
    public class SRC : Department
    {
        /// <summary>
        /// </summary>
        private static Department m_instance;

        public static Department Instance
        {
            get { return SRC.m_instance; }
            set { SRC.m_instance = value; }
        }

        // Overload the conversion from Department to Dept:
        public static implicit operator Dept(SRC x)
        {
            return Dept.SRC;
        }
    }
}
