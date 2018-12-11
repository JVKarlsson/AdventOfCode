using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2018.Day
{
    class Day11
    {
        private int input = 9995;
        public Day11()
        {
            solve();
        }

        private void solve()
        {
            var Cells = new int[301, 301];
            var PowerList = new List<List<int>>();

            for (int x = 1; x <= 300; x++)
            {
                var row = new List<int>();
                for (int y = 1; y <= 300; y++)
                {
                    row.Add(SetPowerLevel(x, y));
                }
                PowerList.Add(row);
            }

            var p1 = PartOne(PowerList);
            //var p2 = PartTwo(PowerList);
            Console.WriteLine("Advent of Code Day 11 part 1 : {0},{1}", p1[0],p1[1]);
            //Console.WriteLine("Advent of Code Day 11 part 2 : {0},{1},{2}", p2.ElementAt(0), p2.ElementAt(1), p2.ElementAt(2));
            // part 2 took 40 min to run so just wrote down the answer after i got it the first time. 
            Console.WriteLine("Advent of Code Day 11 part 2 : {0},{1},{2}", 233, 116, 15);
        }

        private int[] PartOne(List<List<int>> PowerList)
        {
            var power = 0;
            var cord = new int[2];

            int gridsize = 3;
            var length = PowerList.Count - (gridsize - 1);
            for (int x = 1; x <= length; x++)
            {
                for (int y = 1; y <= length; y++)
                {
                    var gridval = 0;
                    for (int j = 0; j < gridsize; j++)
                    {
                        for (int i = 0; i < gridsize; i++)
                        {
                            var X = x + i - 1;
                            var Y = y + j - 1;
                            gridval += (PowerList.ElementAt(X)).ElementAt(Y);
                        }
                    }
                    if (gridval > power)
                    {
                        power = gridval;
                        cord = new int[] { x, y };
                    }
                }
            }
            return cord;
        }

        private List<int> PartTwo(List<List<int>> PowerList)
        {
            var BigPower = 0;
            var BigCord = new int[2];
            var BigSize = 0;
            for (int s = 1; s <= 300; s++)
            {
                var power = 0;
                var cord = new int[2];

                int gridsize = s;
                var length = PowerList.Count - (gridsize - 1);
                for (int x = 1; x <= length; x++)
                {
                    for (int y = 1; y <= length; y++)
                    {
                        var gridval = 0;
                        for (int j = 0; j < gridsize; j++)
                        {
                            for (int i = 0; i < gridsize; i++)
                            {
                                var X = x + i - 1;
                                var Y = y + j - 1;
                                gridval += (PowerList.ElementAt(X)).ElementAt(Y);
                            }
                        }
                        if (gridval > power)
                        {
                            power = gridval;
                            cord = new int[] { x, y };
                        }
                    }
                }

                if (power > BigPower)
                {
                    BigPower = power;
                    BigCord = cord;
                    BigSize = gridsize;
                }
            }
            return new List<int> { BigCord[0], BigCord[1], BigSize};
        }

        private int SetPowerLevel(int x, int y)
        {
            var rackId = x + 10;
            var powerLevel = rackId * y;
            powerLevel += input;
            powerLevel *= rackId;
            powerLevel /= 100;
            powerLevel %= 10;
            powerLevel -= 5;
            return powerLevel;
        }
    }
}
