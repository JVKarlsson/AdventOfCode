using AdventOfCode2023.Day;

namespace AdventOfCode2023;

internal class Program
{
    static void Main(string[] args)
    {
        string path = Path.Combine(Path.GetFullPath(@"..\..\..\"), "Inputs");
        _ = new Day01(path);
        Console.ReadLine();
    }
}
