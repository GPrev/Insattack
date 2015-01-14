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
            while (m_game.Board.NbTurns > 0)
            {
                m_game.Board.NbTurns--;
            }
            Assert.IsTrue(m_game.isGamefinished());


            //Test of the interdiction of displacement after the end of the game
            Coord coord = new Coord(2,1);
            unit = m_departments[0].make();
            m_game.Board.addUnit(coord, unit);
            unit.init(2, 4, 4, 3);
            Coord dest = new Coord(coord.X, coord.Y + 1);

            Assert.IsFalse(m_game.move(unit, coord));
            Assert.IsFalse(m_game.move(unit, dest));

            //tests the end of the game
            m_game.Board.NbTurns = 1;
            Assert.IsTrue(m_game.endOfTurn());
            Assert.IsTrue(m_game.isGamefinished());

            m_game.Board.NbTurns = 10;
            m_game.removePlayer(m_game.Players[1]);
            Assert.IsTrue(m_game.endOfTurn());
            Assert.IsTrue(m_game.isGamefinished());
            //Clean
            m_game.Board.removeUnit(unit);

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
                Assert.AreEqual(1, m_game.getPoints(player));
            }
            
            

            Coord coord = new Coord(2, 1);
            Unit unit1 = m_departments[0].make();
            Unit unit2 = m_departments[0].make();

            m_game.Board.addUnit(coord, unit1);
            m_game.Board.addUnit(coord, unit2);
            unit1.init(2, 4, 4, 3);
            unit2.init(2, 4, 4, 3);
            Assert.AreEqual(2, m_game.getPoints(m_game.Players.First()));



            Coord coord2 = new Coord(2, 2);
            Unit unit = m_departments[0].make();
            m_game.Board.addUnit(coord2, unit);
            unit2.init(2, 4, 4, 3);
            Assert.AreEqual(3, m_game.getPoints(m_game.Players.First()));

            unit.Points = 4;
            Assert.AreEqual(7, m_game.getPoints(m_game.Players.First()));


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
            Assert.AreEqual(coord, m_game.Board.find(unit));


            unit.Movement = 0;
            Assert.IsFalse(m_game.move(unit, dest));
            Assert.AreEqual(coord, m_game.Board.find(unit));

            unit.resetMovement();
            Coord dest2 = new Coord(coord.X, coord.Y + 2);
            Assert.IsFalse(Coord.areAdjacent(coord, dest2));
            Assert.IsFalse(m_game.move(unit, dest2));
            Assert.AreEqual(coord, m_game.Board.find(unit));
            Assert.AreEqual(unit.MaxMovement, unit.Movement);


            Assert.IsFalse(m_game.move(unit, new Coord(-4, -42)));
            Assert.AreEqual(coord, m_game.Board.find(unit));
            Assert.AreEqual(unit.MaxMovement, unit.Movement);


            //tests for battles
            unit.init(2, 4, 4, 3);

            //With only one unit on the destination
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

                if (!unit.isDead())
                {
                    Assert.AreEqual(coord, m_game.Board.find(unit));
                }
                else
                {
                    Assert.AreEqual(Coord.nowhere, m_game.Board.find(unit));
                }
            }
            m_game.Board.removeUnit(unit);
            m_game.Board.removeUnit(target);

            //With two units on the destination
            unit = m_departments[0].make();
            m_game.Board.addUnit(coord, unit);
            unit.init(2, 4, 4, 3);

            target = m_departments[1].make();
            m_game.Board.addUnit(dest, target);
            target.init(2, 4, 4, 3);

            Unit target2 = m_departments[1].make();
            m_game.Board.addUnit(dest, target2);
            target2.init(2, 5, 4, 10);

            Assert.AreEqual(2, m_game.Board.UnitTable[dest].Count);
            Assert.AreEqual(3, target.Defense);
            Assert.AreEqual(10, target2.Defense);
            Assert.AreEqual(4, target.Life);
            Assert.AreEqual(5, target2.Life);

            moved = m_game.move(unit, dest);
            Assert.AreEqual(false, moved);

            Assert.AreEqual(target.MaxLife, target.Life);


            //Cleaning the previous tests
            m_game.Board.removeUnit(unit);
            m_game.Board.removeUnit(target);
            m_game.Board.removeUnit(target2);

            //Test of the teleportation
            //TileFactory factory = new TileFactory();
            Player player = m_game.ActivePlayer;
            unit = (new INFO(player)).make();
            coord = new Coord(2, 1);
            dest = new Coord(coord.X, coord.Y + 2);
            m_game.Board.addUnit(coord, unit);

            Assert.IsTrue(dest.exists());
            Assert.IsTrue(coord.exists());
            m_game.Board.Map.TileTable[coord] = TileFactory.Instance.InfoTile;
            m_game.Board.Map.TileTable[dest] = TileFactory.Instance.InfoTile;
            Assert.IsTrue(m_game.move(unit, dest));
            Assert.AreEqual(unit.MaxMovement, unit.Movement);
            m_game.move(unit, coord);


            m_game.Board.Map.TileTable[dest] = TileFactory.Instance.TdTile;
            Assert.IsFalse(m_game.move(unit, dest));
            Assert.AreEqual(unit.MaxMovement, unit.Movement);


            m_game.Board.Map.TileTable[coord] = TileFactory.Instance.TdTile;
            m_game.Board.Map.TileTable[dest] = TileFactory.Instance.InfoTile;
            Assert.IsFalse(m_game.move(unit, dest));
            Assert.AreEqual(unit.MaxMovement, unit.Movement);


            unit = (new SRC(player)).make();
            m_game.Board.Map.TileTable[coord] = TileFactory.Instance.InfoTile;
            m_game.Board.Map.TileTable[dest] = TileFactory.Instance.InfoTile;
            Assert.IsFalse(m_game.move(unit, dest));
            Assert.AreEqual(unit.MaxMovement, unit.Movement);
            m_game.move(unit, coord);

            //clean
            m_game.Board.removeUnit(unit);
        }

        [TestMethod]
        public void Game_GainPointOnKill()
        {
            Player player;
            Unit unit;
            Coord coord;
            Unit target;
            Coord dest;
            while (!m_game.ActivePlayer.Equals(m_game.Players.First())) m_game.endOfTurn();
            

            //gain point on kill allow
            player = m_game.Players.First();
            unit = (new EII(player)).make();
            coord = new Coord(2, 1);
            m_game.Board.addUnit(coord, unit);
            unit.init(1000, 1000, 1000, 1000);

            dest = new Coord(coord.X, coord.Y + 1);
            target = (new GMA(m_game.Players[1])).make();
            target.init(1, 1, 0, 0);
            m_game.Board.addUnit(dest, target);

            Assert.IsTrue(m_game.move(unit, dest));
            Assert.IsTrue(target.isDead());
            Assert.AreEqual(1, unit.Points);

            Assert.IsTrue(m_game.Board.removeUnit(unit));
            Assert.AreEqual(0, m_game.Board.UnitTable[coord].Count);
            Assert.AreEqual(0, m_game.Board.UnitTable[dest].Count);


            //gain point on kill forbidden
            player = m_game.ActivePlayer;
            unit = (new INFO(player)).make();
            coord = new Coord(4, 3);
            m_game.Board.addUnit(coord, unit);
            unit.init(1000, 1000, 1000, 1000);

            dest = new Coord(coord.X, coord.Y + 1);
            target = (new GMA(m_game.Players[1])).make();
            target.init(1, 1, 0, 0);
            m_game.Board.addUnit(dest, target);

            Assert.IsTrue(m_game.move(unit, dest));
            Assert.IsTrue(target.isDead());
            Assert.AreEqual(0, unit.Points);

            Assert.IsTrue(m_game.Board.removeUnit(unit));
            Assert.AreEqual(0, m_game.Board.UnitTable[coord].Count);
            Assert.AreEqual(0, m_game.Board.UnitTable[dest].Count);


        }
        
    }
}
