using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MapDataModel
{
    public class TileFactory
    {
        public TileFactory()
        {
            m_tdTile = new TDTile();
            m_infoTile = new INFOTile();
            m_amphiTile = new AmphiTile();
            m_outdoorTile = new OutdoorTile();
        }

        private TDTile m_tdTile;

        public TDTile TdTile
        {
            get { return m_tdTile; }
            //set { m_tdTile = value; }
        }
        private INFOTile m_infoTile;

        public INFOTile InfoTile
        {
            get { return m_infoTile; }
            //set { m_infoTile = value; }
        }
        private AmphiTile m_amphiTile;

        public AmphiTile AmphiTile
        {
            get { return m_amphiTile; }
            //set { m_amphiTile = value; }
        }
        private OutdoorTile m_outdoorTile;

        public OutdoorTile OutdoorTile
        {
            get { return m_outdoorTile; }
            //set { m_outdoorTile = value; }
        }
    }
}
