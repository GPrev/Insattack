using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using MapDataModel;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Media.Imaging;
using System.IO;

namespace INSAttackTheGame
{
    public class MapView : Canvas
    {
        private const float tileWidth = 79;
        private const float tileHeight = 69;

        private Dictionary<Tile, ImageSource> m_tileImages;

        public void init()
        {
            loadImages();
            InvalidateVisual();
        }

        private Tuple<float, float> toPixels(Coord c)
        {
            float x = .5f + .75f * c.X; //.5 = x of the center of the tile 0,0.  Each tile is .75(tileWidth) pixels more to the right ( .5*(1+sin(pi/6))=.75 )
            float y = 1 + c.Y;  //1 = y of the center of the tile 0,0. Each tile is (tileHeight) pixels lower
            if (c.X % 2 == 1) //"odd" tiles are .5(tileHeight) more up (see reference picture)
                y -= .5f;
            return new Tuple<float, float>(x * tileWidth, y * tileHeight);
        }
        private Coord toCoords(Tuple<float, float> t)
        {
            int doubleY = (int)(t.Item2 / (tileHeight / 2)) - 1; //coordinate y in half-tiles
            int roughX = (int)((t.Item1 - (tileWidth / 2)) / (.75f * tileWidth)); //rough x value of the intersection near t, not 100% accurate
            //the 2 coords near the intersection
            int x1 = roughX;
            int x2 = roughX + 1;
            int y1 = doubleY / 2;
            int y2 = doubleY / 2;
            if (doubleY % 2 != 0) //not between 2 tiles of the same line
            {
                if (roughX % 2 == 0) //between an upper-left tile and a lower-right tile
                    y2++;
                else //between a lower-left tile and an upper-right tile
                    y1++;
            }
            Coord c1 = new Coord(x1, y1);
            Coord c2 = new Coord(x2, y2);

            //determining which one is closest
            if (distanceSquared(t, toPixels(c1)) > distanceSquared(t, toPixels(c2)))
                return c2;
            else
                return c1;
        }
        private float distanceSquared(Tuple<float, float> t1, Tuple<float, float> t2)
        {
            float dx = t1.Item1 - t2.Item1;
            float dy = t1.Item2 - t2.Item2;
            return dx * dx + dy * dy;
        }
        private bool loadImages()
        {
            m_tileImages = new Dictionary<Tile, ImageSource>();
            try
            {
                m_tileImages.Add(TileFactory.Instance.OutdoorTile, BitmapFrame.Create(new Uri(@"pack://application:,,/Resources/Terrain/Outdoor.png")));
                m_tileImages.Add(TileFactory.Instance.AmphiTile, BitmapFrame.Create(new Uri(@"pack://application:,,/Resources/Terrain/Amphi.png")));
                m_tileImages.Add(TileFactory.Instance.TdTile, BitmapFrame.Create(new Uri(@"pack://application:,,/Resources/Terrain/TD.png")));
                m_tileImages.Add(TileFactory.Instance.InfoTile, BitmapFrame.Create(new Uri(@"pack://application:,,/Resources/Terrain/INFO.png")));
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return false;
            }
            return true;
        }
        private void DrawElementOnCanvas(Tile t, Coord pos, DrawingContext dc)
        {
            Tuple<float, float> realPos = toPixels(pos);
            dc.DrawImage(m_tileImages[t], new Rect(realPos.Item1 - tileWidth / 2, realPos.Item2 - tileHeight / 2, tileWidth, tileHeight));
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);
            if(m_tileImages != null) //if initialized correctly
            {
                DrawElementOnCanvas(TileFactory.Instance.OutdoorTile, new Coord(0, 0), drawingContext);
                DrawElementOnCanvas(TileFactory.Instance.AmphiTile, new Coord(1, 0), drawingContext);
                DrawElementOnCanvas(TileFactory.Instance.TdTile, new Coord(0, 1), drawingContext);
                DrawElementOnCanvas(TileFactory.Instance.InfoTile, new Coord(1, 1), drawingContext);
            }
        }
    }
}
