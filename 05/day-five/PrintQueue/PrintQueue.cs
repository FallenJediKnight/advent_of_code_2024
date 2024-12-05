
namespace PrintQueue;

public class PrintQueue
{
    private readonly Dictionary<Tuple<int, int>, bool> _orderings = new();
    private readonly List<List<int>> _invalidOrderings = [];
    private int _midPointSum = 0;
    
    public void ParsePageOrdering(string line)
    {
        string[] split = line.Split("|");
        int first = int.Parse(split[0]);
        int second = int.Parse(split[1]);
        _orderings.Add(new Tuple<int, int>(first, second), true);
    }

    public bool CheckUpdateOrdering(List<int> update)
    {
        for (int i = 0; i < update.Count - 1; i++)
        {
            for (int j = i + 1; j < update.Count; j++)
            {
                if (!_orderings.ContainsKey(new Tuple<int, int>(update[i], update[j])))
                {
                    _invalidOrderings.Add(update);
                    return false;
                }
            }
        }
        int midPointIndex = update.Count / 2;
        int midPoint = update[midPointIndex];
        _midPointSum += midPoint;
        return true;
    }

    public int FixInvalidOrderings()
    {
        int midPointSum = 0;
        foreach (List<int> invalidOrdering in _invalidOrderings)
        {
            invalidOrdering.Sort((x, y) => _orderings.ContainsKey(new Tuple<int, int>(x, y)) ? -1 : 1);
            int midPointIndex = invalidOrdering.Count / 2;
            midPointSum += invalidOrdering[midPointIndex];
        }

        return midPointSum;
    }
    
    public void PrintMidPointSum()
    {
        Console.WriteLine($"Midpoint sum: {_midPointSum}");
    }
    
    public void PrintPageOrderings()
    {
        foreach (KeyValuePair<Tuple<int, int>, bool> ordering in _orderings)
        {
            Console.WriteLine($"{ordering.Key.Item1} -> {ordering.Key.Item2}");
        }
    }
}