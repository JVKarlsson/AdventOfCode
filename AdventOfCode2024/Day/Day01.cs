namespace AdventOfCode2024.Day;

internal class Day01
{
    private readonly string _path;
    public Day01(string path)
    {
        _path = Path.Combine(path, $"Input{this.GetType().Name}.txt");
        PartOne();
        PartTwo();
    }
    private void PartOne()
    {
        var result = File.ReadAllLines(_path)
                .Select(x => x.Split(' ', StringSplitOptions.RemoveEmptyEntries))
                .ToArray();

        var list1 = new List<int>();
        var list2 = new List<int>();
        foreach (var item in result)
        {
            list1.Add(int.Parse(item[0]));
            list2.Add(int.Parse(item[1]));
        }
        list1 = [.. list1.OrderBy(x => x)];
        list2 = [.. list2.OrderBy(x => x)];

        var listOfDiffs = new List<int>();
        for (var i = 0; i < list1.Count; i++)
        {
            listOfDiffs.Add(Math.Abs(list2[i] - list1[i]));
        }
        var diff = listOfDiffs.Sum();

        Console.WriteLine(diff);
    }

    private void PartTwo()
    {
        var result = File.ReadAllLines(_path)
                .Select(x => x.Split(' ', StringSplitOptions.RemoveEmptyEntries))
                .ToArray();

        var listOfDiffs = new List<int>();
        var occurances = result.Select(x => int.Parse(x[1])).GroupBy(x => x).ToDictionary(x => x.Key, x => x.Count());
        foreach (var item in result)
        {
            var value = int.Parse(item[0]);
            var occurance = occurances.GetValueOrDefault(value, 0);
            listOfDiffs.Add(value * occurance);
        }

        var diff = listOfDiffs.Sum();
        Console.WriteLine(diff);
    }

}
