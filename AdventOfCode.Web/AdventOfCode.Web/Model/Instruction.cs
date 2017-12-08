using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdventOfCode.Web.Model
{
    public class Instruction
    {
        public string Register { get; set; }

        public bool IsIncrease { get; set; }

        public int ChangeValue { get; set; }

        public Condition Condition { get; set; }

        
    }
}