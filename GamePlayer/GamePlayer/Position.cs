using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamePlayer
{
    class Position
    {
        public void appearingAndDisappearingBarriers(int width, int height, ref int[, ,] map, int level, double ratioBarriers)
        {
            if (level != 0) reseed(width, height, level, ref map);
            placeBarriers(width, height, level, ref map, ratioBarriers);
        }

        private void reseed(int width, int height, int level, ref int[, ,] map)
        {
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    if (map[i, j, level - 1] == 1)
                        map[i, j, level] = 1;
                }
            }
        }

        private void placeBarriers(int width, int height, int level, ref int[, ,] map, double ratioBarriers)
        {
            Random rand = new Random();
            int ratio = (int)(width * height * ratioBarriers);

            int xBarrier;
            int yBarrier;
            for (int i = 0; i < ratio; i++)
            {
                xBarrier = rand.Next(width);
                yBarrier = rand.Next(height);
                while ((map[xBarrier, yBarrier, level] == 1) || (map[xBarrier, yBarrier, level] == 2))
                {
                    xBarrier = rand.Next(width);
                    yBarrier = rand.Next(height);
                }
                map[xBarrier, yBarrier, level] = 2;
            }
        }

        public bool checkGameOver(int width, int height, int level, int[, ,] map)
        {
            int x = 0;
            int y = 0;
            if (level != 0)
            {
                for (int i = 0; i < width; i++)
                {
                    for (int j = 0; j < height; j++)
                    {
                        if (map[i, j, level - 1] == 1)
                        {
                            x = i;
                            y = j;
                        }
                    }
                }
            }
            if (x == width - 1)
                return true;
            else
                return false;
        }
    }
}
