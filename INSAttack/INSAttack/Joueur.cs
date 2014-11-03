using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace INSAttack
{
    public class Player
    {
        private Department m_department;

        public Department Department
        {
            get { return m_department; }
            set { m_department = value; }
        }
    }
}
