// See https://aka.ms/new-console-template for more information

using System.Text;

var input = File.ReadAllText("input.txt").Split(',');

var result = 0L;

foreach (var line in input)
{
    var split = line.Split('-');
    var minimum = long.Parse(split[0]);
    var maximum = long.Parse(split[1]);

    for (var id = minimum; id <= maximum; id++)
    {
        var numText = id.ToString();
        if (numText.Length % 2 == 1)
            continue;

        var firstHalf = long.Parse(numText[..(numText.Length / 2)]);
        var secondHalf = long.Parse(numText[(numText.Length / 2)..]);
        if (firstHalf - secondHalf == 0)
            result += id;
    }
}

Console.WriteLine(result);

result = 0L;
var lockObject = new object();
Parallel.ForEach(input, line =>
{
    var split = line.Split('-');
    var minimum = long.Parse(split[0]);
    var maximum = long.Parse(split[1]);

    for (var id = minimum; id <= maximum; id++)
    {
        var numText = id.ToString();

        for (var size = 1; size <= numText.Length / 2; size++)
        {
            if (numText.Length % size != 0)
                continue;

            var currentText = numText[0..size];
            var resultText = string.Join("", Enumerable.Range(0, numText.Length / size).Select(_ => currentText));

            if (numText == resultText)
            {
                lock (lockObject)
                {
                    result += id;
                }

                break;
            }
        }
    }
});
Console.WriteLine(result);