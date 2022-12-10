using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022.Day
{
    internal class Day10
    {
        private string _path;
        public Day10(string path)
        {
            _path = Path.Combine(path, $"Input{this.GetType().Name}.txt");
            PartOne();
            PartTwo();
        }
        public void PartOne()
        {
            var lines = File.ReadAllLines(_path).Select(x => x.Split(' ')).ToList();

            var register = 1;
            var signalStrength = 0;
            var cycle = 0;
            foreach (var line in lines)
            {
                cycle++;
                if (cycle % 40 == 20)
                {
                    signalStrength += register * cycle;
                }
                if (cycle == 220)
                {
                    break;
                }



                if (line[0] == "addx")
                {
                    cycle++;
                    if (cycle % 40 == 20)
                    {
                        signalStrength += register * cycle;
                    }
                    register += int.Parse(line[1]);

                    if (cycle == 220)
                    {
                        break;
                    }
                }
            }

            Console.WriteLine($"Advent of Code Day 10 part 1 : {signalStrength}");
        }

        public void PartTwo()
        {
            var lines = File.ReadAllLines(_path).Select(x => x.Split(' ')).ToList();

            var register = 1;
            var cycle = 0;
            var screen = new bool[6, 40];

            foreach (var line in lines)
            {
                RunCycle(ref register, ref cycle, screen, line, false);
                if (line[0] == "addx")
                    RunCycle(ref register, ref cycle, screen, line, true);
            }
            Draw(screen);
            Console.WriteLine($"Advent of Code Day 10 part 2 : RUAKHBEK");
        }

        private static void RunCycle(ref int register, ref int cycle, bool[,] screen, string[] line, bool increaseRegister)
        {
            var row = cycle / 40;
            var column = cycle % 40;
            for (int i = register - 1; i <= register + 1; i++)
            {
                if (column == i)
                {
                    screen[row, column] = true;
                    break;
                }
            }
            if (increaseRegister)
                register += int.Parse(line[1]);
            cycle++;
        }

        private void Draw(bool[,] screen)
        {
            Console.WriteLine();
            Console.WriteLine();
            for (int i = 0; i < screen.GetLength(0); i++)
            {
                string row = "";
                for (int j = 0; j < screen.GetLength(1); j++)
                {
                    row += screen[i, j] ? "#" : ".";
                }
                Console.WriteLine(row);
            }
        }
    }
}
