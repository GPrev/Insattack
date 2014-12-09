using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using INSAttack;
using MapDataModel;
using System.Collections.Generic;


namespace INSAttackTests
{
    [TestClass]
    public class BoardStrategyTests
    {
        private BoardStrategy m_boardCreator;
        private List<Department> m_departments;
        private Board m_board;

        [TestInitialize]
        public void init()
        {
            Player p1 = new Player();
            Player p2 = new Player();

            m_departments = new List<Department>();
            m_departments.Add(new INFO(p1));
            m_departments.Add(new EII(p2));

        }

        [TestCleanup]
        public void cleanup()
        {
            m_departments.Clear();
        }
        
        [TestMethod]
        public void SmallBoardStrategyTest()
        {
            m_boardCreator = new SmallBoardStrategy(m_departments);
            m_board = m_boardCreator.make();
            Assert.AreEqual(m_boardCreator.BoardSize, m_board.Map.Size);

            int nbUnits = 0;
            bool test = false;
            foreach (var unitList in m_board.UnitTable)
            {
                nbUnits += unitList.Value.Count;
                foreach (var u in unitList.Value)
                {
                    test |= ((int)u.Dept == (int)Dept.EII) || ((int)u.Dept == (int)Dept.INFO);
                }
            }
            Assert.AreEqual(m_boardCreator.NbUnits*m_boardCreator.NbPlayers, nbUnits);
            Assert.IsTrue(test);
        }

        [TestMethod]
        public void DemoBoardStrategyTest()
        {
            m_boardCreator = new DemoBoardStrategy(m_departments);
            m_board = m_boardCreator.make();
            Assert.AreEqual(m_boardCreator.BoardSize, m_board.Map.Size);

            bool test = false;
            int nbUnits = 0;
            foreach (var unitList in m_board.UnitTable)
            {
                nbUnits += unitList.Value.Count; 
                foreach (var u in unitList.Value)
                {
                    test |= ((int)u.Dept == (int)Dept.EII) || ((int)u.Dept == (int)Dept.INFO);
                }
            }
            Assert.AreEqual(m_boardCreator.NbUnits * m_boardCreator.NbPlayers, nbUnits);
            Assert.IsTrue(test);
        }

        [TestMethod]
        public void NormalBoardStrategyTest()
        {
            m_boardCreator = new NormalBoardStrategy(m_departments);
            m_board = m_boardCreator.make();
            Assert.AreEqual(m_boardCreator.BoardSize, m_board.Map.Size);

            int nbUnits = 0;
            bool test = false;
            foreach (var unitList in m_board.UnitTable)
            {
                nbUnits += unitList.Value.Count;
                foreach (var u in unitList.Value)
                {
                    test |= ((int)u.Dept == (int)Dept.EII) || ((int)u.Dept == (int)Dept.INFO);
                }
            }
            Assert.AreEqual(m_boardCreator.NbUnits * m_boardCreator.NbPlayers, nbUnits);
            Assert.IsTrue(test);
        }

    }
}
