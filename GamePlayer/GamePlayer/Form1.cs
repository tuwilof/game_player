using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GamePlayer
{
    public partial class Form1 : Form
    {
        int[, ,] map;
        int width;
        int height;
        int level;
        bool flag;
        int pixWidth;
        int pixHeight;
        Position barriers;
        ControlProgram player;
        string str;

        public Form1()
        {
            InitializeComponent();
            DoubleBuffered = true;
            flag = false;
            barriers = new Position();
            player = new ControlProgram();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                System.IO.StreamReader sr = new System.IO.StreamReader(openFileDialog1.FileName);
                str = sr.ReadToEnd();
                sr.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            width = Int32.Parse(textBox1.Text);
            height = Int32.Parse(textBox2.Text);
            map = new int[width, height, 10000];
            level = 0;
            flag = true;
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            if (flag)
            {
                pixWidth = (int)(399 / width);
                pixHeight = (int)(399 / height);
                drawGrid(e.Graphics);
                for (int i = 0; i < width; i++)
                {
                    for (int j = 0; j < height; j++)
                    {
                        draw(e.Graphics, i, j, map[i, j, level]);
                    }
                }
            }
        }

        public void drawGrid(Graphics g)
        {
            Pen p = new Pen(Color.Black);
            for (int i = 0; i < width * pixWidth + 1; i = i + pixWidth)
            {
                g.DrawLine(p, i, 0, i, height * pixHeight);
            }
            for (int i = 0; i < height * pixHeight + 1; i = i + pixHeight)
            {
                g.DrawLine(p, 0, i, width * pixWidth, i);
            }
        }

        public void draw(Graphics g, int x, int y, int type)
        {
            SolidBrush b = new SolidBrush(Color.White);
            if (type == 0)
                b = new SolidBrush(Color.Yellow);
            else if (type == 1)
                b = new SolidBrush(Color.Green);
            else if (type == 2)
                b = new SolidBrush(Color.Red);
            g.FillPolygon(b, new PointF[] { 
                new PointF(1 + x * pixWidth, 1 + y * pixHeight), 
                new PointF(1 + x * pixWidth, pixHeight + y * pixHeight), 
                new PointF(pixWidth + x * pixWidth, pixHeight + y * pixHeight), 
                new PointF(pixWidth + x * pixWidth, 1 + y * pixHeight) 
            });
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (flag)
            {
                if (level % 2 == 0)
                {
                    barriers.appearingAndDisappearingBarriers(width, height, ref map, level);
                }
                else
                {
                    player.permutationPlayer(width, height, ref map, str);
                    level++;
                }
                pictureBox1.Invalidate();
            }
        }
    }

    class ControlProgram
    {
        public void permutationPlayer(int width, int height, ref int[, ,] map, string str)
        {

        }
    }

    class Position
    {
        public void appearingAndDisappearingBarriers(int width, int height, ref int[, ,] map, int level)
        {
            int x = 0;
            int y = 0;

            clearMapAndFindPlayer(width, height, level, ref x, ref y, ref map);
            placeBarriers(width, height, level, x, y, ref map);
        }

        private void clearMapAndFindPlayer(int width, int height, int level, ref int x, ref int y, ref int[, ,] map)
        {
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    if (map[i, j, level] == 1)
                    {
                        x = i;
                        y = j;
                        map[i, j, level] = 0;
                    }
                    else
                    {
                        map[i, j, level] = 0;
                    }
                }
            }
        }

        private void placeBarriers(int width, int height, int level, int xPlayer, int yPlayer, ref int[, ,] map)
        {
            Random rand = new Random();
            int ratio = (int)(width * height * 0.2);

            int xBarrier;
            int yBarrier;
            for (int i = 0; i < ratio; i++)
            {
                xBarrier = rand.Next(width);
                yBarrier = rand.Next(height);
                while ((xBarrier == xPlayer && yBarrier == yPlayer) || (map[xBarrier, yBarrier, level] == 2))
                {
                    xBarrier = rand.Next(width);
                    yBarrier = rand.Next(height);
                }
                map[xBarrier, yBarrier, level] = 2;
            }
        }
    }
}
