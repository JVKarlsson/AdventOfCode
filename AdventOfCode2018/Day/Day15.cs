using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace AdventOfCode2018.Day
{
    class Day15
    {
        private static string path = "";
        List<Entity> entities = new List<Entity>();
        List<Tile> tiles = new List<Tile>();
        public Day15(string p)
        {
            path = Path.Combine(p, "InputDay15.txt");
            Solve();
        }

        private void Solve()
        {
            var lines = File.ReadAllLines(path).ToList();

            for (int y = 0; y < lines.Count; y++)
            {
                var line = lines[y].ToList();
                for (int x = 0; x < line.Count; x++)
                {
                    Tile tile = new Tile(new int[] { x, y });
                    switch (line[x])
                    {
                        case '#':
                            tile.isWall = true;
                            break;
                        case '.':
                            tile.isWall = false;
                            break;
                        case 'E':
                            tile.isWall = false;
                            tile.isOccupied = true;
                            var elf = new Entity(true, new int[] { x, y });
                            tile.OccupyingEntity = elf;
                            entities.Add(elf);
                            break;
                        case 'G':
                            tile.isWall = false;
                            tile.isOccupied = true;
                            var goblin = new Entity(false, new int[] { x, y });
                            tile.OccupyingEntity = goblin;
                            entities.Add(goblin);
                            break;
                        default:
                            Console.WriteLine("it shouldnt be default");
                            break;
                    }
                    tiles.Add(tile);
                }
            }
            SetNeighbours();

            int counter = 0;
            while (Loop(counter))
            {
                ReOrderEntities();
                counter++;
            }
            var solution = counter * (entities.Select(x => x.HP).Sum());
            var test = solution; //703056
        }

        private bool Loop(int counter)
        {
            if (counter % 100 == 0)
                Console.Clear();
            Console.WriteLine("Round " + counter + " started");
            for (int y = 0; y < 32; y++)
            {
                var line = "";
                for (int x = 0; x < 32; x++)
                {
                    var tile = GetTile(new int[] { x, y });

                    if (tile.isWall)
                        line += '#';
                    else if (tile.isOccupied)
                    {
                        if (tile.OccupyingEntity.isElf)
                            line += 'E';
                        else
                            line += 'G';
                    }
                    else
                        line += '.';
                }
                Console.WriteLine(line);
            }
            var dead = new List<Entity>();
            foreach (var entity in entities)
            {
                if (!(dead.Contains(entity)))
                {
                    var enemies = GetTile(entity.Position).Neighbours.
                        Where(x => x.isOccupied).
                        Where(x => x.OccupyingEntity.isElf != entity.isElf).
                        Select(y => y.OccupyingEntity).ToList();

                    if (enemies.Count > 0)
                    {
                        var min = enemies.Min(x => x.HP);
                        var enemy = enemies.Where(x => x.HP == min).First();
                        enemy.HP -= entity.Attack;
                        if (enemy.HP < 1)
                        {
                            var type = enemy.isElf == true ? "Elf" : "Goblin";
                            var grammar = enemy.isElf == true ? "An" : "A";
                            Console.WriteLine("{0} {1} was killed", grammar, type);

                            var tile = GetTile(enemy.Position);
                            tile.OccupyingEntity = null;
                            tile.isOccupied = false;

                            dead.Add(enemy);
                            //entities.Remove(enemy);
                        }
                    }
                    else
                    {
                        enemies = entities.Where(y => !(dead.Contains(y))).Where(x => x.isElf != entity.isElf).ToList();
                        if (enemies.Count < 1)
                            return false;
                        MoveTowardsReachableAdjacentEnemyTiles(entity);
                    }
                }
            }
            foreach (var d in dead)
                entities.Remove(d);

            return true;
            //return (entities.Count(x => x.isElf == true) > 0) && (entities.Count(x => x.isElf == false) > 0);
        }

        private Tile GetTile(int[] pos)
        {
            return tiles.Where(x => x.Postition[0] == pos[0] && x.Postition[1] == pos[1]).FirstOrDefault();
        }

        private void MoveTowardsReachableAdjacentEnemyTiles(Entity entity)
        {
            var adjacentTiles = tiles.Where(x => x.isOccupied).
                                        Where(y => y.OccupyingEntity.isElf != entity.isElf).
                                        SelectMany(z => z.Neighbours).
                                        Where(v => v.isOccupied == false).ToList();

            var nearestTile = WalkToNearest(GetTile(entity.Position), adjacentTiles);

            if (nearestTile != null)
            {
                var currentTile = GetTile(entity.Position);
                currentTile.isOccupied = false;
                currentTile.OccupyingEntity = null;

                nearestTile.isOccupied = true;
                nearestTile.OccupyingEntity = entity;

                entity.Position = nearestTile.Postition;
            }
            else
            {
                var test = 1;
            }
        }


        private List<Tile> GetReachableTiles(Tile start, List<Tile> adjacentTiles)
        {
            var visited = new List<Tile>();
            var stack = new List<Tile>();
            var reachable = new List<Tile>();
            stack.Add(start);

            while (stack.Count > 0)
            {
                var tile = stack.First();
                visited.Add(tile);
                stack.Remove(tile);

                var adjacent = tile.Neighbours.Where(x => !(x.isOccupied || x.isWall)).ToList();
                adjacent = adjacent.Where(y => !(visited.Contains(y))).ToList();
                foreach (var adj in adjacent)
                {
                    if (!(stack.Contains(adj)))
                        stack.Add(adj);
                    if (adjacentTiles.Contains(adj) && !(reachable.Contains(adj)))
                        reachable.Add(adj);
                }
            }
            if (reachable.Count < 1)
            {
                var test = 1;
            }
            return reachable;
        }

        private Tile WalkToNearest(Tile start, List<Tile> adjacentTiles)
        {
            Tile chosenTile = null;
            var reachable = GetReachableTiles(start, adjacentTiles);
            var steps = start.Neighbours.Where(x => !(x.isOccupied || x.isWall)).ToList();

            if (reachable.Count > 0)
            {
                if (steps.Count > 1)
                {
                    int index = -1;
                    int stepCount = int.MaxValue;
                    for (int i = 0; i < steps.Count; i++)
                    {
                        int count = GetLowest(start, steps[i], reachable);
                        if (count < stepCount)
                        {
                            index = i;
                            stepCount = count;
                        }
                    }
                    if (index >= 0)
                        chosenTile = steps[index];
                    else
                    {
                        var test = 0;
                    }
                }
                else
                    chosenTile = steps.FirstOrDefault();
            }
            return chosenTile;
        }

        private /*Dictionary<Tile, int>*/ int GetLowest(Tile start, Tile step, List<Tile> reachable)
        {
            var temp = new Dictionary<Tile, int>();
            foreach (var reach in reachable)
            {
                var visited = new Dictionary<Tile, int>();
                var stack = new Dictionary<Tile, int>();
                visited.Add(start, -1);
                stack.Add(step, 0);
                while (stack.Count > 0)
                {
                    var tile = stack.First();
                    visited.Add(tile.Key, tile.Value);
                    stack.Remove(tile.Key);

                    var adjacent = tile.Key.Neighbours.Where(x => !(x.isOccupied || x.isWall)).ToList();
                    adjacent = adjacent.Where(y => !(visited.ContainsKey(y))).ToList();
                    foreach (var adj in adjacent)
                    {
                        if (!(stack.ContainsKey(adj)))
                            stack.Add(adj, tile.Value + 1);
                        if (reach.Equals(adj) && !(temp.ContainsKey(adj)))
                            temp.Add(adj, tile.Value + 1);
                    }
                }
            }

            var result = int.MaxValue;
            if (temp.Count > 0)
                result = temp.Min(x => x.Value);
            return result;
            //Dictionary<Tile, int> chosen = null;
            //if (temp.Count > 0)
            //{
            //    var min = temp.Min(x => x.Value);
            //    var minTiles = temp.Where(x => x.Value == min).ToDictionary(t => t.Key, t => t.Value);

            //    if (minTiles.Count > 0)
            //    {
            //        var Y = minTiles.Min(x => x.Key.Postition[1]);
            //        var X = minTiles.Where(x => x.Key.Postition[1] == Y).Min(x => x.Key.Postition[0]);
            //        chosen = minTiles.Where(x => x.Key.Postition[0] == X && x.Key.Postition[1] == Y).ToDictionary(t => t.Key, t => t.Value);
            //    }
            //}
            //return chosen;
        }

        private List<Tile> asdasdasd(Tile start, List<Tile> adjacentTiles)
        {
            var visited = new Dictionary<Tile, int>();
            var stack = new Dictionary<Tile, int>();
            var reachable = new Dictionary<Tile, int>();
            stack.Add(start, 0);
            // maybe do this for each adjacent tile it got and limit it if it only can walk one direction
            while (stack.Count > 0)
            {
                var tile = stack.First();
                visited.Add(tile.Key, tile.Value);
                stack.Remove(tile.Key);

                var adjacent = tile.Key.Neighbours.Where(x => !(x.isOccupied || x.isWall)).ToList();
                adjacent = adjacent.Where(y => !(visited.ContainsKey(y))).ToList();
                foreach (var adj in adjacent)
                {
                    if (!(stack.ContainsKey(adj)))
                        stack.Add(adj, tile.Value + 1);
                    if (adjacentTiles.Contains(adj) && !(reachable.ContainsKey(adj)))
                        reachable.Add(adj, tile.Value + 1);
                }
            }
            // 10,19  9,18
            var min = reachable.Min(x => x.Value);
            var minTiles = reachable.Where(x => x.Value == min).ToDictionary(t => t.Key, t => t.Value);

            var asList = reachable.Select(x => x.Key).ToList();
            return asList;
        }

        private void ReOrderEntities()
        {
            entities = entities.OrderBy(y => y.Position[1]).ThenBy(x => x.Position[0]).ToList();
        }

        private void SetNeighbours()
        {
            foreach (var tile in tiles)
            {
                tile.Neighbours = new List<Tile>();

                var x = tile.Postition[0];
                var y = tile.Postition[1];

                var N = GetTile(new int[] { x, y - 1 });
                if (N != null)
                    tile.Neighbours.Add(N);

                var W = GetTile(new int[] { x - 1, y });
                if (W != null)
                    tile.Neighbours.Add(W);

                var E = GetTile(new int[] { x + 1, y });
                if (E != null)
                    tile.Neighbours.Add(E);

                var S = GetTile(new int[] { x, y + 1 });
                if (S != null)
                    tile.Neighbours.Add(S);


            }
        }
    }


    class Tile
    {
        public bool isWall { get; set; }
        public bool isOccupied { get; set; }
        public int[] Postition { get; set; }
        public Entity OccupyingEntity { get; set; }
        public List<Tile> Neighbours { get; set; }
        public Tile(int[] pos)
        {
            Postition = pos;
            isOccupied = false;
        }
    }

    class Entity
    {
        public bool isElf { get; set; }
        public int HP { get; set; }
        public int Attack { get; set; }
        public int[] Position { get; set; }

        public Entity(bool elf, int[] pos)
        {
            isElf = elf;
            Position = pos;
            HP = 300;
            Attack = 3;
        }
    }
}




