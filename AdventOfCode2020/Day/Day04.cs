using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode2020.Day
{
    class Day04
    {
        private string _path;
        public Day04(string path)
        {
            _path = Path.Combine(path, "InputDay04.txt");


            // both can be solved with Regex
            PartOne();
            PartTwo();
        }

        public void PartOne()
        {
            List<Passport> passports = new();
            File.ReadAllText(_path)
                    .Split(new string[] { Environment.NewLine + Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(x => x.Replace(Environment.NewLine, " "))
                    .ToList()
                    .ForEach(x => passports.Add(new Passport(x)));

            var count = passports.Select(x => x.IsValid(true, true)).Where(x => x).Count();
            Console.WriteLine($"Advent of Code Day 04 part 1 : {count}");
        }

        public void PartTwo()
        {
            List<Passport> passports = new();

            int index = 0;
            File.ReadAllText(_path)
                    .Split(new string[] { Environment.NewLine + Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(x => x.Replace(Environment.NewLine, " "))
                    .ToList()
                    .ForEach(x => { passports.Add(new Passport(x, index)); index++; });

            //var list = passports.Where(x => x.IsValid(true, false)).Select(x => x.Index).Agre();
            //var count = passports.Select(x => x.IsValid(true, false)).Where(x => x).Count();
            var count = passports.Where(x => x.IsValid(true, false)).Count();
            Console.WriteLine($"Advent of Code Day 04 part 2 : {count}");
        }
    }

    class Passport
    {
        public int Index { get; set; }
        public Dictionary<string, string> Properties { get; set; }  = new Dictionary<string, string>();
        public Passport(string values, int index = 0)
        {
            Index = index;
            values.Replace(Environment.NewLine, " ")
                .Split(" ")
                .ToList()
                .ForEach(x => AddToDict(x));
        }

        private void AddToDict(string keyValuePair)
        {
            var split = keyValuePair.Split(":");
            _ = Properties.TryAdd(split[0], split[1]);
        }

        public bool IsValid(bool ignoreCid, bool part1)
        {
            var isValid = Properties.TryGetValue("byr", out string value)
                && (part1 || CheckBirth(value));

            isValid &= Properties.TryGetValue("iyr", out value)
                && (part1 || CheckIssue(value));

            isValid &= Properties.TryGetValue("eyr", out value)
                && (part1 || CheckExpiration(value));

            isValid &= Properties.TryGetValue("hgt", out value)
                && (part1 || CheckHeight(value));

            isValid &= Properties.TryGetValue("hcl", out value)
                && (part1 || CheckHair(value));

            isValid &= Properties.TryGetValue("ecl", out value)
                && (part1 || CheckEye(value));

            isValid &= Properties.TryGetValue("pid", out value)
                && (part1 || CheckPid(value));

            isValid &= ignoreCid || Properties.TryGetValue("cid", out value);

            return isValid;
        }

        private bool CheckBirth(string value)
        {
            return value.Length == 4 && int.TryParse(value, out int number) && (1920 <= number && number <= 2002);
        }
        private bool CheckIssue(string value)
        {
            return value.Length == 4 && int.TryParse(value, out int number) && (2010 <= number && number <= 2020);
        }
        private bool CheckExpiration(string value)
        {
            return value.Length == 4 && int.TryParse(value, out int number) && (2020 <= number && number <= 2030);
        }
        private bool CheckHeight(string value)
        {
            var text = value.Substring(value.Length - 2);
            bool isValid = int.TryParse(value.Substring(0, value.Length - 2), out int num);
            isValid &= text switch
            {
                "cm" => (150 <= num && num <= 193),
                "in" => (59 <= num && num <= 76),
                _ => false
            };
            return isValid;
        }
        private bool CheckHair(string value)
        {
            // hcl (Hair Color) - a # followed by exactly six characters 0-9 or a-f.
            bool isValid = value[0] == '#';

            var text = value.Substring(1);
            isValid &= text.Length == 6;
            isValid &= HexCanParse(text);
            return isValid;
        }
        private bool CheckEye(string value)
        {
            // ecl (Eye Color) - exactly one of: amb blu brn gry grn hzl oth.
            return value switch
            {
                "amb" or "blu" or "brn" or "gry" or "grn" or "hzl" or "oth" => true,
                _ => false
            };
        }
        private bool CheckPid(string value)
        {
            // pid(Passport ID) - a nine - digit number, including leading zeroes.
            return value.Length == 9 && int.TryParse(value, out _);
        }
        
        public static bool HexCanParse(string value)
        {
            try
            {
                _ = int.Parse(value, System.Globalization.NumberStyles.HexNumber);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }

}
