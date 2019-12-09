using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode2019.Day
{
    class Day5
    {
        private string _path = "";

        public Day5(string path)
        {
            _path = System.IO.Path.Combine(path, "InputDay5.txt");
            var result = "";
            PartOne().ForEach(x => result += $"{x.ToString()} ");
            Console.WriteLine("Advent of Code Day 5 part 1 : diagnostic code = " + result);
            result = "";
            PartTwo().ForEach(x => result += $"{x.ToString()} ");
            Console.WriteLine("Advent of Code Day 5 part 2 : diagnostic code = " + result);
        }

        private List<int> PartOne()
        {
            var lines = System.IO.File.ReadAllText(_path).Split(",").Select(x => int.Parse(x)).ToArray();
            return RunCode(lines, 1);
        }

        private List<int> PartTwo()
        {
            var lines = System.IO.File.ReadAllText(_path).Split(",").Select(x => int.Parse(x)).ToArray();
            return RunCode(lines, 5);
        }

        private List<int> RunCode(int[] codeList, int input)
        {
            var output = new List<int>();
            var index = 0;
            while (codeList[index] != 99)
            {
                int num = codeList[index];
                int opcode = (num % 10);

                switch (opcode)
                {
                    case 3:
                        codeList[codeList[index + 1]] = input;
                        index += 2;
                        break;
                    case 4:
                        output.Add(codeList[codeList[index + 1]]);
                        index += 2;
                        break;
                    default:

                        var firstAddress = codeList[index + 1];
                        var secondAddress = codeList[index + 2];

                        var firstValue = ((num / 100) % 10) == 1 ? firstAddress : codeList[firstAddress];
                        var secondValue = (((num / 1000) % 10) == 1) ? secondAddress : codeList[secondAddress];

                        var result = HandleOpCode(opcode, firstValue, secondValue, index);
                        
                        if (opcode == 5 || opcode == 6)
                        {
                            index = result;
                        }
                        else
                        {
                            codeList[codeList[index + 3]] = result;
                            index += 4;
                        }
                        break;
                }
            }
            return output;
        }

        static int HandleOpCode(int opCode, int value1, int value2, int index) =>
            opCode switch
            {
                1 => value1 + value2,
                2 => value1 * value2,
                7 => (value1 < value2) ? 1 : 0,
                8 => (value1 == value2) ? 1 : 0,

                5 => (value1 != 0) ? value2 : (index + 3),
                6 => (value1 == 0) ? value2 : (index + 3),

                _ => throw new ArgumentException("Invalid opcode passed: " + opCode)
            };
    }
}
