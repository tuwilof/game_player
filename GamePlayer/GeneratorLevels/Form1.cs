using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using YamlDotNet.Serialization;

namespace GeneratorLevels
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void generationBarriers(int level, int ratio, int width, int height)
        {
            Random rand = new Random();

            Maps maps = new Maps();
            maps.constructorm = new constructorm();
            maps.constructorm.width = 10;
            maps.constructorm.height = 10;

            maps.mainm = new List<mainm>();
            mainm m;
            List<levelm> ll;
            levelm l;
            positionm p;

            for (int j = 0; j < level; j++)
            {
                m = new mainm();
                ll = new List<levelm>();
                for (int i = 0; i < ratio; i++)
                {
                    l = new levelm();
                    l.positionm = new positionm() { x = "" + rand.Next(width), y = "" + rand.Next(height) };
                    if (ll.IndexOf(l) == -1)
                        ll.Add(l);
                }
                m.levelm = ll;
                maps.mainm.Add(m);
            }

            StreamWriter sw = new StreamWriter("prepyatstvia.yml", false, Encoding.UTF8);
            var serializer = new Serializer();
            serializer.Serialize(sw, maps);
            sw.Close();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            int width = Int32.Parse(textBox1.Text);
            int height = Int32.Parse(textBox2.Text);
            double ratioBarriers = (double)Int32.Parse(textBox3.Text) / 100;
            int ratio = (int)(width * height * ratioBarriers);
            int level = Int32.Parse(textBox4.Text);

            generationBarriers(level, ratio, width, height);
        }

        public void wayGeneration(ref Code code, int a1, int a2, int b1, int b2)
        {
            details d;
            main m;
            to to;

            m = new main();
            m.operation = "check";
            m.details = new List<details>();
            d = new details();
            d.obj = "auto";
            d.state = "empty";
            d.repeat = "" + (Math.Abs(a1 - a2) + Math.Abs(b1 - b2));
            to = new to();
            to.dx = "0";
            to.dy = "0";
            if (a1 - a2 < 0)
            {
                to.dx = "-1";
            }
            if (b1 - b2 < 0)
            {
                to.dy = "-1";
            }
            if (a1 - a2 > 0)
            {
                to.dx = "+1";
            }
            if (b1 - b2 > 0)
            {
                to.dy = "+1";
            }
            d.to = to;
            m.details.Add(d);
            code.main.Add(m);

            m = new main();
            m.operation = "move";
            m.details = new List<details>();
            d = new details();
            d.obj = "auto";
            to = new to();
            to.dx = "0";
            to.dy = "0";
            if (a1 - a2 < 0)
            {
                to.dx = "-1";
            }
            if (b1 - b2 < 0)
            {
                to.dy = "-1";
            }
            if (a1 - a2 > 0)
            {
                to.dx = "+1";
            }
            if (b1 - b2 > 0)
            {
                to.dy = "+1";
            }
            d.to = to;
            m.details.Add(d);
            code.main.Add(m);
        }


        private void button2_Click(object sender, EventArgs e)
        {
            Random rand = new Random();
            int width = Int32.Parse(textBox5.Text);
            int height = Int32.Parse(textBox6.Text);
            string name = textBox7.Text;
            int x = Int32.Parse(textBox9.Text);
            int y = Int32.Parse(textBox8.Text);


            int w1 = Int32.Parse(textBox10.Text);
            int h1 = Int32.Parse(textBox11.Text);

            int w2 = Int32.Parse(textBox12.Text);
            int h2 = Int32.Parse(textBox13.Text);

            int w3 = Int32.Parse(textBox14.Text);
            int h3 = Int32.Parse(textBox15.Text);

            Code code = new Code();
            details d;
            constructor c;
            at at;

            code.constructor = new List<constructor>();
            c = new constructor();
            c.operation = "change";
            c.details = new List<details>();
            d = new details();
            code.main = new List<main>();

            d.obj = name;
            at = new at();
            at.x = "" + x;
            at.y = "" + y;
            d.at = at;
            c.details.Add(d);
            code.constructor.Add(c);

            wayGeneration(ref code, w1, x, h1, y);
            wayGeneration(ref code, w2, w1, h2, h1);
            wayGeneration(ref code, w3, w2, h3, h2);

            StreamWriter sw = new StreamWriter("objects.yml", false, Encoding.UTF8);
            var serializer = new Serializer();
            serializer.Serialize(sw, code);
            sw.Close();
        }
    }
}
