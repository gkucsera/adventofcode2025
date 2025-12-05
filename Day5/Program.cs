// See https://aka.ms/new-console-template for more information

var input = File.ReadAllLines("input.txt");

var idRanges = new List<NumberRange>();
var idsToCheck = new List<long>();
foreach (var line in input)
{
    if (long.TryParse(line, out var id))
    {
        idsToCheck.Add(id);
    }
    else if (!string.IsNullOrWhiteSpace(line))
    {
        var split = line.Split('-');
        var from = long.Parse(split[0]);
        var to = long.Parse(split[1]);
        idRanges.Add(new NumberRange(from, to));
    }
}

var result = 0L;
foreach (var id in idsToCheck)
{
    if (idRanges.Any(item => item.IsInRange(id)))
    {
        result++;
    }
}

Console.WriteLine(result);

bool isMerged;
var oldList = idRanges.ToArray();
do
{
    var newList = new List<NumberRange>(oldList);
    isMerged = false;
    for (var i = 0; i < oldList.Length - 1; i++)
    {
        for (var j = i + 1; j < oldList.Length; j++)
        {
            var merged = oldList[i].TryMerge(oldList[j]);
            if (merged.HasValue)
            {
                newList.Add(merged.Value);
                newList.Remove(oldList[j]);
                newList.Remove(oldList[i]);
                isMerged = true;
                break;
            }
        }
        if (isMerged)
            break;
    }
    oldList = newList.ToArray();
} while (isMerged);

result = oldList.Sum(item => (item.End - item.Start) + 1);

Console.WriteLine(result);
return;

record struct NumberRange
{
    public long Start;
    public long End;

    public NumberRange(long start, long end)
    {
        Start = start;
        End = end;
    }

    public bool IsInRange(long number)
    {
        if (number >= Start && number <= End)
        {
            return true;
        }

        return false;
    }

    public NumberRange? TryMerge(NumberRange other)
    {
        if ((Start < other.Start && End < other.Start) || (other.Start < Start && other.End < Start))
            return null;

        return new NumberRange(Math.Min(Start, other.Start), Math.Max(End, other.End));
    }
    
    
}