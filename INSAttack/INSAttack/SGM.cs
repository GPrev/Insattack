﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace INSAttack
{
    public class SGM : Department
    {
        /// <summary>
        /// </summary>
        private static Department m_instance;

        public static Department Instance
        {
            get { return SGM.m_instance; }
            set { SGM.m_instance = value; }
        }
    }
}
