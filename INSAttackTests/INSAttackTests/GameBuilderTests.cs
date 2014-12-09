using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using INSAttack;
using MapDataModel;

namespace INSAttackTests
{
    [TestClass]
    public class GameBuilderTests
    {
        private List<Department> m_departments;

        [TestInitialize]
        public void init()
        {
            m_departments = new List<Department>();
            m_departments.Add(new INFO(new Player()));
            m_departments.Add(new EII(new Player()));

        }

        [TestCleanup]
        public void cleanup()
        {
        }

        [TestMethod]
        public void NewGameBuilderTests()
        {
            NewGameBuilder m_gameBuilder= new NewGameBuilder();
            m_gameBuilder.Departments = m_departments;
            m_gameBuilder.BoardCreator = new SmallBoardStrategy(m_departments);
            Assert.IsNotNull(m_gameBuilder.Departments);
            Assert.IsNotNull(m_gameBuilder.BoardCreator);

            Game game = m_gameBuilder.make();

            Assert.IsTrue(true);
            Assert.IsNotNull(game);
            Assert.IsNotNull(game.Board);
            Assert.AreEqual(m_departments.Count, game.NbPlayer);
            Assert.AreEqual(m_departments.Count, game.Players.Count);

            //Check for the initialisation of units
            foreach (var l in game.Board.UnitTable)
            {
                foreach (var u in l.Value)
                {
                    Assert.AreEqual(2, u.Movement);
                    Assert.AreEqual(4, u.Life);
                    Assert.AreEqual(4, u.Attack);
                    Assert.AreEqual(3, u.Defense);
                }
            }

            Assert.AreSame(m_departments.First().Player, game.ActivePlayer);
        }
    }
}
