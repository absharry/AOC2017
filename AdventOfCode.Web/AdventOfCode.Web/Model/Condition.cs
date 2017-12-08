using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdventOfCode.Web.Model
{
    public class Condition
    {
        public string Register { get; set; }

        public string Operator { get; set; }

        public int Value { get; set; }
    }
}