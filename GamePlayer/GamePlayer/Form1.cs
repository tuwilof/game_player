using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace GamePlayer
{
    public partial class Form1 : Form
    {
        string[, ,] map;
        int width;
        int height;
        int level;
        bool flag;
        int pixWidth;
        int pixHeight;
        Position barriers;
        ControlProgram player;
        Code code;
        Maps maps;
        bool allBad = false;
        string[,] arrayPositionsObjects = new string[1000,3];
        int countObject = 0;
        int indexLevel = 0;

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
                string Document = File.ReadAllText(openFileDialog1.FileName);
                var input = new StringReader(Document);
                var deserializer = new Deserializer(namingConvention: new CamelCaseNamingConvention());
                code = deserializer.Deserialize<Code>(input);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.FileName != "openFileDialog1" && openFileDialog2.FileName != "openFileDialog2")
            {
                level = 0;
                flag = true;
            }
            else
            {
                MessageBox.Show("Ошибка", "Отсутствует входная программа", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
                        draw(e.Graphics, i, j, map[i, j, level - 1]);
                    }
                }
            }
        }

        public void drawGrid(Graphics g)
        {
            Pen p = new Pen(Color.White);
            for (int i = 0; i < width * pixWidth + 1; i = i + pixWidth)
            {
                g.DrawLine(p, i, 0, i, height * pixHeight);
            }
            for (int i = 0; i < height * pixHeight + 1; i = i + pixHeight)
            {
                g.DrawLine(p, 0, i, width * pixWidth, i);
            }
        }

        public void draw(Graphics g, int x, int y, string type)
        {
            Image newImage = Image.FromFile(@"..\..\img\null.png"); ;
            Rectangle rect = new Rectangle(1 + x * pixWidth, 1 + y * pixHeight, pixWidth, pixHeight);

            if (type == null)
            {
                newImage = Image.FromFile(@"..\..\img\asphalt\bg.png");
            }
            else if (type == "barrier")
            {
                newImage = Image.FromFile(@"..\..\img\asphalt\barrier.png");
            }
            else 
            {
                newImage = Image.FromFile(@"..\..\img\asphalt\car.png");
            }
            g.DrawImage(newImage, rect);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (flag)
            {
                if (allBad)
                {
                    timer1.Dispose();
                    MessageBox.Show("Игра окончена", "Вы победили", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                    flag = false;
                }
                if (level % 2 == 0)
                {
                    barriers.appearingAndDisappearingBarriers(ref width, ref height, ref map, level, maps, ref indexLevel);
                }
                else
                {
                    player.permutationPlayer(width, height, ref map, level, code, ref allBad, ref arrayPositionsObjects, ref countObject);
                }
                level++;
                pictureBox1.Invalidate();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (openFileDialog2.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string Document = File.ReadAllText(openFileDialog2.FileName);
                var input = new StringReader(Document);
                var deserializer = new Deserializer(namingConvention: new CamelCaseNamingConvention());
                maps = deserializer.Deserialize<Maps>(input);
            }
        }
    }
}
