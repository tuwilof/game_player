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

        private void button1_Click(object sender, EventArgs e)
        {
            Random rand = new Random();
            int width = Int32.Parse(textBox1.Text);
            int height = Int32.Parse(textBox2.Text);
            double ratioBarriers = (double)Int32.Parse(textBox3.Text) / 100;
            int ratio = (int)(width * height * ratioBarriers);
            int level = Int32.Parse(textBox4.Text);


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

            StreamWriter sw = new StreamWriter("qwe.yml", false, Encoding.UTF8);
            var serializer = new Serializer();
            serializer.Serialize(sw, maps);
            sw.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Random rand = new Random();
            int width = Int32.Parse(textBox5.Text);
            int height = Int32.Parse(textBox6.Text);
            string name = textBox7.Text;
            int x = Int32.Parse(textBox9.Text);
            int y = Int32.Parse(textBox8.Text);


            Code code = new Code();
            details d;
            main m;
            constructor c;
            at at;
            to to;
            code.constructor = new List<constructor>();
            c = new constructor();
            c.operation = "change";
            c.details = new List<details>();
            d = new details();
            d.obj = name;
            at = new at();
            at.x = "" + x;
            at.y = "" + y;
            d.at = at;
            c.details.Add(d);
            code.constructor.Add(c);


            code.main = new List<main>();
            m = new main();
            m.operation = "check";
            m.details = new List<details>();
            d = new details();
            d.obj = "auto";
            d.state = "empty";
            d.repeat = "10";
            to = new to();
            to.dx = "+1";
            to.dy = "0";
            d.to = to;
            m.details.Add(d);
            code.main.Add(m);

            m = new main();
            m.operation = "move";
            m.details = new List<details>();
            d = new details();
            d.obj = "auto";
            to = new to();
            to.dx = "+1";
            to.dy = "0";
            d.to = to;
            m.details.Add(d);
            code.main.Add(m);



            /*
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
            */
            StreamWriter sw = new StreamWriter("pre.yml", false, Encoding.UTF8);
            var serializer = new Serializer();
            serializer.Serialize(sw, code);
            sw.Close();
        }
    }
}
