using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Soap;
using System.Runtime.Serialization.Formatters.Binary;
using MapDataModel;

namespace INSAttack
{
    [Serializable()]
    public class Player
    {
        public Player()
        {
            m_id = m_count;
            m_count++;
            m_name = "Joueur " + m_id;
        }
        public Player(String name, Dept dept = Dept.INFO)
        {
            m_id = m_count;
            m_count++;
            m_name = name;
            m_dept = dept;
        }

        private int m_id;

        private static int m_count = 0;

        public static int Count
        {
            get { return Player.m_count; }
            set { Player.m_count = value; }
        }

        public int Id
        {
            get { return m_id; }
            set { m_id = value; }
        }

        private String m_name;

        public String Name
        {
            get { return m_name; }
            set { m_name = value; }
        }

        private Dept m_dept;

        public Dept Dept
        {
            get { return m_dept; }
            set { m_dept = value; }
        }

        public bool isAlly(Player p)
        {
            return this == p;
        }

        public override bool Equals(object obj)
        {
            return obj is Player && Equals((Player)obj);
        }

        public bool Equals(Player p)
        {
            return m_id == p.Id && m_name.Equals(p.Name);
        }

        public static bool operator ==(Player a, Player b)
        {
            // If both are null, or both are same instance, return true.
            if (System.Object.ReferenceEquals(a, b))
            {
                return true;
            }

            // If one is null, but not both, return false.
            if (((object)a == null) || ((object)b == null))
            {
                return false;
            }

            // Return true if the fields match:
            return a.Equals(b);
        }

        public static bool operator !=(Player a, Player b)
        {
            return !(a == b);
        }

        public override int GetHashCode()
        {
            return m_id;
        }

        public String toString()
        {
            return Name;
        }
    }
}
