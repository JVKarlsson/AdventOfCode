using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020.Day
{
    class Day05
    {
        private string _path;
        public Day05(string path)
        {
            _path = Path.Combine(path, "InputDay05.txt");

            PartOne();
            PartTwo();
        }

        public void PartOne()
        {
            var input = File.ReadAllLines(_path).ToList();
            List<int> ids = new();

            input.ForEach(line => { ids.Add(GetSeatId(line)); });
            var max = ids.Aggregate((highest, next) => next > highest ? next : highest);
            Console.WriteLine($"Advent of Code Day 05 part 1 : {max}");
        }

        public void PartTwo()
        {
            var input = File.ReadAllLines(_path).ToList();
            List<int> ids = new();

            input.ForEach(line => { ids.Add(GetSeatId(line)); });
            var yourSeat = ids.Where(x => !ids.Contains(x+1) || !ids.Contains(x + -1)).OrderBy(x => x).ToList()[2]-1;

            Console.WriteLine($"Advent of Code Day 05 part 2 : {yourSeat}");
        }

        private int GetSeatId(string line)
        {
            var rows = (0d, 127d);
            var seats = (0d, 7d);

            // prob better solution, switch to byte arrays and right/left shift
            foreach (var c in line)
            {
                rows = c switch
                {
                    'F' => (rows.Item1, rows.Item2 - Math.Ceiling((rows.Item2 - rows.Item1) / 2)),
                    'B' => (rows.Item1 + Math.Ceiling((rows.Item2 - rows.Item1) / 2), rows.Item2),

                    _ => rows
                };

                seats = c switch
                {
                    'L' => (seats.Item1, seats.Item2 - Math.Ceiling((seats.Item2 - seats.Item1) / 2)),
                    'R' => (seats.Item1 + Math.Ceiling((seats.Item2 - seats.Item1) / 2), seats.Item2),

                    _ => seats
                };
            }

            return (int)(rows.Item1 * 8 + seats.Item1);
        }
    }
}
