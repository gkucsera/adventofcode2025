Console.WriteLine("Day 1");
var input = File.ReadAllLines("input.txt");

var index = 50;
const int max = 99;
var result = 0;

foreach (var instruction in input)
{
    var direction = instruction[0];
    var length = int.Parse(instruction[1..]) % 100;
    var diff = direction switch
    {
        'R' => 1,
        'L' => -1,
        _ => throw new Exception()
    };

    for (var i = 0; i < length; i++)
    {
        index += diff;
        if (index < 0)
        {
            index = max;
        }
        else if (index > max)
        {
            index = 0;
        }
    }

    if (index == 0)
    {
        result++;
    }
}

Console.WriteLine($"Result: {result}");


index = 50;
result = 0;

foreach (var instruction in input)
{
    var direction = instruction[0];
    var length = int.Parse(instruction[1..]);
    var diff = direction switch
    {
        'R' => 1,
        'L' => -1,
        _ => throw new Exception()
    };

    for (var i = 0; i < length; i++)
    {
        index += diff;
        if (index < 0)
        {
            index = max;
        }
        else if (index > max)
        {
            index = 0;
        }


        if (index == 0)
        {
            result++;
        }
    }
}

Console.WriteLine($"Result: {result}");