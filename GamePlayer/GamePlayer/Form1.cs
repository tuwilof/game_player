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
        int[, ,] map;
        int width;
        int height;
        int level;
        bool flag;
        int pixWidth;
        int pixHeight;
        Position barriers;
        ControlProgram player;
        Code code;

        public Form1()
        {
            InitializeComponent();
            DoubleBuffered = true;
            flag = false;
            barriers = new Position();
            player = new ControlProgram();
            this.Width = 443;
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

        public void draw(Graphics g, int x, int y, int type)
        {
            Image newImage = Image.FromFile(@"..\..\img\null.png"); ;
            Rectangle rect = new Rectangle(1 + x * pixWidth, 1 + y * pixHeight, pixWidth, pixHeight);

            if (type == 0)
            {
                newImage = Image.FromFile(@"..\..\img\asphalt\bg.png");
            }
            else if (type == 1)
            {
                newImage = Image.FromFile(@"..\..\img\asphalt\car.png");
            }
            else if (type == 2)
            {
                newImage = Image.FromFile(@"..\..\img\asphalt\barrier.png");
            }
            g.DrawImage(newImage, rect);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (flag)
            {
                if (barriers.checkGameOver(width, height, level, map))
                {
                    timer1.Dispose();
                    MessageBox.Show("Игра окончена", "Вы победили", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    flag = false;
                }
                if (level % 2 == 0)
                {
                    barriers.appearingAndDisappearingBarriers(width, height, ref map, level, 0.5);
                }
                else
                {
                    player.permutationPlayer(width, height, ref map, level, code);
                }
                level++;
                pictureBox1.Invalidate();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (this.Width != 800) this.Width = 800;
            else this.Width = 443;
        }
    }
}
