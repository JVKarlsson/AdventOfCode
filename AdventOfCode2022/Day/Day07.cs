using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022.Day
{
    internal class Day07
    {
        private string _path;
        public Day07(string path)
        {
            _path = Path.Combine(path, $"Input{this.GetType().Name}.txt");
            PartOne();
            PartTwo();
        }
        public void PartOne()
        {
            var lines = File.ReadAllLines(_path);

            List<DirectoryFile> files = new();
            List<Directory> directories = new();
            Directory currentDir = new() {Name = "/"};
            directories.Add(currentDir);

            for (int i = 1; i < lines.Length; i++)
            {
                var line = lines[i];
                var command = line[2..].Split(' ');

                if (command[0] == "cd")
                {
                    if (command[1] == "..")
                    {
                        currentDir = currentDir.BelongsTo;
                    }
                    else
                    {
                        var dir = currentDir.SubDirectories.First(x => x.Name == command[1]);
                        currentDir = dir;
                    }
                }
                else if (command[0] == "ls")
                {
                    while (lines[i + 1][0] != '$')
                    {
                        i++;
                        var newLine = lines[i];
                        var split = newLine.Split(' ');
                        if (Char.IsNumber(newLine[0]))
                        {
                            var file = new DirectoryFile();
                            file.Name = split[1];
                            file.Size = int.Parse(split[0]);
                            files.Add(file);
                            currentDir.Files.Add(file);
                        }
                        else
                        {
                            var newDir = new Directory()
                            {
                                Name = split[1],
                                BelongsTo = currentDir
                            };
                            currentDir.SubDirectories.Add(newDir);
                            directories.Add(newDir);
                        }
                        if ((i + 1) >= lines.Length)
                        {
                            break;
                        }
                    }
                }
            }

            var result = directories.Where(x => x.TotalSize() <= 100_000).Select(x => x.TotalSize()).Sum();
            Console.WriteLine($"Advent of Code Day 06 part 1 : {result}");
        }

        public void PartTwo()
        {
            var lines = File.ReadAllLines(_path);

            List<DirectoryFile> files = new();
            List<Directory> directories = new();
            Directory currentDir = new() { Name = "/" };
            directories.Add(currentDir);

            for (int i = 1; i < lines.Length; i++)
            {
                var line = lines[i];
                var command = line[2..].Split(' ');

                if (command[0] == "cd")
                {
                    if (command[1] == "..")
                    {
                        currentDir = currentDir.BelongsTo;
                    }
                    else
                    {
                        var dir = currentDir.SubDirectories.First(x => x.Name == command[1]);
                        currentDir = dir;
                    }
                }
                else if (command[0] == "ls")
                {
                    while (lines[i + 1][0] != '$')
                    {
                        i++;
                        var newLine = lines[i];
                        var split = newLine.Split(' ');
                        if (Char.IsNumber(newLine[0]))
                        {
                            var file = new DirectoryFile();
                            file.Name = split[1];
                            file.Size = int.Parse(split[0]);
                            files.Add(file);
                            currentDir.Files.Add(file);
                        }
                        else
                        {
                            var newDir = new Directory()
                            {
                                Name = split[1],
                                BelongsTo = currentDir
                            };
                            currentDir.SubDirectories.Add(newDir);
                            directories.Add(newDir);
                        }
                        if ((i + 1) >= lines.Length)
                        {
                            break;
                        }
                    }
                }
            }

            long totalDiskSpace = 70000000;
            
            long currentUsed = directories.First().TotalSize();
            long currentFree = totalDiskSpace - currentUsed;

            long unusedNeeded = 30000000 - currentFree;

            var result = directories.OrderBy(x => x.TotalSize()).Where(x => x.TotalSize() >= unusedNeeded).First().TotalSize();

            Console.WriteLine($"Advent of Code Day 06 part 2 : {result}");
        }
    }

    internal class Directory
    {
        public string Name { get; set; }
        public Directory BelongsTo { get; set; }
        public List<Directory> SubDirectories { get; set; } = new ();
        public List<DirectoryFile> Files { get; set; } = new();

        public long TotalSize()
        {
            long size = 0;
            Files.ForEach(x => size += x.Size);
            SubDirectories.ForEach(x => size += x.TotalSize());
            return size;
        }
    }

    internal class DirectoryFile
    {
        public string Name { get; set; }
        public long Size { get; set; }
    }
}
