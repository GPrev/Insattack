using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Wrapper;
using MapDataModel;

namespace INSAttackTests
{
    [TestClass]
    public class HintGiverTests
    {
        WrapperHintGiver m_wrapper;

        [TestInitialize]
        public void init()
        {
            m_wrapper = new WrapperHintGiver();
        }

        [TestCleanup]
        public void cleanup()
        {

        }

        [TestMethod]
        public void HintGiver_NbMaxTest()
        {
            
            List<Tuple<Tile, bool, int>> data = new List<Tuple<Tile,bool,int>>();
            for(int i = 0; i < 6; ++i)
            {
                data.Add(new Tuple<Tile, bool, int>(TileFactory.Instance.OutdoorTile, false, 0));
            }
            var res = m_wrapper.giveHint(data);
            Assert.IsTrue(res.Count <= 3);
        }

        [TestMethod]
        public void HintGiver_FreeTilesTest()
        {

            List<Tuple<Tile, bool, int>> data = new List<Tuple<Tile, bool, int>>();
            for (int i = 0; i < 3; ++i)
            {
                data.Add(new Tuple<Tile, bool, int>(TileFactory.Instance.OutdoorTile, true, 0));
            }
            for (int i = 0; i < 3; ++i)
            {
                data.Add(new Tuple<Tile, bool, int>(TileFactory.Instance.OutdoorTile, false, 0));
            }
            var res = m_wrapper.giveHint(data);
            Assert.AreEqual(3, res.Count);
            Assert.IsTrue(res.Contains(3));
            Assert.IsTrue(res.Contains(4));
            Assert.IsTrue(res.Contains(5));
        }

        [TestMethod]
        public void HintGiver_WeakestEnemiesTest()
        {

            List<Tuple<Tile, bool, int>> data = new List<Tuple<Tile, bool, int>>();
            for (int i = 0; i < 2; ++i)
            {
                data.Add(new Tuple<Tile, bool, int>(TileFactory.Instance.OutdoorTile, true, 0));
            }
            for (int i = 0; i < 3; ++i)
            {
                data.Add(new Tuple<Tile, bool, int>(TileFactory.Instance.OutdoorTile, false, i));
            }
            data.Add(new Tuple<Tile, bool, int>(TileFactory.Instance.OutdoorTile, false, 0));

            var res = m_wrapper.giveHint(data);
            Assert.AreEqual(3, res.Count);
            Assert.IsTrue(res.Contains(5));
            Assert.IsTrue(res.Contains(2));
            Assert.IsTrue(res.Contains(3));
        }
    }
}
