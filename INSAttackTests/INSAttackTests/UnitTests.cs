using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using INSAttack;

namespace INSAttackTests
{
    [TestClass]
    public class UnitTests
    {
        Player m_p1;
        Player m_p2;
        Unit m_u1;
        Unit m_u2;

        [TestInitialize]
        public void init()
        {
            m_p1 = new Player();
            m_p2 = new Player();
            m_u1 = new Unit(m_p1);
            m_u2 = new Unit(m_p2);
            m_u1.MaxMovement = 2;
            m_u1.resetMovement();
            m_u2.MaxMovement = 2;
            m_u2.resetMovement();
            m_u1.MaxLife = 2;
            m_u1.Life = 2;
            m_u2.MaxLife = 2;
            m_u2.Life = 2;
        }

        [TestCleanup]
        public void cleanup()
        {

        }

        [TestMethod]
        public void Unit_AllyTest()
        {
            Assert.IsTrue(m_u1.isAlly(m_u1));
            Assert.IsFalse(m_u1.isAlly(m_u2));
        }

        [TestMethod]
        public void Unit_MovementTest()
        {
            Assert.IsTrue(m_u1.tryAndUseMovement(1));
            Assert.IsTrue(m_u1.tryAndUseMovement(1));
            Assert.IsFalse(m_u1.tryAndUseMovement(1));
            Assert.AreEqual(0, m_u1.Movement);
            m_u1.resetMovement();
            Assert.AreEqual(2, m_u1.Movement);
        }

        [TestMethod]
        public void Unit_LifeTest()
        {
            Assert.AreEqual(1, m_u1.getHealthRatio());
            Assert.IsFalse(m_u1.isDead());
            Assert.IsTrue(m_u1.takeHit(1));
            Assert.AreEqual(.5f, m_u1.getHealthRatio());
            Assert.IsFalse(m_u1.takeHit(1));
            Assert.AreEqual(0, m_u1.getHealthRatio());
            Assert.IsTrue(m_u1.isDead());
        }

        [TestMethod]
        public void Unit_InitTest()
        {
            m_u1.init(1, 2, 3, 4);
            Assert.AreEqual(1, m_u1.MaxMovement);
            Assert.AreEqual(1, m_u1.Movement);
            Assert.AreEqual(2, m_u1.MaxLife);
            Assert.AreEqual(2, m_u1.Life);
            Assert.AreEqual(3, m_u1.Attack);
            Assert.AreEqual(4, m_u1.Defense);
        }
    }
}
