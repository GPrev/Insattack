﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using INSAttack;
using MapDataModel;

namespace INSAttackTests
{
    [TestClass]
    public class BoardTests
    {
        Board m_board;
        Player m_p1;

        [TestInitialize]
        public void init()
        {
            m_board = new Board();
            m_p1 = new Player();
        }

        [TestCleanup]
        public void cleanup()
        {

        }

        [TestMethod]
        public void Board_MovementTest()
        {
            Unit u1 = new Unit(m_p1, Dept.INFO);
            Unit u2 = new Unit(m_p1, Dept.INFO);

            Assert.IsTrue(m_board.addUnit(new Coord(1, 1), u1));    //u1 -> (1,1)
            Assert.AreEqual(m_board.find(u1), new Coord(1, 1));

            Assert.IsTrue(m_board.addUnit(new Coord(2, 2), u2));    //u2 -> (2,2)

            Assert.IsTrue(m_board.moveUnit(u1, new Coord(2, 2)));     //u1 -> (2,2)
            Assert.AreEqual(m_board.find(u1), new Coord(2, 2));
            Assert.AreEqual(m_board.find(u2), new Coord(2, 2));

            Assert.IsTrue(m_board.moveUnit(u1, new Coord(3, 3)));     //u1 -> (3,3)
            Assert.AreEqual(m_board.find(u1), new Coord(3, 3));
            Assert.AreEqual(m_board.find(u2), new Coord(2, 2));

            Assert.IsTrue(m_board.removeUnit(u1));                  //u1 -> X
            Assert.IsFalse(m_board.find(u1).exists());
        }

       // [TestMethod]
        public void Board_EqualityTest()
        {
            m_board = new Board();
            Board board = new Board();

            Coord coord = new Coord(1, 1);
            Unit unit = new Unit(m_p1, Dept.EII);

            

            Assert.AreEqual(m_board, m_board);
            Assert.AreEqual(m_board, board);

            board.addUnit(coord, unit);
            Assert.AreNotEqual(m_board, board);
        }
    }
}
