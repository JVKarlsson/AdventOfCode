using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022.Day
{
    internal class Day09
    {
        private string _path;
        public Day09(string path)
        {
            _path = Path.Combine(path, $"Input{this.GetType().Name}.txt");
            PartOne();
            PartTwo();
        }
        public void PartOne()
        {
            var lines = File.ReadAllLines(_path);
            (int X, int Y) headPos = (0, 0);
            (int X, int Y) tailPos = (0, 0);

            var visited = new List<(int X, int Y)>() { tailPos };
            foreach (var line in lines)
            {
                var split = line.Split(' ');
                var dir = split[0];
                var steps = int.Parse(split[1]);
                var direction = GetDirection(dir);

                for (int i = 1; i <= steps; i++)
                {
                    headPos = (headPos.X + direction.X, headPos.Y + direction.Y);

                    if (Math.Abs(headPos.X - tailPos.X) > 1 || Math.Abs(headPos.Y - tailPos.Y) > 1)
                    {
                        var x = headPos.X - tailPos.X;
                        var y = headPos.Y - tailPos.Y;
                        x = x == 0 ? x / 1 : x / Math.Abs(x);
                        y = y == 0 ? y / 1 : y / Math.Abs(y);

                        tailPos.X += x;
                        tailPos.Y += y;

                        visited.Add(tailPos);
                    }
                }
            }
            var result = visited.Distinct().Count();
            Console.WriteLine($"Advent of Code Day 09 part 1 : {result}");
        }

        public void PartTwo()
        {
            var lines = File.ReadAllLines(_path);
            List<(int X, int Y)> nodes = Enumerable.Range(0, 10).Select(x => (0, 0)).ToList();
            var visited = new List<(int X, int Y)>() { (0, 0) };

            foreach (var line in lines)
            {
                var split = line.Split(' ');
                var dir = split[0];
                var steps = int.Parse(split[1]);
                var direction = GetDirection(dir);

                for (int i = 0; i < steps; i++)
                {
                    nodes[0] = (nodes[0].X + direction.X, nodes[0].Y + direction.Y);
                    for (int j = 1; j < nodes.Count; j++)
                    {
                        if (Math.Abs(nodes[j-1].X - nodes[j].X) > 1 || Math.Abs(nodes[j-1].Y - nodes[j].Y) > 1)
                        {
                            var x = nodes[j-1].X - nodes[j].X;
                            var y = nodes[j-1].Y - nodes[j].Y;
                            x = x == 0 ? x / 1 : x / Math.Abs(x);
                            y = y == 0 ? y / 1 : y / Math.Abs(y);

                            var node = nodes[j];
                            node.X += x;
                            node.Y += y;
                            nodes[j] = node;

                            if (j is 9)
                            {
                                visited.Add(nodes[j]);
                            }
                        }
                    }
                }
            }
            var result = visited.Distinct().Count();
            Console.WriteLine($"Advent of Code Day 09 part 2 : {0}");
        }

        public static (int X, int Y) GetDirection(string direction) => direction switch
        {
            "R" => (0, 1),
            "L" => (0, -1),
            "U" => (-1, 0),
            "D" => (1, 0),
            _ => throw new ArgumentOutOfRangeException(nameof(direction), $"Not expected direction value: {direction}"),
        };
    }
}
