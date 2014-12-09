using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using INSAttack;
using MapDataModel;
using System.Linq;


namespace INSAttackTests
{
    [TestClass]
    public class GameTests
    {
        private Game m_game;
        private List<Department> m_departments;

        [TestInitialize]
        public void init()
        {
            NewGameBuilder gameBuilder = new NewGameBuilder();
            m_departments = new List<Department>();
            m_departments.Add(new INFO(new Player()));
            m_departments.Add(new EII(new Player()));
            gameBuilder.Departments = m_departments;
            gameBuilder.BoardCreator = new DemoBoardStrategy(m_departments);
            m_game = gameBuilder.make();
        }

        [TestCleanup]
        public void cleanup()
        {

        }

        [TestMethod]
        public void Game_EndOfTurnTest()
        {
            Coord c = m_game.Board.Map.StartingPos.First();
            Unit unit = m_game.Board.UnitTable[c].First();

            unit.Movement = 0;
            m_game.endOfTurn();

            bool test = true;
            foreach (var l in m_game.Board.UnitTable)
            {
                foreach (var u in l.Value)
                {
                    test &= (u.Movement == u.MaxMovement);
                }
            }
            Assert.IsTrue(test);

            Player p = m_game.ActivePlayer;
            for (int i = 0; i < m_game.NbPlayer; i++)
            {
                m_game.endOfTurn();
            }
            Assert.AreSame(p, m_game.ActivePlayer);
        }

        [TestMethod]
        public void Game_MoveTest()
        {
 

            Coord coord = new Coord(2,1);
            Unit unit = m_departments[0].make();
            m_game.Board.addUnit(coord, unit);
            unit.init(2, 4, 4, 3);

            //tests for simple moves
            Assert.IsTrue(m_game.move(unit, coord));

            Coord dest = new Coord(coord.X, coord.Y + 1);

            Assert.IsTrue(Coord.areAdjacent(coord, dest));
            Assert.IsTrue(m_game.move(unit, dest));
            Assert.AreEqual(dest, m_game.Board.find(unit));
            unit.resetMovement();
            m_game.move(unit, coord);


            unit.Movement = 0;
            Assert.IsFalse(m_game.move(unit, dest));
            Assert.AreEqual(coord, m_game.Board.find(unit));

            unit.resetMovement();
            Coord dest2 = new Coord(coord.X, coord.Y + 2);
            Assert.IsFalse(Coord.areAdjacent(coord, dest2));
            Assert.IsFalse(m_game.move(unit, dest2));
            Assert.AreEqual(coord, m_game.Board.find(unit));

            Assert.IsFalse(m_game.move(unit, new Coord(-4, -42)));
            Assert.AreEqual(coord, m_game.Board.find(unit));


            //tests for battles
            Unit target = m_departments[1].make();
            m_game.Board.addUnit(dest, target);
            target.init(2, 4, 4, 3);
            bool moved = m_game.move(unit, dest);
            Assert.AreEqual(moved, target.isDead());
            if (moved)
            {
                Assert.AreEqual(dest, m_game.Board.find(unit));
            }
            else
            {
                Assert.AreEqual(coord, m_game.Board.find(unit));
            }

            Unit target2 = m_departments[1].make();
            m_game.Board.addUnit(dest, target);

            Assert.AreEqual(4, target.Life);
        }

    }
}
