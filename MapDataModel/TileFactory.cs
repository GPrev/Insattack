using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MapDataModel
{
    public class TileFactory
    {
        protected TileFactory()
        {
            m_tdTile = new TDTile();
            m_infoTile = new INFOTile();
            m_amphiTile = new AmphiTile();
            m_restaurantTile = new RestaurantTile();
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

        private RestaurantTile m_restaurantTile;

        public RestaurantTile RestaurantTile
        {
            get { return m_restaurantTile; }
            //set { m_outdoorTile = value; }
        }

        private static  TileFactory m_instance;

        public static TileFactory Instance
        {
            get { if(m_instance == null) m_instance = new TileFactory(); return m_instance; }
            //set { m_instance = value; }
        }

        public Tile getTile(int id) //would be better using a hashmap
        {
            switch(id)
            {
                case 1:
                    return AmphiTile;
                case 2:
                    return TdTile;
                case 3:
                    return InfoTile;
                case 4:
                    return RestaurantTile;
                default: //0
                    return OutdoorTile;
            }
        }
    }
}
