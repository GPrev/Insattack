using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Wrapper;
using MapDataModel;

namespace INSAttackTests
{
    [TestClass]
    public class MapGeneratorTests
    {
        WrapperMapGenerator m_wrapper;
        MapData m_map;

        [TestInitialize]
        public void init()
        {
            m_wrapper = new WrapperMapGenerator();
            m_map = m_wrapper.makeMap(10, 3, 5, 15, 10, 1);
        }

        [TestCleanup]
        public void cleanup()
        {

        }

        [TestMethod]
        public void MapGenerator_SizeTest()
        {
            Assert.AreEqual(10, m_map.Size);
        }

        [TestMethod]
        public void MapGenerator_NbPlayersTest()
        {
            Assert.AreEqual(3, m_map.StartingPos.Count);
        }

        [TestMethod]
        public void MapGenerator_NbTilesTest()
        {
            //Create a different dictionary to store the counts.
            Dictionary<Tile, int> valCount = new Dictionary<Tile, int>();
            //Iterate through the values, setting count to 1 or incrementing current count.
            foreach (Tile t in m_map.TileTable.Values)
                if (valCount.ContainsKey(t))
                    valCount[t]++;
                else
                    valCount[t] = 1;

            Assert.AreEqual(5, valCount[TileFactory.Instance.AmphiTile]);
            Assert.AreEqual(15, valCount[TileFactory.Instance.TdTile]);
            Assert.AreEqual(10, valCount[TileFactory.Instance.InfoTile]);
            Assert.AreEqual(1, valCount[TileFactory.Instance.RestaurantTile]);
        }
    }
}
