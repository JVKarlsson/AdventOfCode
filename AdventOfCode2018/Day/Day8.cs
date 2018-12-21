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
        private List<Branch> branches = new List<Branch>();
        public Day8(string p)
        {
            path = Path.Combine(p, "InputDay8.txt");
            Solve();
        }

        public void Solve()
        {
            var lines = File.ReadAllText(path).Split(' ').Select(x => Convert.ToInt32(x)).ToList();

            var part1 = recursiveSum(lines);
            var totalSum = 0;
            foreach (var leaf in branches)
            {
                totalSum += leaf.MetaDataSum;
            }

            lines = File.ReadAllText(path).Split(' ').Select(x => Convert.ToInt32(x)).ToList();
            var part2 = recursiveSum2(lines);

            Console.WriteLine("Advent of Code Day 8 part 1 : " + totalSum);
            Console.WriteLine("Advent of Code Day 8 part 2 : " + part2.MetaDataSum);
        }


        public int recursiveSum(List<int> data)
        {
            var childCount = data[0];
            data.RemoveAt(0);
            var metaEntries = data[0];
            data.RemoveAt(0);

            var branch = new Branch(metaEntries);
            var count = 0;
            if (childCount > 0)
            {
                for (int i = 0; i < childCount; i++)
                {
                    count += recursiveSum(data);
                }
            }
            for (int i = 0; i < metaEntries; i++)
            {
                branch.MetaDataSum += data.ElementAt(0);
                data.RemoveAt(0);
            }

            branches.Add(branch);
            return count;
        }
        public Branch recursiveSum2(List<int> data)
        {
            var childCount = data[0];
            data.RemoveAt(0);
            var metaEntries = data[0];
            data.RemoveAt(0);
            var branch = new Branch(metaEntries);

            if (childCount > 0)
            {
                for (int i = 0; i < childCount; i++)
                {
                    branch.Children.Add(recursiveSum2(data));
                }
                for (int i = 0; i < metaEntries; i++)
                {
                    if (branch.Children.Count >= data.ElementAt(0))
                    {
                        var child = branch.Children[data.ElementAt(0) - 1];
                        branch.MetaDataSum += child.MetaDataSum;
                    }
                    data.RemoveAt(0);
                }
            }
            else
            {
                for (int i = 0; i < metaEntries; i++)
                {
                    branch.MetaDataSum += data.ElementAt(0);
                    data.RemoveAt(0);
                }
            }
            return branch;
        }
        
        public class Branch
        {
            public List<Branch> Children { get; set; }
            public int MetaEntries { get; set; }
            public int MetaDataSum { get; set; }

            public Branch(int metaEntries)
            {
                Children = new List<Branch>();
                MetaEntries = metaEntries;
                MetaDataSum = 0;
            }
        }
    }
}





