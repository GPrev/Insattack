using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using INSAttack;
using MapDataModel;

namespace INSAttackTests
{
    [TestClass]
    public class CoordTests
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
        public void Coord_EqualityTest()
        {
            Assert.AreEqual(new Coord(1, 1), new Coord(1, 1));
            Assert.AreNotEqual(new Coord(1, 1), new Coord(2, 1));
        }

        [TestMethod]
        public void Coord_AdjacencyTest()
        {
            Coord c1 = new Coord(1, 1); //"odd" tile

            Assert.IsTrue(Coord.areAdjacent(c1, new Coord(0, 0)));
            Assert.IsTrue(Coord.areAdjacent(c1, new Coord(1, 0)));
            Assert.IsTrue(Coord.areAdjacent(c1, new Coord(2, 0)));
            Assert.IsTrue(Coord.areAdjacent(c1, new Coord(0, 1)));
            Assert.IsTrue(Coord.areAdjacent(c1, new Coord(1, 2)));
            Assert.IsTrue(Coord.areAdjacent(c1, new Coord(2, 1)));

            Assert.IsFalse(Coord.areAdjacent(c1, new Coord(0, -1)));
            Assert.IsFalse(Coord.areAdjacent(c1, new Coord(2, -1)));
            Assert.IsFalse(Coord.areAdjacent(c1, new Coord(-1, 1)));
            Assert.IsFalse(Coord.areAdjacent(c1, new Coord(3, 1)));
            Assert.IsFalse(Coord.areAdjacent(c1, new Coord(0, 2)));
            Assert.IsFalse(Coord.areAdjacent(c1, new Coord(2, 2)));


            Coord c2 = new Coord(2, 1); //"even" tile

            Assert.IsTrue(Coord.areAdjacent(c2, new Coord(1, 1)));
            Assert.IsTrue(Coord.areAdjacent(c2, new Coord(2, 0)));
            Assert.IsTrue(Coord.areAdjacent(c2, new Coord(3, 1)));
            Assert.IsTrue(Coord.areAdjacent(c2, new Coord(1, 2)));
            Assert.IsTrue(Coord.areAdjacent(c2, new Coord(2, 2)));
            Assert.IsTrue(Coord.areAdjacent(c2, new Coord(3, 2)));

            Assert.IsFalse(Coord.areAdjacent(c2, new Coord(1, 0)));
            Assert.IsFalse(Coord.areAdjacent(c2, new Coord(3, 0)));
            Assert.IsFalse(Coord.areAdjacent(c2, new Coord(0, 1)));
            Assert.IsFalse(Coord.areAdjacent(c2, new Coord(4, 1)));
            Assert.IsFalse(Coord.areAdjacent(c2, new Coord(1, 3)));
            Assert.IsFalse(Coord.areAdjacent(c2, new Coord(3, 3)));

            //Inexistent tile
            Assert.IsFalse(Coord.areAdjacent(new Coord(-1, 0), new Coord(0, 0)));
        }
    }
}
