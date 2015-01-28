using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneratorLevels
{
    public class Code
    {
        public List<Constructor> Constructor { get; set; }
        public List<Main> Main { get; set; }
    }

    public class Constructor
    {
        public string Operation { get; set; }
        public List<Details> Details { get; set; }
    }

    public class At
    {
        public int X { get; set; }
        public int Y { get; set; }
    }

    public class Main
    {
        public string Operation { get; set; }
        public List<Details> Details { get; set; }
    }

    public class Details
    {
        public string Object { get; set; }
        public From From { get; set; }
        public Into Into { get; set; }
        public At At { get; set; }
        public To To { get; set; }
        public string State { get; set; }
        public int Repeat { get; set; }
    }

    public class From
    {
        public int X { get; set; }
        public int Y { get; set; }
    }
    public class Into
    {
        public int X { get; set; }
        public int Y { get; set; }
    }

    public class To
    {
        public string Dx { get; set; }
        public string Dy { get; set; }
    }
}