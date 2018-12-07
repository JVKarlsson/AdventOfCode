using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2018.Day
{
    class Day6
    {
        private static string path = "";
        public Day6(string p)
        {
            path = Path.Combine(p, "InputDay6.txt");
            var solution = Solve();
            Console.WriteLine("Advent of Code Day 6 part 1 : " + solution[0]);
            Console.WriteLine("Advent of Code Day 6 part 2 : " + solution[1]);
        }

        private int[] Solve()
        {
            var count = 0;
            var area = 0;
            var lines = File.ReadAllLines(path).ToList();
            var Nodes = new List<coordinate>();
            var max = new int[2];

            foreach (var line in lines)
            {
                var split = line.Split(',');
                var pos = new int[2] { Convert.ToInt32(split[0]), Convert.ToInt32(split[1]) };
                var coord = new coordinate(pos);
                Nodes.Add(coord);

                if (pos[0] > max[0])
                    max[0] = pos[0];
                if (pos[1] > max[1])
                    max[1] = pos[1];
            }


            for (int x = 0; x <= max[0]; x++)
            {
                for (int y = 0; y <= max[1]; y++)
                {
                    var totaldist = 0;
                    var minimumDist = int.MaxValue;
                    int nodeIndex = -1;
                    bool shared = false;
                    bool isInf = x == 0 || x == max[0] || y == 0 || y == max[1] ? true : false;

                    for (int i = 0; i < Nodes.Count; i++)
                    {
                        var dist = Math.Abs((x - Nodes[i].Posititon[0])) + Math.Abs(y - Nodes[i].Posititon[1]);
                        totaldist += dist;
                        if (dist != 0 && dist < minimumDist)
                        {
                            nodeIndex = i;
                            minimumDist = dist;
                            shared = false;
                        }
                        else if (dist == minimumDist)
                        {
                            shared = true;
                        }
                    }

                    if (!shared)
                        Nodes[nodeIndex].area++;
                    if (isInf && !shared)
                        Nodes[nodeIndex].IsInfinite = true;
                    if (!isInf && totaldist < 10000)
                        count++;
                }
            }


            foreach (var node in Nodes)
            {
                if (!node.IsInfinite && node.area > area)
                    area = node.area;
            }

            var solution = new int[] { area, count };
            return solution;
        }
    }


    class coordinate
    {
        public bool IsInfinite { get; set; }
        public int[] Posititon { get; set; }
        public int area { get; set; }

        public coordinate(int[] pos)
        {
            IsInfinite = false;
            Posititon = pos;
            area = 1;
        }
    }
}
