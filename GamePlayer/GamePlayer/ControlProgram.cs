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
        bool[] flagData = new bool[10000];


        public void permutationPlayer(int width, int height, ref string[, ,] map, int level, Code code, ref bool allBad, ref string[,] arrayPositionsObjects, ref int countObject)
        {
            reseed(width, height, level, ref map);
            if (flagConstructor)
            {
                executeConstructor(width, height, level, ref map, code, ref arrayPositionsObjects, ref countObject);
                flagConstructor = false;
            }
            else
            {
                executeProgram(width, height, level, ref map, code, ref allBad, ref arrayPositionsObjects, ref countObject);
            }
        }

        private void executeConstructor(int width, int height, int level, ref string[, ,] map, Code code, ref string[,] arrayPositionsObjects, ref int countObject)
        {
            foreach (var item in code.Constructor)
            {
                if (item.Operation == "change")
                    change(ref map, level, item, ref arrayPositionsObjects, ref countObject);
            }
        }

        private void executeProgram(int width, int height, int level, ref string[, ,] map, Code code, ref bool allBad, ref string[,] arrayPositionsObjects, ref int countObject)
        {
            int index = 0;
            int i = 0;
            foreach (var item in code.Main)
            {
                if (item.Operation == "check" && item.Details[0].To != null)
                {
                    findObject(width, height, level, map, ref index, ref arrayPositionsObjects, ref countObject, item.Details[0].Object);
                    flag = checkTo(ref map, level, item, index, i, ref arrayPositionsObjects, ref countObject);
                }
                else if (item.Operation == "move" && item.Details[0].From != null && item.Details[0].Into != null)
                {
                    if (flag)
                    {
                        moveFromInto(ref map, level, item, ref arrayPositionsObjects, ref countObject, index);
                        flag = true;
                        allBad = false;
                        break;
                    }
                }
                else if (item.Operation == "move" && item.Details[0].To != null)
                {
                    if (flag)
                    {
                        findObject(width, height, level, map, ref index, ref arrayPositionsObjects, ref countObject, item.Details[0].Object);
                        moveTo(ref map, level, item, index, ref arrayPositionsObjects, ref countObject);
                        flag = true;
                        allBad = false;
                        break;
                    }
                }
                allBad = true;
                i++;
            }
        }

        private void change(ref string[, ,] map, int level, Constructor constructor, ref string[,] arrayPositionsObjects, ref int countObject)
        {
            int xAt = constructor.Details[0].At.X;
            int yAt = constructor.Details[0].At.Y;
            map[xAt, yAt, level] = constructor.Details[0].Object;
            arrayPositionsObjects[countObject, 0] = constructor.Details[0].Object;
            arrayPositionsObjects[countObject, 1] = "" + constructor.Details[0].At.X;
            arrayPositionsObjects[countObject, 2] = "" + constructor.Details[0].At.Y;
            countObject++;
        }

        private bool checkTo(ref string[, ,] map, int level, Main main, int index, int i, ref string[,] arrayPositionsObjects, ref int countObject)
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

                if (map[Int32.Parse(arrayPositionsObjects[index, 1]) + shiftX, Int32.Parse(arrayPositionsObjects[index, 2]) + shiftY, level] == null)
                    return true;
                else
                    return false;
            }
            return false;
        }

        private void moveFromInto(ref string[, ,] map, int level, Main main, ref string[,] arrayPositionsObjects, ref int countObject, int index)
        {
            int xFrom = main.Details[0].From.X;
            int yFrom = main.Details[0].From.Y;
            int xInto = main.Details[0].Into.X;
            int yInto = main.Details[0].Into.Y;
            map[xInto, yInto, level] = map[xFrom, yFrom, level];
            map[xFrom, yFrom, level] = null;
            arrayPositionsObjects[index, 1] = "" + xFrom;
            arrayPositionsObjects[index, 2] = "" + yFrom;
        }

        private void moveTo(ref string[, ,] map, int level, Main main, int index, ref string[,] arrayPositionsObjects, ref int countObject)
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


            map[Int32.Parse(arrayPositionsObjects[index, 1]) + shiftX, Int32.Parse(arrayPositionsObjects[index, 2]) + shiftY, level] = map[Int32.Parse(arrayPositionsObjects[index, 1]), Int32.Parse(arrayPositionsObjects[index, 2]), level];
            map[Int32.Parse(arrayPositionsObjects[index, 1]), Int32.Parse(arrayPositionsObjects[index, 2]), level] = null;
            arrayPositionsObjects[index, 1] = "" + (Int32.Parse(arrayPositionsObjects[index, 1]) + shiftX);
            arrayPositionsObjects[index, 2] = "" + (Int32.Parse(arrayPositionsObjects[index, 2]) + shiftY);
        }

        private void findObject(int width, int height, int level, string[, ,] map, ref int index, ref string[,] arrayPositionsObjects, ref int countObject, string nameObject)
        {
            for (int i = 0; i < countObject; i++)
            {
                    if (arrayPositionsObjects[i, 0] == nameObject)
                    {
                        index = i;
                        return;
                    }
            }
        }

        private void reseed(int width, int height, int level, ref string[, ,] map)
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
