// See https://aka.ms/new-console-template for more information

var input = File.ReadAllLines("input.txt").Select(item => item.Split(" ", StringSplitOptions.RemoveEmptyEntries)).ToArray();

var result = 0L;
for (var row = 0; row < input[0].Length; row++)
{
    var isAddition = input[^1][row] == "+";
    var currentResult = long.Parse(input[0][row]);
    for (var i = 1; i < input.Length - 1; i++)
    {
        var number = int.Parse(input[i][row]);
        currentResult = isAddition ? currentResult + number : currentResult * number;
    }

    result += currentResult;
}

Console.WriteLine(result);

var map = File.ReadAllLines("input.txt");

result = 0L;

var numbers = new List<int>();
for (var column = map[0].Length - 1; column >= 0; column--)
{
    var numberString = string.Join("", Enumerable.Range(0, map.Length - 1).Select(i => map[i][column])).Trim();
    var currentNumber = int.Parse(numberString);
    numbers.Add(currentNumber);
    if (map[^1][column] == ' ')
    {
        continue;
    }

    var isAddition = map[^1][column] == '+';
    var currentResult = 0L;
    foreach (var number in numbers)
    {
        if (currentResult == 0)
        {
            currentResult = number;
        }
        else
        {
            currentResult = isAddition ? currentResult + number : currentResult * number;
        }
    }

    result += currentResult;

    numbers.Clear();
    column--;
}


Console.WriteLine(result);