using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneratorLevels
{
    public class Maps
    {
        public constructorm constructorm { get; set; }
        public List<mainm> mainm { get; set; }
    }

    public class constructorm
    {
        public int width { get; set; }
        public int height { get; set; }
    }

    public class mainm
    {
        public List<levelm> levelm { get; set; }
    }

    public class levelm
    {
        public positionm positionm { get; set; }
    }

    public class positionm
    {
        public string x { get; set; }
        public string y { get; set; }
    }
}

