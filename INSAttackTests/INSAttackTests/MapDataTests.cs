using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MapDataModel;

namespace INSAttackTests
{

    [TestClass]
    public class MapDataTests
    {


        [TestMethod]
        public void MapData_EqualityTests()
        {
            MapData m_map = new MapData();
            MapData map = new MapData();

            Assert.AreEqual(m_map, map);

            //Assert.AreNotEqual(m_map, map);
            Assert.AreEqual(map, map);
        }

        [TestMethod]
        public void Tile_EqualityTests()
        {
            INFOTile info1 = new INFOTile();
            INFOTile info2 = new INFOTile();

            OutdoorTile out1 = new OutdoorTile();
            OutdoorTile out2 = new OutdoorTile();

            TDTile td1 = new TDTile();
            TDTile td2 = new TDTile();

            AmphiTile amphi1 = new AmphiTile();
            AmphiTile amphi2 = new AmphiTile();

            Assert.AreEqual(info1, info2);
            Assert.AreEqual(out1, out2);
            Assert.AreEqual(td1, td2);
            Assert.AreEqual(amphi1, amphi2);

            Assert.AreNotEqual(info1, out2);
            Assert.AreNotEqual(td1, out2);
            Assert.AreNotEqual(td1, info1);
            Assert.AreNotEqual(td2, amphi2);

            Tile tile = new INFOTile();
            Assert.AreEqual(info1, tile);
            Assert.AreNotEqual(tile, td1);
        }
    }
}
