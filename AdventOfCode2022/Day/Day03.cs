using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022.Day
{
    internal class Day03
    {
        private string _path;
        public Day03(string path)
        {
            _path = Path.Combine(path, $"Input{this.GetType().Name}.txt");
            //PartOne();
            PartTwo();
        }
        public void PartOne()
        {
            var lines = File.ReadAllLines(_path);

            List<char[]> data = new (); 
            foreach (var item in lines)
            {
                var half = item.Length / 2;

                var first = item.Substring(0, half).ToArray();
                var second = item.Substring(half).ToArray();

                var intersects = first.Intersect(second).ToArray();
                data.Add(intersects);
            }

            var result = 0;
            foreach (var item in data)
            {
                foreach (var character in item)
                {
                    if (Char.IsUpper(character))
                    {
                        var val = character - 64 + 26;
                        result += val;
                    }
                    else
                    {
                        var val = character - 96;
                        result += val;
                    }
                }
            }
            Console.WriteLine($"Advent of Code Day 01 part 1 : {result}");
        }

        public void PartTwo()
        {
            var lines = File.ReadAllLines(_path).ToList();
            var asd = Partition(lines, 3);

            List<char[]> data = new();
            foreach (var item in asd)
            {
                var first = item[0].ToArray();
                var second = item[1].ToArray();
                var third = item[2].ToArray();
                var intersects = first.Intersect(second).Intersect(third).ToArray();
                data.Add(intersects);
            }

            var result = 0;
            foreach (var item in data)
            {
                foreach (var character in item)
                {
                    if (Char.IsUpper(character))
                    {
                        var val = character - 64 + 26;
                        result += val;
                    }
                    else
                    {
                        var val = character - 96;
                        result += val;
                    }
                }
            }

            Console.WriteLine($"Advent of Code Day 01 part 2 : {0}");
        }

        public static IEnumerable<List<string>> Partition(IList<string> source, Int32 size)
        {
            for (int i = 0; i < Math.Ceiling(source.Count / (Double)size); i++)
                yield return new List<string>(source.Skip(size * i).Take(size));
        }
    }
}
