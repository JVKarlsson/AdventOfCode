using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2018.Day
{
    class Day10
    {
        private static string path = "";
        public Day10(string p)
        {
            path = Path.Combine(p, "InputDay10.txt");
            Console.WriteLine("Advent of Code Day 10");
            PartOne();
        }

        private void PartOne()
        {
            var lines = File.ReadAllLines(path).ToList();
            var lights = new List<Light>();
            foreach (var line in lines)
            {
                var temp = line.Substring(10, 14);
                var split = temp.Split(',');
                var pos = new int[] { Convert.ToInt32(split[0]), Convert.ToInt32(split[1]) };
                temp = line.Substring(36, 6);
                split = temp.Split(',');
                var vel = new int[] { Convert.ToInt32(split[0]), Convert.ToInt32(split[1]) };
                var light = new Light(pos, vel);
                lights.Add(light);
            }

            bool haveMessage = false;
            var count = 0;
            while (!haveMessage)
            {
                count++;
                DrawLights(count, lights);
            }
        }

        private void DrawLights(int count, List<Light> lights)
        {
            bool haveWritten = false;
            var Mx = lights.Aggregate((l, r) => l.Position[0] > r.Position[0] ? l : r).Position[0];
            var My = lights.Aggregate((l, r) => l.Position[1] > r.Position[1] ? l : r).Position[1];
            var mx = lights.Aggregate((l, r) => l.Position[0] > r.Position[0] ? r : l).Position[0];
            var my = lights.Aggregate((l, r) => l.Position[1] > r.Position[1] ? r : l).Position[1];

            if ((Mx - mx) < 100 && (My - my) < 100)
            {
                for (int y = my; y <= My; y++)
                {
                    var lineToWrite = "";
                    for (int x = mx; x <= Mx; x++)
                    {
                        foreach (var light in lights)
                        {
                            if (light.Position[0] == x && light.Position[1] == y)
                                haveWritten = true;
                        }

                        if (haveWritten)
                        {
                            lineToWrite += "#";
                            haveWritten = false;
                        }
                        else
                        {
                            lineToWrite += " ";
                        }
                    }
                    Console.WriteLine(lineToWrite);
                }
                Console.Clear();
            }

            foreach (var light in lights)
            {
                light.Position[0] += light.Velocity[0];
                light.Position[1] += light.Velocity[1];
            }
        }
    }

    class Light
    {
        public int[] Position { get; set; }
        public int[] Velocity { get; set; }

        public Light(int[] pos, int[] vel)
        {
            Position = pos;
            Velocity = vel;
        }
    }
}
