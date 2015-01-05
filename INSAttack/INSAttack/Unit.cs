using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MapDataModel;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Soap;
using System.Runtime.Serialization.Formatters.Binary;

namespace INSAttack
{
    [Serializable()]
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

        private Dept m_dept;

        public Dept Dept
        {
            get { return m_dept; }
            //set { m_dept = value; }
        }

        private int m_id;

        public int Id
        {
            get { return m_id; }
            set { m_id = value; }
        }

        private static int m_count = 0;

        public static int Count
        {
            get { return Unit.m_count; }
            set { Unit.m_count = value; }
        }

        public Unit(INSAttack.Player player, Dept dept)
        {
            m_player = player;
            m_dept = dept;
            m_id = m_count;
            m_count++;
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

        public void useAllMovement()
        {
            m_movement = 0;
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

        public override bool Equals(object obj)
        {
            return obj is Unit && Equals((Unit)obj);
        }

        public bool Equals(Unit unit)
        {
            if (!m_id.Equals(unit.m_id)) return false;
            if (!m_player.Equals(unit.m_player)) return false;
            if (!m_dept.Equals(unit.m_dept)) return false;
            if (!m_life.Equals(unit.m_life)) return false;
            if (!m_maxLife.Equals(unit.m_maxLife)) return false;
            if (!m_movement.Equals(unit.m_movement)) return false;
            if (!m_maxMovement.Equals(unit.m_maxMovement)) return false;
            if (!m_attack.Equals(unit.m_attack)) return false;
            if (!m_defense.Equals(unit.m_defense)) return false;

            return true;
        }
    }
}
