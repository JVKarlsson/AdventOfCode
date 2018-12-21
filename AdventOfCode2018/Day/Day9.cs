using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2018.Day
{
    class Day9
    {
        public Day9()
        {
            Console.WriteLine("Advent of Code Day 9 Part 1: " + Solve(1));
            Console.WriteLine("Advent of Code Day 9 Part 2: " + Solve(100));

        }

        private long Solve(int multiplier)
        {
            var players = 446;
            var marbles = 71522 * multiplier;
            var points = new long[players];
            var marbleCircle = new LinkedList<int>();

            var current = marbleCircle.AddFirst(0);

            for (int i = 1; i < marbles; i++)
            {
                if (i % 23 == 0)
                {
                    int score = i;
                    for (int j = 0; j < 7; j++)
                        current = current.Previous == null ? marbleCircle.Last : current.Previous;
                    score += current.Value;

                    var RemoveThis = current;
                    current = current.Next == null ? marbleCircle.First : current.Next;
                    marbleCircle.Remove(RemoveThis);

                    points[i % players] += score;
                }
                else
                    current = marbleCircle.AddAfter(current.Next == null ? marbleCircle.First : current.Next, i);
            }
            return points.Max();
        }
    }
}
