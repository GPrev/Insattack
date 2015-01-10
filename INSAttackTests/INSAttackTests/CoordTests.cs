using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using INSAttack;
using MapDataModel;
using System.Collections.Generic;

namespace INSAttackTests
{
    [TestClass]
    public class CoordTests
    {
        private Coord c1, c2;
        private List<Coord> adj1, adj2;

        [TestInitialize]
        public void init()
        {
            c1 = new Coord(1, 1); //"odd" tile
            c2 = new Coord(2, 1); //"even" tile

            adj1 = new List<Coord>();
            adj1.Add(new Coord(0, 0));
            adj1.Add(new Coord(1, 0));
            adj1.Add(new Coord(2, 0));
            adj1.Add(new Coord(0, 1));
            adj1.Add(new Coord(1, 2));
            adj1.Add(new Coord(2, 1));

            adj2 = new List<Coord>();
            adj2.Add(new Coord(1, 1));
            adj2.Add(new Coord(2, 0));
            adj2.Add(new Coord(3, 1));
            adj2.Add(new Coord(1, 2));
            adj2.Add(new Coord(2, 2));
            adj2.Add(new Coord(3, 2));
        }

        [TestCleanup]
        public void cleanup()
        {

        }

        [TestMethod]
        public void Coord_EqualityTest()
        {
            Assert.AreEqual(c1, c1);
            Assert.AreNotEqual(c2, c1);
        }

        [TestMethod]
        public void Coord_AdditionTest()
        {
            Assert.AreEqual(new Coord(3,2), c1 + c2);
        }

        [TestMethod]
        public void Coord_AdjacencyDetecionTest()
        {
            //odd tile

            foreach(Coord c in adj1)
            {
                Assert.IsTrue(Coord.areAdjacent(c1, c));
            }

            Assert.IsFalse(Coord.areAdjacent(c1, new Coord(0, -1)));
            Assert.IsFalse(Coord.areAdjacent(c1, new Coord(2, -1)));
            Assert.IsFalse(Coord.areAdjacent(c1, new Coord(-1, 1)));
            Assert.IsFalse(Coord.areAdjacent(c1, new Coord(3, 1)));
            Assert.IsFalse(Coord.areAdjacent(c1, new Coord(0, 2)));
            Assert.IsFalse(Coord.areAdjacent(c1, new Coord(2, 2)));

            //even tile

            foreach (Coord c in adj2)
            {
                Assert.IsTrue(Coord.areAdjacent(c2, c));
            }

            Assert.IsFalse(Coord.areAdjacent(c2, new Coord(1, 0)));
            Assert.IsFalse(Coord.areAdjacent(c2, new Coord(3, 0)));
            Assert.IsFalse(Coord.areAdjacent(c2, new Coord(0, 1)));
            Assert.IsFalse(Coord.areAdjacent(c2, new Coord(4, 1)));
            Assert.IsFalse(Coord.areAdjacent(c2, new Coord(1, 3)));
            Assert.IsFalse(Coord.areAdjacent(c2, new Coord(3, 3)));

            //Inexistent tile
            Assert.IsFalse(Coord.areAdjacent(new Coord(-1, 0), new Coord(0, 0)));
        }
        
        [TestMethod]
        public void Coord_AdjacencyListTest()
        {
            List<Coord> c1adjacent = c1.adjacentCoords();
            Assert.AreEqual(6, c1adjacent.Count);
            foreach (Coord c in adj1)
            {
                Assert.IsTrue(c1adjacent.Contains(c));
            }

            List<Coord> c2adjacent = c2.adjacentCoords();
            Assert.AreEqual(6, c2adjacent.Count);
            foreach (Coord c in adj2)
            {
                Assert.IsTrue(c2adjacent.Contains(c));
            }
        }
    }
}
