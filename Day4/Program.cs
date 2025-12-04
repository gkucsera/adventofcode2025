// See https://aka.ms/new-console-template for more information

const char empty = '.';
const char full = '@';

var map = File.ReadAllLines("input.txt").Select(item => item.ToCharArray()).ToArray();
var result = 0;

for (var i = 0; i < map.Length; i++)
{
    for (var j = 0; j < map[i].Length; j++)
    {
        if (map[i][j] == empty)
            continue;

        var neighbors = 0;
        neighbors += CheckSurrounding(i - 1, j - 1);
        neighbors += CheckSurrounding(i - 1, j);
        neighbors += CheckSurrounding(i - 1, j + 1);

        neighbors += CheckSurrounding(i, j - 1);
        neighbors += CheckSurrounding(i, j + 1);

        neighbors += CheckSurrounding(i + 1, j - 1);
        neighbors += CheckSurrounding(i + 1, j);
        neighbors += CheckSurrounding(i + 1, j + 1);

        if (neighbors < 4)
        {
            result++;
        }
    }
}

Console.WriteLine(result);

result = 0;
var keepGoing = false;
do
{
    keepGoing = false;
    for (var i = 0; i < map.Length; i++)
    {
        for (var j = 0; j < map[i].Length; j++)
        {
            if (map[i][j] == empty)
                continue;

            var neighbors = 0;
            neighbors += CheckSurrounding(i - 1, j - 1);
            neighbors += CheckSurrounding(i - 1, j);
            neighbors += CheckSurrounding(i - 1, j + 1);

            neighbors += CheckSurrounding(i, j - 1);
            neighbors += CheckSurrounding(i, j + 1);

            neighbors += CheckSurrounding(i + 1, j - 1);
            neighbors += CheckSurrounding(i + 1, j);
            neighbors += CheckSurrounding(i + 1, j + 1);

            if (neighbors < 4)
            {
                result++;
                map[i][j] = empty;
                keepGoing = true;
            }
        }
    }
} while (keepGoing);

Console.WriteLine(result);


return;

int CheckSurrounding(int x, int y)
{
    if (x < 0 || x >= map.Length || y < 0 || y >= map[x].Length)
        return 0;

    return map[x][y] == full ? 1 : 0;
}