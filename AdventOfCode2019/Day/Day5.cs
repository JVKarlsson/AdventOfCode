using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode2019.Day
{
    class Day5
    {
        private string _path = "";
        private int _output = 1;

        public Day5(string path)
        {
            _path = System.IO.Path.Combine(path, "InputDay4.txt");
            Console.WriteLine("Advent of Code Day 5 part 1 : diagnostic code = " + PartOne());
            Console.WriteLine("Advent of Code Day 5 part 2 : diagnostic code = " + PartTwo());
        }

        private string PartOne()
        {
            var lines = System.IO.File.ReadAllText(_path).Split(",").Select(x => int.Parse(x)).ToList(); ;
            throw new NotImplementedException();
        }

        private string PartTwo()
        {
            throw new NotImplementedException();
        }

        private int RunCode(List<int> codeList)
        {
            var numOfinstructions = 4;
            for (int i = 0; i < codeList.Count;)
            {

                switch (codeList[i])
                {
                    case 1:
                        codeList[codeList[i + 3]] = codeList[codeList[i + 1]] + codeList[codeList[i + 2]];
                        break;
                    case 2:
                        codeList[codeList[i + 3]] = codeList[codeList[i + 1]] * codeList[codeList[i + 2]];
                        break;
                    case 3:
                        numOfinstructions = 2;
                        break;
                    case 4:
                        numOfinstructions = 2;
                        break;
                    case 99:
                        return codeList[0];
                    default:
                        // handle parameter mode
                        break;
                }

                i += numOfinstructions;
            }
            return codeList[0];
        }

        private void RunCommand(List<int> codeList, int pointer)
        {
            switch (codeList[pointer])
            {
                case 1:
                    codeList[codeList[pointer + 3]] = codeList[codeList[pointer + 1]] + codeList[codeList[pointer + 2]];
                    break;
                case 2:
                    codeList[codeList[pointer + 3]] = codeList[codeList[pointer + 1]] * codeList[codeList[pointer + 2]];
                    break;
                case 3:
                    //numOfinstructions = 2;
                    break;
                case 4:
                    //numOfinstructions = 2;
                    break;
                case 99:
                    return codeList[0];
                default:
                    // handle parameter mode
                    break;
            }
        }
    }
}
