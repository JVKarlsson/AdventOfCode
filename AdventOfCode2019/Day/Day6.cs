using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventOfCode2019.Day
{
    class Day6
    {
        private string _path = "";

        public Day6(string path)
        {
            _path = System.IO.Path.Combine(path, "InputDay6.txt");
            Console.WriteLine("Advent of Code Day 6 part 1 : Orbit sum = " + PartOne());
            Console.WriteLine("Advent of Code Day 6 part 2 : Orbit sum = " + PartTwo());
        }

        private int PartOne()
        {
            var list = File.ReadAllLines(_path).ToList();
            //var test = new List<string> { "COM)B", "B)C", "C)D", "D)E", "E)F", "B)G", "G)H", "D)I", "E)J", "J)K", "K)L" };

            var objects = new Dictionary<string, Node>();
            foreach (var item in list)
            {
                var nodes = item.Split(")");
                var targetNode = new Node(nodes[0], null);
                var orbiter = new Node(nodes[1], targetNode);

                _ = objects.TryAdd(targetNode.Name, targetNode);
                _ = objects.TryAdd(orbiter.Name, orbiter);
                objects[orbiter.Name].Orbiting = objects[targetNode.Name];
            }

            var sum = objects.Sum(x => x.Value.OrbitCount);
            return sum;
        }

        private int PartTwo()
        {
            var list = File.ReadAllLines(_path).ToList();
            //var test = new List<string> { "COM)B","B)C","C)D","D)E","E)F","B)G","G)H","D)I","E)J","J)K","K)L","K)YOU","I)SAN"};
            var objects = new Dictionary<string, Node>();
            foreach (var item in list)
            {
                var nodes = item.Split(")");
                var targetNode = new Node(nodes[0], null);
                var orbiter = new Node(nodes[1], targetNode);

                _ = objects.TryAdd(targetNode.Name, targetNode);
                _ = objects.TryAdd(orbiter.Name, orbiter);
                objects[orbiter.Name].Orbiting = objects[targetNode.Name];
            }

            Node meetup = null;
            var pointer = objects["YOU"];
            while (pointer.Orbiting != null)
            {
                pointer = pointer.Orbiting;
                pointer.Visited = true;
            }
            pointer = objects["SAN"];
            while (pointer.Orbiting != null)
            {
                pointer = pointer.Orbiting;
                if (pointer.Visited)
                {
                    meetup = pointer;
                    break;
                }
            }

            var sum = objects["YOU"].OrbitCount + objects["SAN"].OrbitCount - (2 * meetup.OrbitCount) - 2;
            return sum;
        }
    }

    class Node
    {
        public Node Orbiting;
        public string Name;
        public bool Visited { get; set; } = false;
        public Node(string name, Node orbiting)
        {
            Name = name;
            Orbiting = orbiting;
        }

        public int OrbitCount
        {
            get
            {
                return Orbiting?.OrbitCount + 1 ?? 0;
            }
        }
    }
}
