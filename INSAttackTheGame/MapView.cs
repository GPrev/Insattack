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
using System.Windows.Input;
using System.IO;

namespace INSAttackTheGame
{
    public class MapView : Canvas
    {
        private const float tileWidth = 79;
        private const float tileHeight = 69;

        private Dictionary<Tile, ImageSource> m_tileImages;
        ImageSource m_cursorImage;

        MapData m_map;
        Coord m_cursorPos;

        public Coord CursorPos
        {
            get { return m_cursorPos; }
            set
            {
                if (m_map.isValid(value))
                    m_cursorPos = value;
                else
                    unselect();
            }
        }

        public void init(MapData map)
        {
            m_map = map;
            m_cursorPos = Coord.nowhere;
            loadImages();
            InvalidateMeasure();
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
        private Coord toCoords(float x, float y)
        {
            return toCoords(new Tuple<float, float>(x, y));
        }
        private Coord toCoords(Tuple<float, float> t)
        {
            int doubleY = (int)(t.Item2 / (tileHeight / 2)) - 1; //coordinate y in half-tiles
            int roughX = (int)((t.Item1 - (tileWidth / 2)) / (.75f * tileWidth)); //rough x value of the intersection near t, not 100% accurate
            //the 2 coords near the intersection
            int x1 = roughX;
            int x2 = roughX + 1;
            int y1 = (doubleY+2) / 2 -1; // the +2 and -1 are used to counteract the fact that negative values are rounded up and not down when cast to int (and -1 is the only negative value that matters)
            int y2 = (doubleY + 2) / 2 - 1; //same as above
            if ((doubleY+2) % 2 != 0) //not between 2 tiles of the same line. +2 deals with negative values (-1 is the only one that matters)
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

                m_cursorImage = BitmapFrame.Create(new Uri(@"pack://application:,,/Resources/UI/Cursor.png"));
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
            DrawElementOnCanvas(m_tileImages[t], pos, dc);
        }
        private void DrawElementOnCanvas(ImageSource i, Coord pos, DrawingContext dc)
        {
            Tuple<float, float> realPos = toPixels(pos);
            dc.DrawImage(i, new Rect(realPos.Item1 - tileWidth / 2, realPos.Item2 - tileHeight / 2, tileWidth, tileHeight));
        }

        protected override void OnRender(DrawingContext drawingContext) //Draws the terrain on the canvas
        {
            base.OnRender(drawingContext);
            if(m_tileImages != null) //if initialized correctly
            {
                foreach(var t in m_map.TileTable)
                {
                    DrawElementOnCanvas(t.Value, t.Key, drawingContext); //draws each tile
                }
                if(m_map.isValid(m_cursorPos))
                    DrawElementOnCanvas(m_cursorImage, m_cursorPos, drawingContext); //draws the cursor
            }
        }

        protected override System.Windows.Size MeasureOverride(System.Windows.Size constraint) //Returns the measure of the canvas
        {
            base.MeasureOverride(constraint);
            if(m_map == null) //if the map hasn't been initialized yet
                return new System.Windows.Size(0,0);
            //else
            Tuple<float, float> lastPos = toPixels(new Coord(m_map.Size, m_map.Size)); //position of the last tile
            return new Size(lastPos.Item1, lastPos.Item1 + tileHeight * 1.5); //rough estimation of the size, always equal or slightly higher
        }
        
        public void onClick(object sender, MouseButtonEventArgs e) //Called by the main window if clicked
        {
            CursorPos = toCoords((float)e.GetPosition(this).X, (float)e.GetPosition(this).Y); //updates the cursor position
            InvalidateVisual(); //refreshes the display
        }

        public void unselect() //unselects selected tile if any
        {
            m_cursorPos = Coord.nowhere;
        }
    }
}
