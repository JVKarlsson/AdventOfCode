using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

            for (int i = 0; i < lines.Count; i++)
            {
                var line = lines[i].ToList();
                for (int j = 0; j < line.Count; j++)
                {
                    Tile tile = new Tile(new int[] { i, j });
                    switch (line[j])
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
                            var elf = new Entity(true, new int[] { i, j });
                            tile.OccupyingEntity = elf;
                            entities.Add(elf);
                            break;
                        case 'G':
                            tile.isWall = false;
                            tile.isOccupied = true;
                            var goblin = new Entity(false, new int[] { i, j });
                            tile.OccupyingEntity = goblin;
                            entities.Add(goblin);
                            break;
                        default:
                            var test = 0;
                            break;
                    }
                    tiles.Add(tile);
                }
            }

            int counter = 0;
            while (Loop())
            {
                ReOrderEntities();
                counter++;
            }
            var solution = counter * (entities.Select(x => x.HP).Sum());
            
        }

        private bool Loop()
        {
            foreach (var entity in entities)
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
                        // fix death
                        var tile = GetTile(enemy.Position);
                        tile.OccupyingEntity = null;
                        tile.isOccupied = false;
                        entities.Remove(enemy);
                    }
                }
                else
                {
                    enemies = entities.Where(x => x.isElf != entity.isElf).ToList();
                    if (enemies.Count < 1)
                        return false;

                    // get all awailable tiles next to enemies
                    // are they reachable?
                    // pick the path with the fewest steps
                }
            }
            return true;
            //return (entities.Count(x => x.isElf == true) > 0) && (entities.Count(x => x.isElf == false) > 0);
        }
        
        private Tile GetTile(int[] pos)
        {
            return tiles.Where(x => x.Postition == pos).FirstOrDefault();
        }
        private void ReOrderEntities()
        {
            entities = entities.OrderBy(y => y.Position[1]).ThenBy(x => x.Position[0]).ToList();
        }

        private void SetNeighbours(List<Tile> tiles)
        {
            foreach (var tile in tiles)
            {
                tile.Neighbours = new List<Tile>();

                var x = tile.Postition[0];
                var y = tile.Postition[1];

                for (int i = 0; i < 4; i++)
                {
                    var neigh = tiles.ElementAtOrDefault(i);
                    if (neigh != null && !(neigh.isWall))
                    {

                    }
                    else
                    {

                    }
                }
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
