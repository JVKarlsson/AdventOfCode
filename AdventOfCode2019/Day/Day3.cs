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


            /*
                Del 1: Hitta närmaste korsningen till origo. Beräknas enligt Manhattan distansen.

                Del 2: Hitta korsningen med minsta antal steg till origa (a + b)

             */


            var solution = Solve();
            Console.WriteLine("Advent of Code Day 3 part 1 : Distance = " + solution.Item1);
            Console.WriteLine("Advent of Code Day 3 part 2 : Distance = " + solution.Item1);
        }

        private (int, int) Solve()
        {
            var lines = File.ReadAllLines(_path);
            var wire1_path = lines[0].Split(",").ToList();
            var wire2_path = lines[1].Split(",").ToList();

            /* Added tuple to value instead of just an integer */
            var path = new Dictionary<(int, int), (int, int)>();
            var currentpos = (X: 0, Y: 0);

            /* added a step counter*/
            var steps = 0;

            foreach (var movement in wire1_path)
            {
                var change = Move(movement[0]);
                for (int i = 0; i < int.Parse(movement.Substring(1)); i++)
                {
                    /* increase the step counter*/
                    steps++;
                    currentpos.X += change.Item1;
                    currentpos.Y += change.Item2;
                    var dist = Math.Abs(currentpos.X) + Math.Abs(currentpos.Y);
                    _ = path.TryAdd(currentpos, (dist, steps));
                    //  path.TryAdd(currentpos, dist);
                }
            }

            currentpos = (0, 0);
            steps = 0;
            var minimumDistance = int.MaxValue;

            /* added minimumsteps */
            var minimumSteps = int.MaxValue;

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
                        var dist = Math.Abs(currentpos.X) + Math.Abs(currentpos.Y);

                        /* get sum of the steps */
                        var stepCount = steps + path[currentpos].Item2;

                        minimumDistance = Math.Min(minimumDistance, dist);
                        minimumSteps = Math.Min(minimumSteps, stepCount);
                    }
                }
            }
            return (minimumDistance, minimumSteps);

        }

        private int PartOne()
        {
            var lines = File.ReadAllLines(_path);

            /* Get the paths for each line */
            var wire1_path = lines[0].Split(",").ToList();
            var wire2_path = lines[1].Split(",").ToList();

            var path = new Dictionary<(int, int), int>();

            /* We set startposition as origin to easier calculate the manhattan distance */
            var currentpos = (X: 0, Y: 0);

            /* Itterate through the first path and add each step and distance to the dictionary */
            foreach (var movement in wire1_path)
            {
                var change = Move(movement[0]);

                /* for each step in the movement, add to the dictionary*/
                for (int i = 0; i < int.Parse(movement.Substring(1)); i++)
                {
                    currentpos.X += change.Item1;
                    currentpos.Y += change.Item2;
                    var dist = Math.Abs(currentpos.X) + Math.Abs(currentpos.Y);
                    _ = path.TryAdd(currentpos, dist);
                }
            }

            /* Reset the currentposition to origin */
            currentpos = (0, 0);
            var minimumDistance = int.MaxValue;

            /* Itterate through the first path */
            foreach (var movement in wire2_path)
            {
                var change = Move(movement[0]);
                for (int i = 0; i < int.Parse(movement.Substring(1)); i++)
                {
                    currentpos.X += change.Item1;
                    currentpos.Y += change.Item2;

                    /* If the current step has been crosed by the first line, get the distance from origin and set minimumdistance if it is the lowest */
                    if (path.ContainsKey(currentpos))
                    {
                        var dist = Math.Abs(currentpos.X) + Math.Abs(currentpos.Y);
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
