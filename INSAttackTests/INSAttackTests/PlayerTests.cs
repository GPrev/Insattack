using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using INSAttack;

namespace INSAttackTests
{
    [TestClass]
    public class PlayerTests
    {
        Player m_p1;
        Player m_p2;

        [TestInitialize]
        public void init()
        {
            m_p1 = new Player();
            m_p2 = new Player();
        }

        [TestCleanup]
        public void cleanup()
        {

        }

        [TestMethod]
        public void Player_IDTest()
        {
            Assert.AreEqual(m_p2.Id, m_p1.Id + 1);
        }

        [TestMethod]
        public void Player_EqualityTest()
        {
            Assert.AreEqual(m_p1, m_p1);
            Assert.AreNotEqual(m_p1, m_p2);
        }

        [TestMethod]
        public void Player_AllyTest()
        {
            Assert.IsTrue(m_p1.isAlly(m_p1));
            Assert.IsFalse(m_p1.isAlly(m_p2));
        }
    }
}
