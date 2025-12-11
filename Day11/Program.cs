// See https://aka.ms/new-console-template for more information

var graph = File.ReadAllLines("input.txt").Select(item =>
{
    var split = item.Split(' ');
    var key = split[0][..^1];
    var values = split[1..];
    return (key, values);
}).ToDictionary(k => k.key, v => v.values.ToHashSet());
graph.Add("out", []);

var result = 0L;
GoNext("you", "out");
Console.WriteLine(result);

var reaches = new HashSet<string>();
var notReaches = new HashSet<string>();

//srv => fft
MapCache("svr", "fft");
GoNextWithCache("svr", "fft");
var finalResult = result;
result = 0;
reaches.Add("svr");
reaches.Clear();
notReaches.Clear();
// fft => dac
MapCache("fft", "dac");
GoNextWithCache("fft", "dac");
finalResult *= result;
result = 0;
reaches.Add("fft");
reaches.Clear();
notReaches.Clear();
// dac => out
MapCache("dac", "out");
GoNextWithCache("dac", "out");
finalResult *= result;
reaches.Add("dac");
reaches.Clear();
notReaches.Clear();

Console.WriteLine(finalResult);


void GoNext(string key, string finalKey)
{
    foreach (var item in graph[key])
    {
        if (item == finalKey)
        {
            result++;
            return;
        }

        GoNext(item, finalKey);
    }
}

void GoNextWithCache(string key, string finalKey)
{
    if (!reaches.Contains(key))
    {
        return;
    }

    foreach (var item in graph[key])
    {
        if (item == finalKey)
        {
            result++;
            return;
        }

        GoNextWithCache(item, finalKey);
    }
}

bool MapCache(string key, string endKey)
{
    if (key == endKey)
        return true;

    if (reaches.Contains(key))
        return true;

    if (notReaches.Contains(key))
        return false;

    var currentReaches = false;
    foreach (var nextKey in graph[key])
    {
        var ok = MapCache(nextKey, endKey);
        if (!currentReaches && ok)
        {
            currentReaches = true;
        }
    }

    if (currentReaches)
    {
        reaches.Add(key);
        return true;
    }

    notReaches.Add(key);
    return false;
}