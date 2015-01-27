using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamePlayer
{
    class Position
    {
        public void appearingAndDisappearingBarriers(int width, int height, ref string[, ,] map, int level, double ratioBarriers)
        {
            if (level != 0) reseed(width, height, level, ref map);
            placeBarriers(width, height, level, ref map, ratioBarriers);
        }

        private void reseed(int width, int height, int level, ref string[, ,] map)
        {
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    if (map[i, j, level - 1] != null && map[i, j, level - 1] != "barrier")
                        map[i, j, level] = map[i, j, level - 1];
                }
            }
        }

        private void placeBarriers(int width, int height, int level, ref string[, ,] map, double ratioBarriers)
        {
            Random rand = new Random();
            int ratio = (int)(width * height * ratioBarriers);

            int xBarrier;
            int yBarrier;
            for (int i = 0; i < ratio; i++)
            {
                xBarrier = rand.Next(width);
                yBarrier = rand.Next(height);
                while (map[xBarrier, yBarrier, level] != null)
                {
                    xBarrier = rand.Next(width);
                    yBarrier = rand.Next(height);
                }
                map[xBarrier, yBarrier, level] = "barrier";
            }
        }
    }
}
