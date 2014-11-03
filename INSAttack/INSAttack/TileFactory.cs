using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace INSAttack
{
    public class TileFactory
    {
        private TDTile m_tdTiles;

        public TDTile TdTiles
        {
            get { return m_tdTiles; }
            set { m_tdTiles = value; }
        }
        private INFOTile m_infoTiles;

        public INFOTile InfoTiles
        {
            get { return m_infoTiles; }
            set { m_infoTiles = value; }
        }
        private AmphiTile m_amphiTiles;

        public AmphiTile AmphiTiles
        {
            get { return m_amphiTiles; }
            set { m_amphiTiles = value; }
        }
        private OutdoorTile m_outdoorTiles;

        public OutdoorTile OutdoorTiles
        {
            get { return m_outdoorTiles; }
            set { m_outdoorTiles = value; }
        }

        public Tile makeTD()
        {
            throw new NotImplementedException();
        }

        public Tile makeAmphi()
        {
            throw new System.NotImplementedException();
        }

        public Tile makeINFO()
        {
            throw new System.NotImplementedException();
        }

        public Tile makeOutdoor()
        {
            throw new System.NotImplementedException();
        }
    }
}
