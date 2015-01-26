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
            foreach (var item in code.Main)
            {
                if (item.Operation == "move")
                    move(ref map, level, item);
            }
        }

        private void change(ref int[, ,] map, int level, Constructor constructor)
        {
            int xAt = constructor.Detailschange.At.X;
            int yAt = constructor.Detailschange.At.Y;
            map[xAt, yAt, level] = 1;
        }

        private void move(ref int[, ,] map, int level, Main main)
        {
            int xFrom = main.Details.From.X;
            int yFrom = main.Details.From.Y;
            int xInto = main.Details.Into.X;
            int yInto = main.Details.Into.Y;
            map[xInto, yInto, level] = map[xFrom, yFrom, level];
            map[xFrom, yFrom, level] = 0;
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
