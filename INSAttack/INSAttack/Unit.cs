using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace INSAttack
{
    public class Unit
    {
        private int m_attack;

        public int Attack
        {
            get { return m_attack; }
            set { m_attack = value; }
        }
        private int m_defense;

        public int Defense
        {
            get { return m_defense; }
            set { m_defense = value; }
        }
        private int m_life;

        public int Life
        {
            get { return m_life; }
            set { m_life = value; }
        }
        private int m_maxLife;

        public int MaxLife
        {
            get { return m_maxLife; }
            set { m_maxLife = value; }
        }
        private int m_movement;

        public int Movement
        {
            get { return m_movement; }
            set { m_movement = value; }
        }
        private int m_maxMovement;

        public int MaxMovement
        {
            get { return m_maxMovement; }
            set { m_maxMovement = value; }
        }

        private Player m_player;

        public Player Player
        {
            get { return m_player; }
            set { m_player = value; }
        }
    }
}
