﻿using System.Collections.Generic;
namespace GamePlayer
{
    public class Code
    {
        public List<Constructor> Constructor { get; set; }
        public List<Main> Main { get; set; }
    }

    public class Constructor
    {
        public string Operation { get; set; }
        public Detailschange Detailschange { get; set; }
    }

    public class Detailschange
    {
        public string Object { get; set; }
        public At At { get; set; }
    }

    public class At
    {
        public int X { get; set; }
        public int Y { get; set; }
    }

    public class Main
    {
        public string Operation { get; set; }
        public Details Details { get; set; }
    }

    public class Details
    {
        public string Object { get; set; }
        public From From { get; set; }
        public Into Into { get; set; }
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
}