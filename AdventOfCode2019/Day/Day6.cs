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
            var nodeDictionary = new Dictionary<string, Node>();

            foreach (var item in list)
            {
                var nodes = item.Split(")");
                var targetNode = new Node(nodes[0], null);
                var orbiterNode = new Node(nodes[1], targetNode);

                _ = nodeDictionary.TryAdd(targetNode.Name, targetNode);
                if (!nodeDictionary.TryAdd(orbiterNode.Name, orbiterNode))
                {
                    nodeDictionary[orbiterNode.Name].Orbiting = nodeDictionary[targetNode.Name];
                }
                //nodeDictionary.TryAdd(orbiterNode.Name, orbiterNode);

            }

            var sum = nodeDictionary.Sum(x => x.Value.OrbitCount);
            return sum;
        }

        private int PartTwo()
        {
            var list = File.ReadAllLines(_path).ToList();
            var nodeDictionary = new Dictionary<string, Node>();
            foreach (var item in list)
            {
                var nodes = item.Split(")");
                var targetNode = new Node(nodes[0], null);
                var orbiterNode = new Node(nodes[1], targetNode);

                _ = nodeDictionary.TryAdd(targetNode.Name, targetNode);
                _ = nodeDictionary.TryAdd(orbiterNode.Name, orbiterNode);

                nodeDictionary[orbiterNode.Name].Orbiting = nodeDictionary[targetNode.Name];
            }

            // Del 2
            Node meetup = null;

            var pointer = nodeDictionary["YOU"];
            while (pointer.Orbiting != null)
            {
                pointer = pointer.Orbiting;
                pointer.Visited = true;
            }

            pointer = nodeDictionary["SAN"];
            while (pointer.Orbiting != null)
            {
                pointer = pointer.Orbiting;
                if (pointer.Visited)
                {
                    meetup = pointer;
                    break;
                }
            }

            var sum = 
                nodeDictionary["YOU"].OrbitCount 
                + nodeDictionary["SAN"].OrbitCount 

                // ta bort distansen från mötesplatsen till COM och multplicera med 2 (YOU + SAN)
                - (2 * meetup.OrbitCount)

                // "Between the objects they are orbiting - not between YOU and SAN"
                - 2;

            return sum;
        }
    }

    class Node
    {
        public Node Orbiting;
        public string Name;

        // Del 2 property
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
