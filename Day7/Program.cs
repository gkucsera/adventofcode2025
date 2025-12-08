// See https://aka.ms/new-console-template for more information

var map = File.ReadAllLines("input.txt").Select(item => item.ToCharArray()).ToArray();

var result = 0;

var start = map[0].IndexOf('S');
var rayMap = new HashSet<int> { start };

foreach (var line in map.Skip(1))
{
    var nextRayMap = new HashSet<int>();
    foreach (var ray in rayMap)
    {
        if (line[ray] == '.')
        {
            nextRayMap.Add(ray);
        }
        else
        {
            result += 1;
            nextRayMap.Add(ray - 1);
            nextRayMap.Add(ray + 1);
        }
    }

    rayMap = nextRayMap;
}

Console.WriteLine(result);

var rayMapDict = new Dictionary<int, long> { { start, 1 } };

foreach (var line in map.Skip(1))
{
    var nextRayMap = new Dictionary<int, long>();
    foreach (var ray in rayMapDict)
    {
        if (line[ray.Key] == '.')
        {
            if (!nextRayMap.ContainsKey(ray.Key))
            {
                nextRayMap.Add(ray.Key, ray.Value);
            }
            else
            {
                nextRayMap[ray.Key] += ray.Value;
            }
        }
        else
        {
            if (!nextRayMap.ContainsKey(ray.Key - 1))
            {
                nextRayMap[ray.Key - 1] = ray.Value;
            }
            else
            {
                nextRayMap[ray.Key - 1] += ray.Value;
            } 
            
            if (!nextRayMap.ContainsKey(ray.Key + 1))
            {
                nextRayMap[ray.Key + 1] = ray.Value;
            }
            else
            {
                nextRayMap[ray.Key + 1] += ray.Value;
            }

        }
    }

    rayMapDict = nextRayMap;
}

Console.WriteLine(rayMapDict.Sum(item => item.Value));
