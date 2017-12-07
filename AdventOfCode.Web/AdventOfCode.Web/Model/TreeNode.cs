using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdventOfCode.Web.Model
{
    public class TreeNode
    {
        public TreeNode(string name, int weight)
        {
            this.Name = name;
            this.Weight = weight;
        }

        public string Name { get; set; }

        public int Weight { get; set; }

        public string[] Children {get; set;}
    }
}