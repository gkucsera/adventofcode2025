// See https://aka.ms/new-console-template for more information

var input = File.ReadAllLines("input.txt");
var result = 0L;

foreach (var line in input)
{
    var numbers = line.Select(item => int.Parse(item.ToString())).ToArray();

    var firstMax = numbers[0];
    var firstIndex = 0;
    var secondMax = 0;
    for (var i = 1; i < numbers.Length - 1; i++)
    {
        if (numbers[i] > firstMax)
        {
            firstMax = numbers[i];
            firstIndex = i;
        }
    }

    for (var i = firstIndex + 1; i < numbers.Length; i++)
    {
        if (numbers[i] > secondMax)
        {
            secondMax = numbers[i];
        }
    }

    result += firstMax * 10 + secondMax;
}

Console.WriteLine(result);

result = 0L;
Parallel.ForEach(input, line =>
{
    var numbers = line.Select(item => int.Parse(item.ToString())).ToArray();

    var resultNumbers = new int[12];
    var startIndex = -1;

    for (var number = 0; number < 12; number++)
    {
        var endIndex = numbers.Length - 12 + number;
        for (var i = startIndex + 1; i < endIndex + 1; i++)
        {
            if (numbers[i] > resultNumbers[number])
            {
                resultNumbers[number] = numbers[i];
                startIndex = i;
            }
        }
    }

    var currentMax = long.Parse(string.Join("", resultNumbers));
    Interlocked.Add(ref result, currentMax);
});
Console.WriteLine(result);