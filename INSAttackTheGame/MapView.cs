using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using MapDataModel;

namespace INSAttackTheGame
{
    public class MapView : Canvas
    {
        private const float tileWidth = 79;
        private const float tileHeight = 69;

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
            int doubleY = (int)(t.Item2/(tileHeight/2))-1; //coordinate y in half-tiles
            int roughX = (int)((t.Item1-(tileWidth/2))/(.75f*tileWidth)); //rough x value of the intersection near t, not 100% accurate
            //the 2 coords near the intersection
            int x1=roughX;
            int x2=roughX+1;
            int y1 = doubleY/2;
            int y2 = doubleY/2;
            if(doubleY%2 != 0) //not between 2 tiles of the same line
            {
                if(roughX%2==0) //between an upper-left tile and a lower-right tile
                    y2++;
                else //between a lower-left tile and an upper-right tile
                    y1++;
            }
            Coord c1 = new Coord(x1,y1);
            Coord c2 = new Coord(x2,y2);

            //determining which one is closest
            if (distanceSquared(t, toPixels(c1)) > distanceSquared(t, toPixels(c2)))
                return c2;
            else
                return c1;
        }
        private float distanceSquared(Tuple<float, float> t1, Tuple<float, float> t2)
        {
            float dx = t1.Item1-t2.Item1;
            float dy = t1.Item2 - t2.Item2;
            return dx * dx + dy * dy;
        }
    }
}
