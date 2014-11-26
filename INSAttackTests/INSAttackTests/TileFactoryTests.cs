using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using INSAttack;
using MapDataModel;

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
        public void Tile_FactoryUnityTest()
        {
            Tile tile1 = m_tileFactory.InfoTile;
            Tile tile2 = m_tileFactory.InfoTile;
            Assert.AreEqual(tile1, tile2);

            tile1 = m_tileFactory.TdTile;
            tile2 = m_tileFactory.TdTile;
            Assert.AreEqual(tile1, tile2);

            tile1 = m_tileFactory.OutdoorTile;
            tile2 = m_tileFactory.OutdoorTile;
            Assert.AreEqual(tile1, tile2);

            tile1 = m_tileFactory.AmphiTile;
            tile2 = m_tileFactory.AmphiTile;
            Assert.AreEqual(tile1, tile2);
        }

        [TestMethod]
        public void Tile_CostTests()
        {
            int cost = m_tileFactory.InfoTile.getcost(Dept.INFO);
            Assert.AreEqual(cost, 1);
        }
    }
}
