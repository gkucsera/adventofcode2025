// See https://aka.ms/new-console-template for more information

using Day8;

var points = File.ReadAllLines("input.txt").Select(item =>
{
    var split = item.Split(',');
    return new Position { X = int.Parse(split[0]), Y = int.Parse(split[1]), Z = int.Parse(split[2]) };
}).ToArray();

var distances = new Dictionary<double, (Position, Position)>();
var connections = new List<HashSet<Position>>();
for (var i = 0; i < points.Length - 1; i++)
{
    for (var j = i + 1; j < points.Length; j++)
    {
        var distance = points[i].GetDistance(points[j]);
        distances.Add(distance, (points[i], points[j]));
    }
}

foreach (var pair in distances.OrderBy(item => item.Key).Take(1000))
{
    var circuit1 = connections.SingleOrDefault(item => item.Contains(pair.Value.Item1));
    var circuit2 = connections.SingleOrDefault(item => item.Contains(pair.Value.Item2));
    if (circuit1 == null && circuit2 == null)
    {
        connections.Add([pair.Value.Item1, pair.Value.Item2]);
    }

    else if (circuit1 != null && circuit2 == null)
    {
        circuit1.Add(pair.Value.Item2);
    }
    else if (circuit2 != null && circuit1 == null)
    {
        circuit2.Add(pair.Value.Item1);
    }
    else  if (circuit1 != null && circuit2 != null && circuit1 != circuit2)
    {
        foreach (var item in circuit2)
        {
            circuit1.Add(item);
        }

        connections.Remove(circuit2);
    }
    else if (circuit1 != null && circuit2 != null && circuit1 == circuit2)
    {
    }
    else
    {
        throw new Exception();
    }
}

connections = connections.OrderByDescending(item => item.Count).Take(3).ToList();
var result = (long)connections.First().Count;
result = connections.Skip(1).Aggregate(result, (current, connection) => current * connection.Count);
Console.WriteLine(result);

result = 0;
connections.Clear();
foreach (var pair in distances.OrderBy(item => item.Key))
{
    var circuit1 = connections.SingleOrDefault(item => item.Contains(pair.Value.Item1));
    var circuit2 = connections.SingleOrDefault(item => item.Contains(pair.Value.Item2));
    if (circuit1 == null && circuit2 == null)
    {
        connections.Add([pair.Value.Item1, pair.Value.Item2]);
    }

    else if (circuit1 != null && circuit2 == null)
    {
        circuit1.Add(pair.Value.Item2);
    }
    else if (circuit2 != null && circuit1 == null)
    {
        circuit2.Add(pair.Value.Item1);
    }
    else  if (circuit1 != null && circuit2 != null && circuit1 != circuit2)
    {
        foreach (var item in circuit2)
        {
            circuit1.Add(item);
        }

        connections.Remove(circuit2);
    }
    else if (circuit1 != null && circuit2 != null && circuit1 == circuit2)
    {
    }
    else
    {
        throw new Exception();
    }

    if (connections.Count == 1 && connections[0].Count == points.Length)
    {
        result = (long)pair.Value.Item1.X * pair.Value.Item2.X;
        break;
    }
}
Console.WriteLine(result);