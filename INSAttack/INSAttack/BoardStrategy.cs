using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MapDataModel;


namespace INSAttack
{
    public abstract class BoardStrategy : INSAttack.Factory<Board>
    {
        private int m_boardSize;

        public int BoardSize
        {
            get { return m_boardSize; }
            set { m_boardSize = value; }
        }
        private int m_nbPlayers;

        public int NbPlayers
        {
            get { return m_nbPlayers; }
            set { m_nbPlayers = value; }
        }
        private int m_nbTurns;

        public int NbTurns
        {
            get { return m_nbTurns; }
            set { m_nbTurns = value; }
        }
        private int m_nbUnits;

        public int NbUnits
        {
            get { return m_nbUnits; }
            set { m_nbUnits = value; }
        }

        public Board make()
        {
            throw new NotImplementedException();
        }
    }
}
