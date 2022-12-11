using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022.Day
{
    internal class Day11
    {
        private string _path;
        public Day11(string path)
        {
            _path = Path.Combine(path, $"Input{this.GetType().Name}.txt");
            PartOne();
            PartTwo();
        }
        public void PartOne()
        {
            var monkeys = File.ReadAllText(_path).Split("\r\n\r\n").Select(x => new Monkey(x)).ToList();
            monkeys.ForEach(x => x.Monkeys = monkeys);

            for (int i = 0; i < 20; i++)
            {
                monkeys.ForEach(x => x.Play());
            }

            var result = monkeys.Select(x => x.InspectionCount).OrderByDescending(x => x).Take(2).Aggregate((a, x) => a * x);
            Console.WriteLine($"Advent of Code Day 11 part 1 : {result}");
        }

        public void PartTwo()
        {
            var monkeys = File.ReadAllText(_path).Split("\r\n\r\n").Select(x => new Monkey(x)).ToList();

            // globalmodulo happily stolen from reddit
            var globalModulo = monkeys.Select(m => m.TestNumber).Aggregate((m, i) => m * i);
            monkeys.ForEach(x => { x.Monkeys = monkeys; x.GlobalModulo = (ulong)globalModulo; }) ;

            for (int i = 0; i < 10000; i++)
            {
                monkeys.ForEach(x => x.Play(false));
            }
            var result = monkeys.Select(x => x.InspectionCount).OrderByDescending(x => x).Take(2).Aggregate((a, x) => a * x);
            Console.WriteLine($"Advent of Code Day 11 part 2 : {result}");
        }
    }

    internal class Monkey
    {
        public Monkey(string monkeyText)
        {
            var split = monkeyText.Split("\r\n  ").ToList();

            Id = int.Parse(split[0].Split(' ')[1].Replace(":", ""));
            Inventory = split[1].Split(": ")[1].Split(',').Select(x => ulong.Parse(x.Trim())).ToList();
            Operation = split[2].Split(" = ")[1].Trim().Split(' ').ToList();
            TestNumber = decimal.Parse(split[3].Split(" by ")[1]);
            TestTrue = int.Parse(split[4].Split(' ').Last());
            TestFalse = int.Parse(split[5].Split(' ').Last());
            InspectionCount = 0;
            //.Select(x => ulong.Parse).ToList();
        }

        public int Id { get; set; }
        public List<ulong> Inventory { get; set; }
        public List<string> Operation { get; set; }
        public decimal TestNumber { get; set; }
        public int TestTrue { get; set; }
        public int TestFalse { get; set; }
        public List<Monkey> Monkeys { get; set; }
        public ulong InspectionCount { get; set; }
        public ulong GlobalModulo { get; set; }

        public void Play(bool divide = true)
        {
            List<ulong> toRemove = new();
            for (int i = 0; i < Inventory.Count; i++)
            {
                Inventory[i] = HandleOperation(Inventory[i], Operation);
                if (divide)
                    Inventory[i] /= 3;
                else
                {
                    Inventory[i] %= GlobalModulo;
                }

                if (Inventory[i] % TestNumber == 0)
                {
                    Monkeys[TestTrue].Inventory.Add(Inventory[i]);
                }
                else
                {
                    Monkeys[TestFalse].Inventory.Add(Inventory[i]);
                }
                toRemove.Add(Inventory[i]);
                InspectionCount++;
            }
            toRemove.ForEach(x => Inventory.Remove(x));
        }

        private ulong HandleOperation(ulong value, List<string> operation)
        {
            ulong result = 0;
            if (operation[1] == "*")
            {
                if (operation[2] == "old")
                {
                    result = value * value;
                }
                else
                {
                    result = value * ulong.Parse(operation[2]);
                }
            }
            else if (operation[1] == "+")
            {
                if (operation[2] == "old")
                {
                    result = value + value;
                }
                else
                {
                    result = value + ulong.Parse(operation[2]);
                }
            }
            return result;
        }
    }
}
