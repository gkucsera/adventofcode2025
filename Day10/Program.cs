// See https://aka.ms/new-console-template for more information

using System.Diagnostics;
using Day10;

var lines = File.ReadAllLines("input.txt");
var machines = lines.Select(item =>
{
    var split = item.Split(' ');
    var pattern = split[0][1..^1];
    var buttons = split[1..^1];
    return new Machine(pattern, buttons, split.Last());
}).ToList();

var stopwatch = new Stopwatch();
stopwatch.Start();
Parallel.ForEach(machines, machine =>
{
    machine.FindBest();
});
var result = machines.Sum(item=> item.ButtonMap[item.Pattern]);
stopwatch.Stop();
Console.WriteLine($"{result} -  {stopwatch.ElapsedMilliseconds} ms");

stopwatch.Reset();


// stopwatch.Start();
// Parallel.ForEach(machines, machine =>
// {
//     machine.FindBestJoltage();
// });
var i = 1;
foreach (var machine in machines)
{
    stopwatch.Start();
    machine.FindBestJoltage();
    stopwatch.Stop();
    Console.WriteLine($"{i++} - {stopwatch.ElapsedMilliseconds} ms");
}
result = machines.Sum(item=> item.ButtonMap[item.JoltagePattern]);
stopwatch.Stop();
Console.WriteLine($"{result} -  {stopwatch.ElapsedMilliseconds} ms");