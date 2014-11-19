using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace INSAttack
{
    public abstract class GameBuilder : INSAttack.Factory<Game>
    {
        public Game make()
        {
            throw new NotImplementedException();
        }
    }
}
