using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamePlayer
{
    class ControlProgram
    {
        private bool flag = true;

        public void permutationPlayer(int width, int height, ref int[, ,] map, int level, Code code)
        {
            reseed(width, height, level, ref map);
            if (flag)
            {
                executeConstructor(width, height, level, ref map, code);
                flag = false;
            }
            else
            {
                executeProgram(width, height, level, ref map, code);
            }
        }

        private void executeConstructor(int width, int height, int level, ref int[, ,] map, Code code)
        {
            foreach (var item in code.Constructor)
            {
                if (item.Operation == "change")
                    change(ref map, level, item);
            }
        }

        private void executeProgram(int width, int height, int level, ref int[, ,] map, Code code)
        {
            int x = 0;
            int y = 0;
            foreach (var item in code.Main)
            {
                findObject(width, height, level, map, ref x, ref y);
                if (item.Operation == "move1")
                    move1(ref map, level, item);
                else if (item.Operation == "move2")
                    move2(ref map, level, item, x, y);
            }
        }

        private void change(ref int[, ,] map, int level, Constructor constructor)
        {
            int xAt = constructor.Details[0].At.X;
            int yAt = constructor.Details[0].At.Y;
            map[xAt, yAt, level] = 1;
        }

        private void move1(ref int[, ,] map, int level, Main main)
        {
            int xFrom = main.Details[0].From.X;
            int yFrom = main.Details[0].From.Y;
            int xInto = main.Details[0].Into.X;
            int yInto = main.Details[0].Into.Y;
            map[xInto, yInto, level] = map[xFrom, yFrom, level];
            map[xFrom, yFrom, level] = 0;
        }

        private void move2(ref int[, ,] map, int level, Main main, int x, int y)
        {
            int shiftX = 0;
            int shiftY = 0;
            if (main.Details[0].To.Dx[0] == '+')
            {
                shiftX += Int32.Parse("" + main.Details[0].To.Dx[1]);  
            }
            else if (main.Details[0].To.Dx[0] == '-')
            {
                shiftX -= Int32.Parse("" + main.Details[0].To.Dx[1]);
            }
            if (main.Details[0].To.Dy[0] == '+')
            {
                shiftY += Int32.Parse("" + main.Details[0].To.Dy[1]);
            }
            else if (main.Details[0].To.Dy[0] == '-')
            {
                shiftY -= Int32.Parse("" + main.Details[0].To.Dy[1]);
            }

            map[x + shiftX, y + shiftY, level] = map[x, y, level];
            map[x, y, level] = 0;
        }

        private void findObject(int width, int height, int level, int[, ,] map, ref int x, ref int y)
        {
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    if (map[i, j, level] == 1)
                    {
                        x = i;
                        y = j;
                        return;
                    }
                }
            }
        }

        private void reseed(int width, int height, int level, ref int[, ,] map)
        {
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    map[i, j, level] = map[i, j, level - 1];
                }
            }
        }
    }
}
