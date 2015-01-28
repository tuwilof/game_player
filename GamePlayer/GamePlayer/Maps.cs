using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamePlayer
{

    public class Maps
    {
        public Constructorm Constructorm { get; set; }
        public List<Mainm> Mainm { get; set; }
    }

    public class Constructorm
    {
        public int width { get; set; }
        public int height { get; set; }
    }

    public class Mainm
    {
        public List<Levelm> Levelm { get; set; }
    }

    public class Levelm
    {
        public Positionm Positionm { get; set; }
    }

    public class Positionm
    {
        public int X { get; set; }
        public int Y { get; set; }
    }
}

