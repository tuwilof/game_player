using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneratorLevels
{
    public class Code
    {
        public List<constructor> constructor { get; set; }
        public List<main> main { get; set; }
    }

    public class constructor
    {
        public string operation { get; set; }
        public List<details> details { get; set; }
    }

    public class at
    {
        public string x { get; set; }
        public string y { get; set; }
    }

    public class main
    {
        public string operation { get; set; }
        public List<details> details { get; set; }
    }

    public class details
    {
        public string obj { get; set; }
        public from from { get; set; }
        public into into { get; set; }
        public at at { get; set; }
        public to to { get; set; }
        public string state { get; set; }
        public string repeat { get; set; }
    }

    public class from
    {
        public string x { get; set; }
        public string y { get; set; }
    }
    public class into
    {
        public string x { get; set; }
        public string y { get; set; }
    }

    public class to
    {
        public string dx { get; set; }
        public string dy { get; set; }
    }
}