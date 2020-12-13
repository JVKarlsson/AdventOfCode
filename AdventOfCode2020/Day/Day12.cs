using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020.Day
{
    class Day12
    {
        private string _path;
        public Day12(string path)
        {
            _path = Path.Combine(path, "InputDay12.txt");

            PartOne();
            PartTwo();
        }

        public void PartOne()
        {
            var input = File.ReadAllLines(_path).Select(x => (x[0], int.Parse(x[1..]))).ToList();
            (int, int) pos = (0, 0);
            var direction = 180;

            foreach (var line in input)
            {

                var test = line.Item1 switch
                {
                    'N' => (0, line.Item2),
                    'S' => (0, -line.Item2),
                    'E' => (line.Item2, 0),
                    'W' => (-line.Item2, 0),
                    'F' => Forward(direction, line.Item2),
                    _ => (0, 0)
                };
                pos = (pos.Item1 + test.Item1, pos.Item2 + test.Item2);

                direction = line.Item1 switch
                {
                    'L' => (direction + (360 - line.Item2)) % 360,
                    'R' => (direction + line.Item2) % 360,
                    _ => direction
                };
            }

            var result = Math.Abs(pos.Item1) + Math.Abs(pos.Item2);
            Console.WriteLine($"Advent of Code Day 12 part 1 : {result}");
        }
        private (int, int) Forward(int direction, int units)
        {
            return direction switch
            {
                90 => (0, units),
                270 => (0, -units),
                180 => (units, 0),
                0 => (-units, 0),
                _ => (0, 0)
            };
        }

        public void PartTwo()
        {
            var input = File.ReadAllLines(_path).Select(x => (x[0], int.Parse(x[1..]))).ToList();
            (int, int) pos = (0, 0);
            (int, int) waypos = (10, 1);

            foreach (var line in input)
            {
                waypos = line.Item1 switch
                {
                    'N' => (waypos.Item1, waypos.Item2 + line.Item2),
                    'S' => (waypos.Item1, waypos.Item2 - line.Item2),
                    'E' => (waypos.Item1 + line.Item2, waypos.Item2),
                    'W' => (waypos.Item1 - line.Item2, waypos.Item2),
                    'L' or 'R' => RotateWaypoint(line, waypos),
                    _ => waypos
                };

                pos = line.Item1 switch
                {
                    'F' => (pos.Item1 + (waypos.Item1  * line.Item2), pos.Item2 + (waypos.Item2  * line.Item2)),
                    _ => pos
                };
            }

            var result = Math.Abs(pos.Item1) + Math.Abs(pos.Item2);
            Console.WriteLine($"Advent of Code Day 12 part 2 : {result}");
        }

        private (int, int) RotateWaypoint((char, int) line, (int,int) waypos)
        {
            var rotations = line.Item1 == 'L' ? (4 - line.Item2/90) % 4 : ( line.Item2 / 90) % 4;
            for (int i = 0; i < rotations; i++)
            {
                waypos = (waypos.Item2, -waypos.Item1);
            }

            return waypos;
        }
    }
}
