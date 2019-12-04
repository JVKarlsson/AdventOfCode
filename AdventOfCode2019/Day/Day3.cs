using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventOfCode2019.Day
{
    class Day3
    {
        private string _path = "";
        public Day3(string path)
        {
            _path = Path.Combine(path, "InputDay3.txt");
            Console.WriteLine("Advent of Code Day 3 part 1 : Distance = " + PartOne());
            Console.WriteLine("Advent of Code Day 3 part 2 : Distance = " + PartTwo());
        }

        private int PartOne()
        {
            var lines = File.ReadAllLines(_path);
            var wire1_path = lines[0].Split(",").ToList();
            var wire2_path = lines[1].Split(",").ToList();

            var path = new Dictionary<(int, int), int>();
            var currentpos = (X: 0, Y: 0);

            foreach (var movement in wire1_path)
            {
                var change = Move(movement[0]);
                for (int i = 0; i < int.Parse(movement.Substring(1)); i++)
                {
                    currentpos.X += change.Item1;
                    currentpos.Y += change.Item2;
                    var dist = Math.Abs(currentpos.X) + Math.Abs(currentpos.Y);
                    _ = path.TryAdd(currentpos, dist);
                }
            }

            currentpos = (0, 0);
            var minimumDistance = int.MaxValue;

            foreach (var movement in wire2_path)
            {
                var change = Move(movement[0]);
                for (int i = 0; i < int.Parse(movement.Substring(1)); i++)
                {
                    currentpos.X += change.Item1;
                    currentpos.Y += change.Item2;
                    if (path.ContainsKey(currentpos))
                    {
                        var dist = Math.Abs(currentpos.X) + Math.Abs(currentpos.Y);
                        minimumDistance = Math.Min(minimumDistance, dist);
                    }
                }
            }
            return minimumDistance;
        }


        private int PartTwo()
        {
            var lines = File.ReadAllLines(_path);
            var wire1_path = lines[0].Split(",").ToList();
            var wire2_path = lines[1].Split(",").ToList();

            var path = new Dictionary<(int, int), int>();
            var currentpos = (X: 0, Y: 0);
            var steps = 0;

            foreach (var movement in wire1_path)
            {
                var change = Move(movement[0]);
                for (int i = 0; i < int.Parse(movement.Substring(1)); i++)
                {
                    steps++;
                    currentpos.X += change.Item1;
                    currentpos.Y += change.Item2;
                    _ = path.TryAdd(currentpos, steps);
                }
            }

            currentpos = (0, 0);
            var minimumDistance = int.MaxValue;

            steps = 0;
            foreach (var movement in wire2_path)
            {
                var change = Move(movement[0]);
                for (int i = 0; i < int.Parse(movement.Substring(1)); i++)
                {
                    steps++;
                    currentpos.X += change.Item1;
                    currentpos.Y += change.Item2;
                    if (path.ContainsKey(currentpos))
                    {
                        var dist = steps + path[currentpos];
                        minimumDistance = Math.Min(minimumDistance, dist);
                    }
                }
            }
            return minimumDistance;
        }

        private static (int, int) Move(char movement) =>
            movement switch
            {
                'U' => (0, 1),
                'D' => (0, -1),
                'L' => (-1, 0),
                'R' => (1, 0),
                _ => throw new ArgumentException(message: "invalid character value", paramName: nameof(movement)),
            };
    }
}
