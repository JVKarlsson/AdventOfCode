using AdventOfCode2022.Day;

namespace AdventOfCode2022
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string path = Path.Combine(Path.GetFullPath(@"..\..\..\"), "Inputs");

            //_ = new Day01(path);
            //_ = new Day02(path);
            //_ = new Day03(path);
            //_ = new Day04(path);
            //_ = new Day05(path);
            //_ = new Day06(path);
            _ = new Day07(path);

            Console.ReadLine();
        }
    }
}