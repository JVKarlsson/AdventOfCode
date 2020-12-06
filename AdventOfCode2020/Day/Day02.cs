using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode2020.Day
{
    class Day02
    {
        private string _path;
        public Day02(string path)
        {
            _path = Path.Combine(path, "InputDay02.txt"); ;
            PartOne();
            PartTwo();
        }

        public void PartOne()
        {
            var input = File.ReadAllLines(_path).ToList();

            var validPasswords = 0;
            foreach (var line in input)
            {
                var splitLine = line.Split(" ");
                var minAndMax = splitLine[0].Split("-").Select(x => int.Parse(x)).ToList();
                var letter = splitLine[1].Substring(0, 1);

                //var test = GetAllIndexes(this string source, string matchString)
                var count = GetAllIndexes(splitLine[2], letter).Count();
                if (count >= minAndMax[0] && count <= minAndMax[1])
                    validPasswords++;
            }
            Console.WriteLine($"Advent of Code Day 02 part 1 : {validPasswords}");
        }

        public void PartTwo()
        {
            var input = File.ReadAllLines(_path).ToList();

            var validPasswords = 0;
            foreach (var line in input)
            {
                var splitLine = line.Split(" ");
                var minAndMax = splitLine[0].Split("-").Select(x => int.Parse(x) - 1 ).ToList();
                var letter = splitLine[1].Substring(0, 1);

                var indexes = GetAllIndexes(splitLine[2], letter);

                var count = indexes.Where(x => minAndMax.Contains(x)).Count();
                if (count == 1)
                    validPasswords++;
            }
            Console.WriteLine($"Advent of Code Day 02 part 2 : {validPasswords}"); 
        }

        public static IEnumerable<int> GetAllIndexes(string source, string matchString)
        {
            matchString = Regex.Escape(matchString);
            foreach (Match match in Regex.Matches(source, matchString))
            {
                yield return match.Index;
            }
        }
    }
}
