using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2018.Day
{
    class Day8
    {
        private static string path = "";
        public Day8(string p)
        {
            path = Path.Combine(p, "InputDay8.txt");
            Console.WriteLine("Advent of Code Day 8 part 1 : " + PartOne());
            //Console.WriteLine("Advent of Code Day 8 part 2 : " + PartTwo());
        }

        public int PartOne()
        {
            var tree = new Dictionary<int, List<string>>();

            var branches = new List<Branch>();
            var lines = File.ReadAllText(path).Split(' ').Select(x => Convert.ToInt32(x)).ToList();
            var metaIndex = -1;
            var depth = 0;

            bool haveTree = false;
            while (!haveTree)
            {
                var children = lines.First();
                var metaEntries = lines.ElementAt(1);

                var branch = new Branch(children, metaEntries, depth);
                depth++;
            }



            return -1;
        }

        public int recursiveSum(int children, List<int> data)
        {
            var sum = 0;

            var child = data.First();
            var metaEntries = data.ElementAt(1);

            //var branch = new Branch(children, metaEntries, depth);

            for (int i = 0; i < children; i++)
            {
                sum += recursiveSum(0, null);
            }

            return sum;
        }
    }

    class Branch
    {
        public int Children { get; set; }
        public int MetaEntries { get; set; }
        public int Depth { get; set; }
        public bool IsEndNode { get; set; }

        public Branch(int children, int metaEntries, int depth)
        {
            Children = children;
            MetaEntries = metaEntries;
            Depth = depth;

            if (Children == 0)
                IsEndNode = true;
            else
                IsEndNode = false;
        }
    }
}
