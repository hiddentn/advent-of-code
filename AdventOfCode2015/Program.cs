﻿using System.Diagnostics;
using AdventOfCode.Abstractions;
using AdventOfCode2015.Day2;

var options = DaySolverOptions.Configure(opt => { opt.InputFilepath = "Day2/input.txt"; });

var solver = new Day2Solver(options);
var watch = new Stopwatch();

watch.Start();
var part1 = solver.SolvePart1();
watch.Stop();
Console.WriteLine($"Part 1: {part1}");
Console.WriteLine($"Time: {watch.ElapsedMilliseconds}ms");
watch.Restart();
var part2 = solver.SolvePart2();
watch.Stop();
Console.WriteLine($"Part 2: {part2}");
Console.WriteLine($"Time: {watch.ElapsedMilliseconds}ms");