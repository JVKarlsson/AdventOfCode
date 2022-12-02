using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022.Day
{
    internal class Day02
    {
        private string _path;
        public Day02(string path)
        {
            _path = Path.Combine(path, $"Input{this.GetType().Name}.txt");
            PartOne();
            PartTwo();
        }
        public void PartOne()
        {
            var result = File.ReadAllLines(_path)
                .Select(x => GetScore(Translate(x[0]), Translate(x[2])))
                .Sum();
            Console.WriteLine($"Advent of Code Day 01 part 1 : {result}");
        }

        public void PartTwo()
        {
            var result = File.ReadAllLines(_path)
                .Select(x => GetScore(Translate(x[0]), Translate2(x)))
                .Sum();
            Console.WriteLine($"Advent of Code Day 01 part 2 : {result}");
        }

        private static int GetScore(Sign oponent, Sign us)
        {
            int score = (int)us;
            if (oponent == us)
                score += 3;
            else if (oponent is Sign.Rock && us is Sign.Paper)
                score += 6;
            else if (oponent is Sign.Paper && us is Sign.Scissors)
                score += 6;
            else if (oponent is Sign.Scissors && us is Sign.Rock)
                score += 6;
            return score;
        }

        public static Sign Translate(char value) => value switch
        {
            'A' or 'X' => Sign.Rock,
            'B' or 'Y' => Sign.Paper,
            'C' or 'Z' => Sign.Scissors,
            _ => throw new ArgumentOutOfRangeException(),
        };
        public static Sign Translate2(string values) => values switch
        {
            ("A X") => Sign.Scissors,
            ("A Y") => Sign.Rock,
            ("A Z") => Sign.Paper,

            ("B X") => Sign.Rock,
            ("B Y") => Sign.Paper,
            ("B Z") => Sign.Scissors,

            ("C X") => Sign.Paper,
            ("C Y") => Sign.Scissors,
            ("C Z") => Sign.Rock,

            _ => throw new ArgumentOutOfRangeException(),
        };

        public enum Sign
        {
            Rock = 1,
            Paper = 2,
            Scissors = 3
        }
    }
}