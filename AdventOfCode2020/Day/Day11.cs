using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020.Day
{
    class Day11
    {
        private readonly string _path;


        public Day11(string path)
        {
            _path = Path.Combine(path, "InputDay11.txt");

            PartOne();
            PartTwo();
        }

        public void PartOne()
        {
            var arr = File.ReadAllLines(_path).Select(x => x.Select(y => y).ToArray()).ToArray();
            while (MovePeople(arr)) { }
            var occupiedSeats = arr.Select(x => x.Select(y => y == '#' ? 1 : 0).Sum()).Sum();

            Console.WriteLine($"Advent of Code Day 11 part 1 : {occupiedSeats}");
        }

        public void PartTwo()
        {
            var input = File.ReadAllLines(_path).ToList();

            var nodes = input.Select((x, xpos) => x.Select((y, yPos) => new Node((xpos, yPos), y)).ToArray()).ToArray();
            nodes.ToList().ForEach(y => y.ToList().ForEach(x => x.AssignNeighboursPart2(nodes)));

            while (MovePeople2(nodes)) { }
            var occupiedSeats = nodes.Select(x => x.Select(y => y.CurrentType == NodeType.occupied ? 1 : 0).Sum()).Sum();

            Console.WriteLine($"Advent of Code Day 11 part 2 : {occupiedSeats}");
        }

        private bool MovePeople(char[][] arr)
        {
            char[][] copy = arr.Select(x => x.Select(y => y).ToArray()).ToArray();

            var changed = false;
            for (int x = 0; x < arr.Length; x++)
            {
                for (int y = 0; y < arr[x].Length; y++)
                {
                    char node = arr[x][y];
                    if (node != '.')
                    {
                        var sum = CheckAdjacent(x, y, copy);
                        var newNode = node == 'L' ?
                                            sum == 0 ? '#' : 'L' :
                                            sum < 4 ? '#' : 'L';

                        changed |= node != newNode;
                        arr[x][y] = newNode;
                    }
                }
            }
            return changed;
        }

        private int CheckAdjacent(int x, int y, char[][] arr)
        {
            var takeX = x > 0 ? 3 : 2;
            var takeY = y > 0 ? 3 : 2;

            var sum = arr.Skip(x - 1).Take(takeX)
                            .Select(subArr => subArr.Skip(y - 1).Take(takeY))
                            .Sum(x => x.Count(y => y == '#'));

            sum -= arr[x][y] == '#' ? 1 : 0;
            return sum;
        }

        private bool MovePeople2(Node[][] nodes)
        {
            for (int x = 0; x < nodes.Length; x++)
            {
                for (int y = 0; y < nodes[x].Length; y++)
                {
                    var node = nodes[x][y];
                    if (node.CurrentType != NodeType.floor)
                    {
                        var num = node.CheckAdjacents();
                        node.Nextype = node.CurrentType switch
                        {
                            NodeType.vacant => num == 0 ? NodeType.occupied : NodeType.vacant,
                            NodeType.occupied => num > 4 ? NodeType.vacant : NodeType.occupied,
                            _ => throw new NotImplementedException()
                        };
                    }
                }
            }
            var asd = nodes.Select(x => x.Where(y => y.SwapType()).Count()).Sum();
            return asd > 0;
        }
    }



    class Node
    {
        public NodeType CurrentType { get; set; }
        public NodeType Nextype { get; set; }
        public Node[] AdjacentSeats { get; set; }
        public (int, int) Position { get; set; }

        public Node((int, int) pos, char type)
        {
            Position = pos;
            CurrentType = type switch
            {
                '.' => NodeType.floor,
                'L' => NodeType.vacant,
                '#' => NodeType.occupied,
                _ => NodeType.wall
            };
            Nextype = CurrentType;
        }

        public void AssignNeighboursPart1(Node[][] nodes)
        {
            var directions = new List<(int, int)> { (-1, -1), (-1, 0), (-1, 1), (0, -1), (0, 1), (1, -1), (1, 0), (1, 1) };
            var test = directions.Select((x, i) => nodes.ElementAtOrDefault(Position.Item1 * x.Item1)?.ElementAtOrDefault(Position.Item2 * x.Item2)).ToArray();
            AdjacentSeats = test;
        }

        public void AssignNeighboursPart2(Node[][] nodes)
        {
            var directions = new List<(int, int)> { (-1, -1), (-1, 0), (-1, 1), (0, -1), (0, 1), (1, -1), (1, 0), (1, 1) };
            AdjacentSeats = directions.Select(x => AssignNeighbourInDirection(x, nodes)).ToArray();
        }
        private Node AssignNeighbourInDirection((int, int) dir, Node[][] nodes, int step = 0)
        {
            step++;
            var node = nodes.ElementAtOrDefault(Position.Item1 + step * dir.Item1)?.ElementAtOrDefault(Position.Item2 + step * dir.Item2);

            if (node == null)
                node = new Node((-1, -1), 'n');
            else if (node.CurrentType == NodeType.floor)
                node = AssignNeighbourInDirection(dir, nodes, step);
            return node;
        }

        public int isOccupied() => CurrentType == NodeType.occupied ? 1 : 0;
        public int CheckAdjacents()
        {
            var test = AdjacentSeats;
            var occupiedSeats = AdjacentSeats.Where(x => x != null).Select(x => x.isOccupied()).Sum();
            return occupiedSeats;
        }

        public bool SwapType()
        {
            var swapped = CurrentType != Nextype;
            CurrentType = Nextype;
            return swapped;
        }


    }
    public enum NodeType { floor, vacant, occupied, wall }
}
