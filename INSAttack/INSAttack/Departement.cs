using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MapDataModel;

namespace INSAttack
{
    public abstract class Department : Factory<object>
    {

        public Department(Player player)
        {
            m_player = player;
        }

        public Department()
        {
        }

        private Player m_player;

        public Player Player
        {
            get { return m_player; }
            set { m_player = value; }
        }

        public object make()
        {
            return new Unit(m_player, (Dept)this);
        }

        public override int GetHashCode()
        {
            return (int)this + m_player.Id * (int)Dept.NB_DEPT;
        }

        // Overload the conversion from Department to Dept:
        public static implicit operator Dept(Department x)
        {
            return Dept.NB_DEPT;
        }

        // Overload the conversion from Department to int:
        public static implicit operator int(Department x)
        {
            return (int)(Dept)x;
        }

        public override bool Equals(object obj)
        {
            return obj is Department && Equals((Department)obj);
        }

        public bool Equals(Department p)
        {
            return (Dept)this == (Dept)p;
        }
    }
}
