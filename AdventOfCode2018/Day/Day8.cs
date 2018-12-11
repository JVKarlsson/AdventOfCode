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
            var tree = new Dictionary<int, List<string>>();
            var lines = File.ReadAllText(path).Split(' ').Select(x => Convert.ToInt32(x)).ToList();
            
            var part1 = recursiveSum(lines);
            var totalSum = 0;
            foreach (var leaf in branches)
            {
                totalSum += leaf.MetaDataSum;
            }

            branches.Clear();
            var part2 = recursiveSum2(lines);
            var totalSumPart2 = 0;
            foreach (var leaf in branches)
            {
                totalSumPart2 += leaf.MetaDataSum;
            }

            Console.WriteLine("Advent of Code Day 8 part 1 : " + totalSum);
            Console.WriteLine("Advent of Code Day 8 part 2 : " + totalSumPart2);
        }

        public int recursiveSum(List<int> data)
        {
            var steps = 0;
            var childCount = data.ElementAt(steps);
            var metaEntries = data.ElementAt(steps + 1);
            steps += 2;
            var branch = new Branch(childCount, metaEntries);

            if (childCount > 0)
            {
                for (int i = 0; i < childCount; i++)
                {
                    steps += recursiveSum(data.GetRange(steps, (data.Count - (steps + 1))));
                }
            }

            for (int i = steps; i < (steps + metaEntries); i++)
            {
                branch.MetaDataSum += data.ElementAt(i);
            }
            steps += metaEntries;

            branches.Add(branch);
            return steps;
        }

        public int[] recursiveSum2(List<int> data)
        {
            var steps = 0;
            var childCount = data.ElementAt(steps);
            var metaEntries = data.ElementAt(steps + 1);
            steps += 2;
            var branch = new Branch(childCount, metaEntries);

            if (childCount > 0)
            {
                var dataList = new List<int[]>();
                for (int i = 0; i < childCount; i++)
                {
                    var result = recursiveSum2(data.GetRange(steps, (data.Count - (steps + 1))));
                    dataList.Add(result);
                    steps += result[0];
                }
                for (int i = steps; i < (steps + metaEntries); i++)
                {
                    var index = data.ElementAt(i);
                    if (index < childCount)
                    {
                        branch.MetaDataSum += dataList.ElementAt(index-1)[1];
                    }
                }
            }
            else
            {
                for (int i = steps; i < (steps + metaEntries); i++)
                {
                    branch.MetaDataSum += data.ElementAt(i);
                }
            }
            steps += metaEntries;
            branches.Add(branch);
            return new int[] { steps , branch.MetaDataSum};
        }
    }

    class Branch
    {
        public int Children { get; set; }
        public int MetaEntries { get; set; }
        public int MetaDataSum { get; set; }

        public Branch(int children, int metaEntries)
        {
            Children = children;
            MetaEntries = metaEntries;
            MetaDataSum = 0;
        }
    }
}
