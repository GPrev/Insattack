using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using INSAttack;

namespace INSAttackTests
{
    [TestClass]
    public class TileFactoryTests
    {
        TileFactory m_tileFactory;

        [TestInitialize]
        public void init()
        {
            m_tileFactory = new TileFactory();
        }

        [TestCleanup]
        public void cleanup()
        {

        }

        [TestMethod]
        public void UnityTest()
        {
            Tile tile1 = m_tileFactory.InfoTile;
            Tile tile2 = m_tileFactory.InfoTile;
            Assert.AreEqual(tile1, tile2);
        }
    }
}
