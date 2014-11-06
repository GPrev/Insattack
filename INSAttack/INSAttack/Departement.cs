using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace INSAttack
{
    public abstract class Department : Factory<object>
    {
        private Player m_player;

        public Player Player
        {
            get { return m_player; }
            set { m_player = value; }
        }
    
        public object make()
        {
            throw new NotImplementedException();
        }
    }
}
