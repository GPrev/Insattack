using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using INSAttack;
using MapDataModel;

namespace INSAttackTests
{
    [TestClass]
    public class TileFactoryTests
    {

        [TestInitialize]
        public void init()
        {
        }

        [TestCleanup]
        public void cleanup()
        {

        }

        [TestMethod]
        public void TileFactory_UnityTest()
        {
            Tile tile1 = TileFactory.Instance.InfoTile;
            Tile tile2 = TileFactory.Instance.InfoTile;
            Assert.AreEqual(tile1, tile2);

            tile1 = TileFactory.Instance.TdTile;
            tile2 = TileFactory.Instance.TdTile;
            Assert.AreEqual(tile1, tile2);

            tile1 = TileFactory.Instance.OutdoorTile;
            tile2 = TileFactory.Instance.OutdoorTile;
            Assert.AreEqual(tile1, tile2);

            tile1 = TileFactory.Instance.AmphiTile;
            tile2 = TileFactory.Instance.AmphiTile;
            Assert.AreEqual(tile1, tile2);
        }

        [TestMethod]
        public void TileFactory_CostTests()
        {
            int cost = TileFactory.Instance.InfoTile.getcost(Dept.INFO);
            Assert.AreEqual(cost, 1);
        }
    }
}
