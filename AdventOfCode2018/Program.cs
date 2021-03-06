﻿using AdventOfCode2018.Day;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2018
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = Path.Combine(Path.GetFullPath(@"..\..\"), "Inputs");

            //var day1 = new Day1(path);
            //var day2 = new Day2(path);
            //var day3 = new Day3(path);
            //var day4 = new Day4(path);
            //var day5 = new Day5(path);
            //var day6 = new Day6(path);
            //var Day7 = new Day7(path);
            //var Day8 = new Day8(path);
            //var Day9 = new Day9();
            //var Day10 = new Day10(path);
            //var Day11 = new Day11();
            //var Day12 = new Day12(path);
            //
            //
            //var Day15 = new Day15(path);
            //
            //
            //
            //
            //


            var attributes = File.ReadAllLines(@"C:\Users\jimmy.karlsson\Desktop\SitskortResponse.txt").ToArray();
            var list = new List<string>();
            foreach (var attribute in attributes)
            {
                if (attribute.ToUpper().Contains(("FriendlyName=\"employeeHsaId\"").ToUpper()))
                {
                    var value = attribute.Split(new string[] { "<saml:AttributeValue>" }, StringSplitOptions.None).ElementAt(1).Substring(0,26);
                    list.Add(value);
                }
            }
            var HsaId = list.Single();
            Console.WriteLine(HsaId);

            Console.ReadLine();
        }
    }
}
