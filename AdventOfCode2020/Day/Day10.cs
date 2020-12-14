using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020.Day
{
    class Day10
    {
        private readonly string _path;


        public Day10(string path)
        {
            _path = Path.Combine(path, "InputDay10.txt");

            PartOne();
            PartTwo();
        }

        public void PartOne()
        {
            var input = File.ReadAllLines(_path).Select(x => int.Parse(x)).Distinct().OrderBy(x => x).ToList();

            var DeviceJoltage = input.Last() + 3;
            var currentJoltage = 0;
            List<int> differences = new();

            while (input.Count > 0)
            {
                var valid = input.Where(x => x - 3 <= currentJoltage).ToList();
                differences.Add(valid.First() - currentJoltage);
                currentJoltage = valid.First();

                input = input.Where(x => x > currentJoltage).ToList();
            }
            differences.Add(3);
            var jolt1 = differences.Count(x => x == 1);
            var jolt3 = differences.Count(x => x == 3);
            var sum = jolt1 * jolt3;
            Console.WriteLine($"Advent of Code Day 10 part 1 : {sum}");
        }

        public void PartTwo()
        {
            var input = File.ReadAllLines(_path).Select(x => int.Parse(x)).Distinct().OrderBy(x => x).ToList();

            input.Add(input.Last() + 3);
            //input.Insert(0,0);

            Dictionary<long, long> resultSet = new ();
            var sum = ArrangeAdapters(input.ToArray(), resultSet);



            //input 13 result 12 
            //var sum2 = input.Aggregate(
            //    new[] { (jolts: 0, count: 1L) },
            //    (acc, nextJolts) =>
            //        acc[^Math.Min(2, acc.Length)..]
            //           .Append((
            //                       jolts: nextJolts,
            //                       count: acc.Where(prev => nextJolts - prev.jolts <= 3).Sum(x => x.count)
            //                   ))
            //           .ToArray(),
            //    acc => acc.Last().count);



            Console.WriteLine($"Advent of Code Day 10 part 2 : {sum}");
        }




        private long ArrangeAdapters(int[] input, Dictionary<long, long> resultSet)
        {
            //Enumerable.Range(2, input.Length - 1).Select(x => resultSet.Add(x + 2, 0));

            //var sum = input.Select((x, i) => 
            //{
            //    var remaining = input[i..];
            //    var valid = remaining.Where(y => y - 3 <= x).ToList();
            //    var count = 0;



            //    return count;
            //});
            var currentJoltage = 0;
            var valid = input.Where(x => x - 3 <= currentJoltage).ToList();
            var cont = valid.FirstOrDefault();


            //var steps = Enumerable.Range(2, input.Length - 1).ToList();
            //steps.ForEach(x => resultSet.Add(x, 0));

            //foreach (var step in steps)
            //{

            //}



            if (resultSet.ContainsKey(input.Length) )
            {

            }
            else
            {

            }
            //var res =  ? 1 : Math.Pow(2, (ArrangeAdapters(input[valid.Count -1], resultSet) - 1));


            //long count = valid.Count == 0 ? 1 : 0;
            //count += valid.Select(x => ArrangeAdapters(input.Where(y => y > x).ToList(), x)).Sum();












            long count = input.Length == 1 ? 1 : 0;

            if (resultSet.ContainsKey(input.Length))
            {
                return resultSet[input.Length];
            }

            if (input.Length == 1)
            {
                return 1;
            }

            count = 0;
            currentJoltage = input[0];

            //count += input.Aggregate((x, i) => ArrangeAdapters(input.Where(y => y > x).ToList(), x));

            for (int i = 1; i < input.Length; i++)
            {
                if (input[i] - 3 <= currentJoltage)
                {
                    count += ArrangeAdapters(input[i..], resultSet);
                }
                else
                {
                    break;
                }
            }

            resultSet.Add(input.Length, count);



            return count;
        }

        //private long ArrangeAdapters(int[] input, Dictionary<long, long> resultSet)
        //{
        //    if (resultSet.ContainsKey(input.Length))
        //    {
        //        return resultSet[input.Length];
        //    }

        //    if (input.Length == 1)
        //    {
        //        return 1;
        //    }

        //    long count = 0;
        //    long currentJoltage = input[0];

        //    //count += input.Aggregate((x, i) => ArrangeAdapters(input.Where(y => y > x).ToList(), x));

        //    for (int i = 1; i < input.Length; i++)
        //    {
        //        if (input[i] - 3 <= currentJoltage)
        //        {
        //            count += ArrangeAdapters(input[i..], resultSet);
        //        }
        //        else
        //        {
        //            break;
        //        }
        //    }

        //    resultSet.Add(input.Length, count);



        //var valid = input.Where(x => x - 3 <= currentJoltage).ToList();
        //long count = valid.Count == 0 ? 1 : 0;
        //count += valid.Select(x => ArrangeAdapters(input.Where(y => y > x).ToList(), x)).Sum();

        //    return count;
        //}

        private long CountAdapters(List<int> input, int currentJoltage)
        {
            var valid = input.Where(x => x - 3 <= currentJoltage).ToList();
            long count = valid.Count == 0 ? 1 : 0;
            count += valid.Select(x => CountAdapters(input.Where(y => y > x).ToList(), x)).Sum();
            return count;
        }
    }
}