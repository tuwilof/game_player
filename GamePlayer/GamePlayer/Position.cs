using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamePlayer
{
    class Position
    {
        bool flag = true;

        public void appearingAndDisappearingBarriers(ref int width, ref int height, ref string[, ,] map, int level, Maps maps, ref int indexLevel)
        {
            if (flag)
            {
                width = maps.Constructorm.width;
                height = maps.Constructorm.height;
                map = new string[width, height, 10000];
                flag = false;
            }

            if (level != 0) reseed(width, height, level, ref map);
            placeBarriers(width, height, level, ref map, maps, ref indexLevel);
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

        private void placeBarriers(int width, int height, int level, ref string[, ,] map, Maps maps, ref int indexLevel)
        {
            foreach (var item in maps.Mainm[indexLevel].Levelm)
            {
                if (map[item.Positionm.X, item.Positionm.Y, level] == null)
                    map[item.Positionm.X, item.Positionm.Y, level] = "barrier";
            }
            indexLevel++;
            if (indexLevel == maps.Mainm.Count)
                indexLevel = 0;

        }
    }
}
