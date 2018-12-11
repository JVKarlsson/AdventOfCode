using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2018.Day
{
    class Day7
    {
        private static string path = "";
        public Day7(string p)
        {
            path = Path.Combine(p, "InputDay7.txt");
            //Console.WriteLine("Advent of Code Day 7 part 1 : " + PartOne());
            Console.WriteLine("Advent of Code Day 7 part 2 : " + PartTwo());
        }

        private string PartOne()
        {
            var Nodes = new Dictionary<char, Node>();
            var order = "";
            var lines = File.ReadAllLines(path).ToList();
            foreach (var line in lines)
            {
                var id = line.ElementAt(5);
                var parent = new Node(id);
                if (!Nodes.ContainsKey(id))
                    Nodes.Add(id, parent);
                else
                    parent = Nodes[id];


                var childId = line.ElementAt(36);
                var child = new Node(childId);
                if (!Nodes.ContainsKey(childId))
                    Nodes.Add(childId, child);
                else
                    child = Nodes[childId];

                parent.Children.Add(child);
                child.Parents.Add(parent);
            }

            Nodes = OrderDictionary(Nodes);

            while (Nodes.Count > 0)
            {
                var parent = Nodes.First();
                foreach (var child in parent.Value.Children)
                {
                    child.Parents.Remove(parent.Value);
                }

                order += (parent.Key);
                Nodes.Remove(parent.Key);
                Nodes = OrderDictionary(Nodes);
            }

            return order;
        }


        private int PartTwo()
        {
            var Nodes = new Dictionary<char, Node>();
            var Workers = new List<Worker>();
            var lines = File.ReadAllLines(path).ToList();


            for (int i = 0; i < 5; i++)
                Workers.Add(new Worker(i));

            foreach (var line in lines)
            {
                var id = line.ElementAt(5);
                var parent = new Node(id);
                if (!Nodes.ContainsKey(id))
                    Nodes.Add(id, parent);
                else
                    parent = Nodes[id];


                var childId = line.ElementAt(36);
                var child = new Node(childId);
                if (!Nodes.ContainsKey(childId))
                    Nodes.Add(childId, child);
                else
                    child = Nodes[childId];

                parent.Children.Add(child);
                child.Parents.Add(parent);
            }
            Nodes = OrderDictionary(Nodes);

            var Taken = new Dictionary<char, Node>();
            var time = 0;
            while (Nodes.Count > 0)
            {
                time++;
                foreach (var worker in Workers)
                {
                    if (worker.WorkItem == '-')
                    {
                        foreach (var node in Nodes)
                        {
                            if (node.Value.Parents.Count == 0)
                            {
                                if (!(Taken.ContainsKey(node.Key)))
                                {
                                    Taken.Add(node.Key, node.Value);
                                    worker.WorkItem = node.Key;
                                    worker.TimeLeft = 60 + node.Key-64;
                                    break;
                                }
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                }

                foreach (var worker in Workers)
                {
                    worker.TimeLeft--;
                    if (worker.TimeLeft == 0)
                    {
                        var n = Nodes[worker.WorkItem];
                        foreach (var child in n.Children)
                        {
                            child.Parents.Remove(n);
                        }

                        Nodes.Remove(n.id);
                        worker.WorkItem = '-';
                        Nodes = OrderDictionary(Nodes);
                    }
                }
            }
            return time;
        }
        
        private Dictionary<char, Node> OrderDictionary(Dictionary<char, Node> Nodes)
        {
            var temp = new Dictionary<char, Node>();
            foreach (var node in Nodes.OrderBy(x => x.Value.Parents.Count).ThenBy(y => y.Key))
            {
                temp.Add(node.Key, node.Value);
            }
            return temp;
        }
    }

    class Worker
    {
        public int id { get; set; }
        public char WorkItem { get; set; }
        public int TimeLeft { get; set; }

        public Worker(int i)
        {
            id = i;
            TimeLeft = 0;
            WorkItem = '-';
        }
    }

    class Node
    {
        public char id { get; set; }
        public List<Node> Parents { get; set; }
        public List<Node> Children { get; set; }

        public Node(char c)
        {
            id = c;
            Parents = new List<Node>();
            Children = new List<Node>();
        }
    }
}
