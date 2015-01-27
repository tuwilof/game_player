using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamePlayer
{
    class ControlProgram
    {
        private bool flagConstructor = true;
        private bool flag = true;
        int[] data = new int[10000];
        bool[] flagData= new bool[10000];


        public void permutationPlayer(int width, int height, ref int[, ,] map, int level, Code code, ref bool allBad)
        {
            reseed(width, height, level, ref map);
            if (flagConstructor)
            {
                executeConstructor(width, height, level, ref map, code);
                flagConstructor = false;
            }
            else
            {
                executeProgram(width, height, level, ref map, code, ref allBad);
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

        private void executeProgram(int width, int height, int level, ref int[, ,] map, Code code, ref bool allBad)
        {
            int x = 0;
            int y = 0;
            int i = 0;
            foreach (var item in code.Main)
            {
                findObject(width, height, level, map, ref x, ref y);
                if (item.Operation == "check" && item.Details[0].To != null)
                    flag = checkTo(ref map, level, item, x, y, i);
                else if (item.Operation == "move" && item.Details[0].From != null && item.Details[0].Into != null)
                {
                    if (flag)
                    {
                        moveFromInto(ref map, level, item);
                        flag = true;
                        allBad = false;
                        break;
                    }
                }
                else if (item.Operation == "move" && item.Details[0].To != null)
                {
                    if (flag)
                    {
                        moveTo(ref map, level, item, x, y);
                        flag = true;
                        allBad = false;
                        break;
                    }
                }
                allBad = true;
                i++;
            }
        }

        private void change(ref int[, ,] map, int level, Constructor constructor)
        {
            int xAt = constructor.Details[0].At.X;
            int yAt = constructor.Details[0].At.Y;
            map[xAt, yAt, level] = 1;
        }

        private bool checkTo(ref int[, ,] map, int level, Main main, int x, int y, int i)
        {
            int shiftX = 0;
            int shiftY = 0;

            if (flagData[i] == false)
            {
                data[i] = main.Details[0].Repeat;
                flagData[i] = true;
            }
            if (data[i] > 0)
            {
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
                data[i]--;

                if (map[x + shiftX, y + shiftY, level] == 0)
                    return true;
                else
                    return false;
            }
            return false;
        }

        private void moveFromInto(ref int[, ,] map, int level, Main main)
        {
            int xFrom = main.Details[0].From.X;
            int yFrom = main.Details[0].From.Y;
            int xInto = main.Details[0].Into.X;
            int yInto = main.Details[0].Into.Y;
            map[xInto, yInto, level] = map[xFrom, yFrom, level];
            map[xFrom, yFrom, level] = 0;
        }

        private void moveTo(ref int[, ,] map, int level, Main main, int x, int y)
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
