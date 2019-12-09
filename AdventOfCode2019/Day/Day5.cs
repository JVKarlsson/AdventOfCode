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
                        int param1Mode = (num / 100) % 10;
                        int param2Mode = (num / 1000) % 10;

                        int addr1 = codeList[index + 1];
                        int addr2 = codeList[index + 2];

                        int param1 = (param1Mode == 1) ? addr1 : codeList[addr1];
                        int param2 = (param2Mode == 1) ? addr2 : codeList[addr2];


                        if (opcode != 5 && opcode != 6)
                        {
                            int varAddr = codeList[index + 3];
                            int result = HandleOpCode(opcode, param1, param2);

                            codeList[varAddr] = result;
                            index += 4;
                        }
                        else
                        {
                            index = ShouldJump(opcode, param1) ? param2 : (index + 3);
                        }


                        break;
                }
            }
            return output;
        }

        static int HandleOpCode(int opCode, int value1, int value2)
        {
            if (opCode == 1) return (value1 + value2);
            if (opCode == 2) return (value1 * value2);
            if (opCode == 7) return (value1 < value2) ? 1 : 0;
            if (opCode == 8) return (value1 == value2) ? 1 : 0;
            throw new ArgumentException("Invalid opcode passed: " + opCode);
        }

        static bool ShouldJump(int opCode, int value)
        {
            if (opCode == 5) return value != 0;
            if (opCode == 6) return value == 0;

            return false;
        }
    }
}
