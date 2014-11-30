using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MapDataModel;

namespace INSAttack
{
    public abstract class GameBuilder : INSAttack.Factory<Game>
    {

        private List<Department> departments;

        public Game make()
        {
            throw new NotImplementedException();
            Game game = new Game();
            //création du plateau
            BoardStrategy boardCreator = new NormalBoardStrategy();
            Board board = boardCreator.make();
            game.Board = board;

            //création des joueurs
            departments = new List<Department>();
            Player p;
            for (int i = 0; i < 2; i++)
            {
                p = new Player();
                game.Players.Add(p);
                departments.Add(new INFO(p));
            }

            //Création des unités

             //initilisation du 1er tour
                game.ActivePlayer = game.Players.First();

            return game;
        }
    }
}
