using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace INSAttack
{
    public abstract class BoardStrategy : INSAttack.Factory<Board>
    {
        public int NbPlayers
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }

        public int BoardSize
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }

        public int NbTurns
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }

        public int NbUnits
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }

        public object make()
        {
            throw new NotImplementedException();
        }
    }
}
