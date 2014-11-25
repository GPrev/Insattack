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

        public Unit(INSAttack.Player player)
        {
            m_player = player;
        }

        public void init(int movement, int life, int attack=1, int defense=1)
        {
            m_maxMovement = movement;
            m_movement = m_maxMovement;
            m_maxLife = life;
            m_life = m_maxLife;
            m_attack = attack;
            m_defense = defense;
        }

        //return true if the target is an ally overthise false.
        public bool isAlly(Unit cible)
        {
            return m_player.isAlly(cible.Player);
        }

        //returns true if the unit has enough movement
        public bool tryAndUseMovement(int movementUsed)
        {
            if (movementUsed <= m_movement)
            {
                m_movement -= movementUsed;
                return true;
            }
            //else
            return false;
        }

        public void resetMovement()
        {
            m_movement = m_maxMovement;
        }

        //returns true if still alive
        public bool takeHit(int damage)
        {
            if(damage >= m_life)
            {
                m_life = 0;
                return false;
            }
            //else still alive
            m_life -= damage;
            return true;
        }

        public bool isDead()
        {
            return m_life <= 0;
        }

        public float getHealthRatio()
        {
            return (float)m_life / (float)m_maxLife;
        }
    }
}
