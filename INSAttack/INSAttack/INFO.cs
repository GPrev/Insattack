﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace INSAttack
{
    public class INFO : Department
    {
        /// <summary>
        /// </summary>
        private static Department m_instance;

        public static Department Instance
        {
            get { return INFO.m_instance; }
            set { INFO.m_instance = value; }
        }
    }
}