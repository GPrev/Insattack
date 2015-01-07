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
            m_departments.Add(new EII(new Player()));
            m_departments.Add(new INFO(new Player()));
            gameBuilder.Departments = m_departments;
            gameBuilder.BoardCreator = new NormalBoardStrategy(m_departments);
            m_game = gameBuilder.make();
        }

        [TestCleanup]
        public void cleanup()
        {

        }

        [TestMethod]
        public void Game_EqualityTest()
        {
            NewGameBuilder gameBuilder = new NewGameBuilder();
            List<Department> departments = new List<Department>();
            departments.Add(new SRC(new Player()));
            departments.Add(new INSAttack.GC(new Player()));
            gameBuilder.Departments = departments;
            gameBuilder.BoardCreator = new NormalBoardStrategy(departments);
            Game game = gameBuilder.make();

            Assert.AreEqual(m_game, m_game);
            Assert.AreNotEqual(m_game, game);
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

        public void Game_PointsTests()
        {
            int test = 0;
            foreach (var ul in m_game.Board.UnitTable)
            {
                test++;
            }
            Assert.AreEqual(2, test);

            foreach (var player in m_game.Players)
            {
                Assert.AreEqual(1, m_game.points(player));
            }
            
            

            Coord coord = new Coord(2, 1);
            Unit unit1 = m_departments[0].make();
            Unit unit2 = m_departments[0].make();

            m_game.Board.addUnit(coord, unit1);
            m_game.Board.addUnit(coord, unit2);
            unit1.init(2, 4, 4, 3);
            unit2.init(2, 4, 4, 3);
            Assert.AreEqual(2, m_game.points(m_game.Players.First()));



            Coord coord2 = new Coord(2, 2);
            Unit unit = m_departments[0].make();
            m_game.Board.addUnit(coord2, unit);
            unit2.init(2, 4, 4, 3);
            Assert.AreEqual(3, m_game.points(m_game.Players.First()));

            unit.Points = 4;
            Assert.AreEqual(7, m_game.points(m_game.Players.First()));


            m_game.Board.removeUnit(unit1);
            m_game.Board.removeUnit(unit2);
            m_game.Board.removeUnit(unit);
        }

        [TestMethod]
        public void Game_countUnitsTests()
        {
            int nbUnitsPerPlayer = 8;
            foreach (var player in m_game.Players)
            {
                Assert.AreEqual(nbUnitsPerPlayer, m_game.countUnits(player));
            }

            Coord coord = new Coord(2, 1);
            Unit unit1 = m_departments[0].make();
            Unit unit2 = m_departments[0].make();

            m_game.Board.addUnit(coord, unit1);
            m_game.Board.addUnit(coord, unit2);
            unit1.init(2, 4, 4, 3);
            unit2.init(2, 4, 4, 3);
            Assert.AreEqual(nbUnitsPerPlayer+2, m_game.countUnits(m_game.Players.First()));



            Coord coord2 = new Coord(1, 1);
            Unit unit = m_departments[0].make();
            m_game.Board.addUnit(coord2, unit);
            unit.init(2, 4, 4, 3);
            Assert.AreEqual(nbUnitsPerPlayer+3, m_game.countUnits(m_game.Players.First()));


            m_game.Board.removeUnit(unit1);
            m_game.Board.removeUnit(unit2);
            m_game.Board.removeUnit(unit);
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

            Unit target2 = m_departments[1].make();
            m_game.Board.addUnit(dest, target);
            target2.init(2, 5, 4, 10);

            Assert.AreEqual(2, m_game.Board.UnitTable[dest].Count);
            Assert.AreEqual(3, target.Defense);
            Assert.AreEqual(10, target2.Defense);
            Assert.AreEqual(4, target.Life);
            Assert.AreEqual(5, target2.Life);


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

            Assert.AreEqual(4, target.Life);
        }
        
    }
}
