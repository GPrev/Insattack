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

        public bool exists() //returns false if in the negative range
        {
            return m_x >= 0 && m_y >= 0;
        }

        public static bool areAdjacent(Coord c1, Coord c2)
        {
            if (!c1.exists() || !c2.exists()) //both squares must esist
                return false;

            if(c1.Y == c2.Y) //on the same line
            {
                return Math.Abs(c1.X - c2.X) == 1; //adjacent if exactly one tile apart
            }
            if(c1.X%2 == 1) //c1 is an "odd" square, slightly above the line
            {
                if (c1.Y - c2.Y == 1) //c2 is 1 tile higher than c1
                {
                    return Math.Abs(c1.X - c2.X) <= 1; //adjacent if close enough horizontally
                }
                else if (c2.Y - c1.Y == 1) //c1 is 1 tile higher than c2
                {
                    return c1.X == c2.X; //they have to be aligned horizontally
                }
            }
            else //c1 is an "even" square, slightly below the line
            {
                if (c2.Y - c1.Y == 1) //c1 is 1 tile higher than c2
                {
                    return Math.Abs(c2.X - c1.X) <= 1; //adjacent if close enough horizontally
                }
                else if (c1.Y - c2.Y == 1) //c2 is 1 tile higher than c1
                {
                    return c2.X == c1.X; //they have to be aligned horizontally
                }
            }
            //else
            return false;
        }

        public static Coord nowhere = new Coord(-1,-1);
    }
}
