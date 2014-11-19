using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace INSAttack
{
    public class Coord
    {
        public Coord(int x, int y)
        {
            m_x = x;
            m_y = y;
        }

        private int m_x;

        public int X
        {
            get { return m_x; }
            set { m_x = value; }
        }
        private int m_y;

        public int Y
        {
            get { return m_y; }
            set { m_y = value; }
        }
        public override int GetHashCode()
        {
            return 16 * m_x + m_y;
        }

        public override bool Equals(object obj)
        {
            return obj is Coord && Equals((Coord)obj);
        }

        public bool Equals(Coord p)
        {
            return m_x == p.X && m_y == p.Y;
        }
    }
}
