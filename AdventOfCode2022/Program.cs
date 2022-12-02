using AdventOfCode.Abstractions;
using AdventOfCode2022.Day1;

var options = DaySolverOptions.Configure(opt => { opt.InputFilepath = "Day1/input.txt"; });

var solver = new Day1(options);

Console.WriteLine($"Part 1: {solver.SolvePart1()}");
Console.WriteLine($"Part 2: {solver.SolvePart2()}");
