﻿using AdventOfCode._2024.Day5;
using AdventOfCode.Common;


var options = DaySolverOptions.Configure(opt => { opt.InputFilepath = "2024/Day5/input.txt"; });
var solver = new Day5Solver(options);
var watch = Stopwatch.StartNew();
var part1 = solver.SolvePart1();
watch.Stop();


Console.WriteLine($"Part 1: {part1}");
Console.WriteLine($"Time: {watch.ElapsedMilliseconds}ms");
watch.Restart();

var part2 = solver.SolvePart2();
watch.Stop();
Console.WriteLine($"Part 2: {part2}");
Console.WriteLine($"Time: {watch.ElapsedMilliseconds}ms");
