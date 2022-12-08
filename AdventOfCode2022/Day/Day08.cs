using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022.Day
{
    internal class Day08
    {
        private string _path;
        public Day08(string path)
        {
            _path = Path.Combine(path, $"Input{this.GetType().Name}.txt");
            PartOne();
            PartTwo();
        }
        public void PartOne()
        {
            var lines = File.ReadAllLines(_path);
            List<Tree> treeList = new();
            var trees = GetTreeAsArray(treeList, lines);
            var result = treeList.Where(x => x.IsVisible(trees)).Count();
            Console.WriteLine($"Advent of Code Day 08 part 1 : {result}");
        }

        public void PartTwo()
        {
            var lines = File.ReadAllLines(_path);
            List<Tree> treeList = new();
            var trees = GetTreeAsArray(treeList, lines);
            var innerMostTrees = treeList.Where(x
                => x.Position.X > 0
                && x.Position.Y > 0
                && x.Position.X < trees.GetLength(0) - 1
                && x.Position.Y < trees.GetLength(1) - 1).ToList();
            var result = innerMostTrees.Select(x => x.ScenicScore(trees)).Max();

            Console.WriteLine($"Advent of Code Day 08 part 2 : {result}");
        }

        public Tree[,] GetTreeAsArray(List<Tree> treeList, string[] lines)
        {
            var trees = new Tree[lines[0].Length, lines.Length];
            for (int i = 0; i < lines.Length; i++)
            {
                for (int j = 0; j < lines[i].Length; j++)
                {
                    trees[i, j] = new Tree
                    {
                        Height = int.Parse(lines[i][j].ToString()),
                        Position = (i, j)
                    };
                    treeList.Add(trees[i, j]);
                }
            }
            return trees;
        }
    }

    internal class Tree
    {
        public int Height { get; set; }
        public (int X, int Y) Position { get; set; }

        public bool IsVisible(Tree[,] trees)
        {
            var left = IsVisibleFromDirection(trees, (0, -1));
            var right = left || IsVisibleFromDirection(trees, (0, 1));
            var up = right || IsVisibleFromDirection(trees, (-1, 0));
            var down = up || IsVisibleFromDirection(trees, (1, 0));
            return down;
        }

        public long ScenicScore(Tree[,] trees)
        {
            var left = ScenicScoreFromDirection(trees, (0, -1));
            var right = ScenicScoreFromDirection(trees, (0, 1));
            var up = ScenicScoreFromDirection(trees, (-1, 0));
            var down = ScenicScoreFromDirection(trees, (1, 0));
            return left * right *  up * down;
        }

        private bool IsVisibleFromDirection(Tree[,] trees, (int X, int Y) direction)
        {
            (int X, int Y) checkPosition = Position;
            while (checkPosition.X > 0
                && checkPosition.Y > 0
                && checkPosition.X < trees.GetLength(0) - 1
                && checkPosition.Y < trees.GetLength(1) - 1)
            {
                checkPosition.X += direction.X;
                checkPosition.Y += direction.Y;
                if (Height <= trees[checkPosition.X, checkPosition.Y].Height)
                {
                    return false;
                }
            }
            return true;
        }

        private long ScenicScoreFromDirection(Tree[,] trees, (int X, int Y) direction)
        {
            (int X, int Y) checkPosition = Position;
            int count = 0;
            while (checkPosition.X > 0
                && checkPosition.Y > 0
                && checkPosition.X < trees.GetLength(0) - 1
                && checkPosition.Y < trees.GetLength(1) - 1)
            {
                checkPosition.X += direction.X;
                checkPosition.Y += direction.Y;
                if (Height <= trees[checkPosition.X, checkPosition.Y].Height)
                {
                    count++;
                    break;
                }
                count++;
            }
            return count;
        }
    }
}
