using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2018.Day
{
    class Day4
    {
        private static string path = "";

        public Day4(string p)
        { //11367
            path = Path.Combine(p, "InputDay4.txt");
            var solution = GetSolutions();
            Console.WriteLine("Advent of Code Day 4 part 1 : " + solution[0]);
            Console.WriteLine("Advent of Code Day 4 part 2 : " + solution[1]);
        }

        private List<int> GetSolutions()
        {
            var dict = new Dictionary<string, Guard>();
            var lines = File.ReadAllLines(path).OrderBy(x => DateTime.Parse(x.Substring(1, 16))).ToList();

            string GuardId = "";
            var fellAsleep = 0;

            foreach (var line in lines)
            {
                var date = line.Substring(1, 16); // substring 1,16
                var minuteSlept = line.Substring(15, 2);
                if (line.Contains("Guard"))
                {
                    var id = line.Substring(26);
                    id = id.Substring(0, id.IndexOf("b"));
                    GuardId = id;

                    if (!dict.ContainsKey(GuardId))
                        dict.Add(GuardId, new Guard());
                }
                else if (line.Contains("wakes"))
                {
                    var wakes = Convert.ToInt32(line.Substring(15, 2));
                    for (int i = fellAsleep; i < wakes; i++)
                    {
                        dict[GuardId].minutesAsleep++;
                        if (!(dict[GuardId].dates.ContainsKey(i.ToString())))
                            dict[GuardId].dates.Add(i.ToString(), 1);
                        else
                            dict[GuardId].dates[i.ToString()]++;
                    }
                }
                else if (line.Contains("falls"))
                {
                    if (GuardId == "1171")
                    {
                        var a = 1;
                    }
                    fellAsleep = Convert.ToInt32(line.Substring(15, 2));
                }

            }
            dict.OrderBy(x => x.Value.minutesAsleep);

            var sleepsId = "";
            var time = 0;
            foreach (var guard in dict)
            {
                if (guard.Value.minutesAsleep > time)
                {
                    time = guard.Value.minutesAsleep;
                    sleepsId = guard.Key;
                }
            }

            var sleepytime = dict[sleepsId].dates.Aggregate((l, r) => l.Value > r.Value ? l : r).Key;
            var result = Convert.ToInt32(sleepsId) * Convert.ToInt32(sleepytime);

            var count = 0;
            sleepsId = "";
            var day = "";
            foreach (var d in dict)
            {
                var datecount = 0;
                var dayKey = "";

                foreach (var item in d.Value.dates)
                {
                    if (item.Value > datecount)
                    {
                        datecount = item.Value;
                        dayKey = item.Key;
                    }
                }

                if (datecount > count)
                {
                    sleepsId = d.Key;
                    var temp = d.Value.dates[dayKey];
                    day = dayKey;
                    count = datecount;
                }
            }


            var solution = new List<int>();
            solution.Add(result);
            result = Convert.ToInt32(sleepsId) * Convert.ToInt32(day);
            solution.Add(result);
            return solution;
        }
    }
    class Guard
    {
        public int minutesAsleep = 0;
        public Dictionary<string, int> dates = new Dictionary<string, int>();
    }
}
