using System.Text;

namespace CeresSearch;

public class CeresSearch
{
    private readonly List<List<string>> _grid = [];
    private readonly List<Tuple<int, int>> _startingPositions = [];
    private readonly Dictionary<Tuple<int, int>, int> _searchResults = new();
    private readonly Dictionary<Tuple<int, int>, int> _aPositions = new();

    public void Add(string line, string startingLetter)
    {
        List<string> row = [];
        foreach (string letter in line.Select(letterChar => letterChar.ToString()))
        {
            row.Add(letter);
            if (letter.Equals("a", StringComparison.CurrentCultureIgnoreCase))
            {
                Tuple<int, int> aPosition = new(_grid.Count, row.Count - 1);
                _aPositions.Add(aPosition, 0);
            }

            if (!letter.Equals(startingLetter, StringComparison.CurrentCultureIgnoreCase)) continue;
            Tuple<int, int> startingPosition = new(_grid.Count, row.Count - 1);
            _startingPositions.Add(startingPosition);
            _searchResults[startingPosition] = 0;
        }
        _grid.Add(row.ToList());
    }
    
    public int SearchPuzzle1(string searchString)
    {
        return _startingPositions.Select(
            startingPosition => (List<bool>) [
                SearchLeft(startingPosition, searchString),
                SearchRight(startingPosition, searchString),
                SearchUp(startingPosition, searchString),
                SearchDown(startingPosition, searchString),
                SearchLeftUp(startingPosition, searchString),
                SearchRightUp(startingPosition, searchString),
                SearchLeftDown(startingPosition, searchString),
                SearchRightDown(startingPosition, searchString)]
            ).Select(searchList => searchList.Count(search => search)).Sum();
    }
    
    public int SearchPuzzle2(string searchString)
    {
        foreach (Tuple<int, int> startingPosition in _startingPositions)
        {
            SearchLeftUp(startingPosition, searchString);
            SearchRightUp(startingPosition, searchString);
            SearchLeftDown(startingPosition, searchString);
            SearchRightDown(startingPosition, searchString);
        }
        return _aPositions.Count(aPosition => aPosition.Value == 2);
    }
    
    public void PrintGrid()
    {
        foreach (List<string> row in _grid)
        {
            Console.WriteLine(string.Join("", row));
        }
    }
    
    public void PrintStartingPositions()
    {
        foreach (Tuple<int, int> startingPosition in _startingPositions)
        {
            Console.WriteLine($"Starting position: {startingPosition.Item1}, {startingPosition.Item2}");
        }
    }
    
    public void PrintSearchResults()
    {
        Console.WriteLine(_searchResults.Count);
        foreach (KeyValuePair<Tuple<int, int>, int> searchResult in _searchResults)
        {
            Console.WriteLine($"Search result: {searchResult.Key.Item1}, {searchResult.Key.Item2} - {searchResult.Value}");
        }
    }
    
    public void PrintAPositions()
    {
        Console.WriteLine(_aPositions.Count);
        foreach (KeyValuePair<Tuple<int, int>, int> aPosition in _aPositions)
        {
            Console.WriteLine($"A position: {aPosition.Key.Item1}, {aPosition.Key.Item2} - {aPosition.Value}");
        }
    }
    
    private bool SearchLeft(Tuple<int, int> startingPosition, string searchString)
    {
        Tuple<int, int> currentPosition = startingPosition;
        StringBuilder stringBuilder = new();
        while (true)
        {
            stringBuilder.Append(_grid[currentPosition.Item1][currentPosition.Item2]);
            if (stringBuilder.ToString().Equals(searchString))
            {
                _searchResults[startingPosition] += 1;
                return true;
            }
            if (!searchString.Contains(stringBuilder.ToString()))
            {
                return false;
            }
            if (currentPosition.Item2 == 0)
            {
                return false;
            }
            currentPosition = TraverseLeft(currentPosition);
        }
    }
    
    private bool SearchRight(Tuple<int, int> startingPosition, string searchString)
    {
        Tuple<int, int> currentPosition = startingPosition;
        StringBuilder stringBuilder = new();
        while (true)
        {
            stringBuilder.Append(_grid[currentPosition.Item1][currentPosition.Item2]);
            if (stringBuilder.ToString().Equals(searchString))
            {
                _searchResults[startingPosition] += 1;
                return true;
            }
            if (!searchString.Contains(stringBuilder.ToString()))
            {
                return false;
            }
            if (currentPosition.Item2 == _grid[currentPosition.Item1].Count - 1)
            {
                return false;
            }
            currentPosition = TraverseRight(currentPosition);
        }
    }
    
    private bool SearchUp(Tuple<int, int> startingPosition, string searchString)
    {
        Tuple<int, int> currentPosition = startingPosition;
        StringBuilder stringBuilder = new();
        while (true)
        {
            stringBuilder.Append(_grid[currentPosition.Item1][currentPosition.Item2]);
            if (stringBuilder.ToString().Equals(searchString))
            {
                _searchResults[startingPosition] += 1;
                return true;
            }
            if (!searchString.Contains(stringBuilder.ToString()))
            {
                return false;
            }
            if (currentPosition.Item1 == 0)
            {
                return false;
            }
            currentPosition = TraverseUp(currentPosition);
        }
    }
    
    private bool SearchDown(Tuple<int, int> startingPosition, string searchString)
    {
        Tuple<int, int> currentPosition = startingPosition;
        StringBuilder stringBuilder = new();
        while (true)
        {
            stringBuilder.Append(_grid[currentPosition.Item1][currentPosition.Item2]);
            if (stringBuilder.ToString().Equals(searchString))
            {
                _searchResults[startingPosition] += 1;
                return true;
            }
            if (!searchString.Contains(stringBuilder.ToString()))
            {
                return false;
            }
            if (currentPosition.Item1 == _grid.Count - 1)
            {
                return false;
            }
            currentPosition = TraverseDown(currentPosition);
        }
    }

    private bool SearchLeftUp(Tuple<int, int> startingPosition, string searchString)
    {
        Tuple<int, int> currentPosition = startingPosition;
        StringBuilder stringBuilder = new();
        while (true)
        {
            stringBuilder.Append(_grid[currentPosition.Item1][currentPosition.Item2]);
            if (stringBuilder.ToString().Equals(searchString))
            {
                _searchResults[startingPosition] += 1;
                if (searchString.Equals("MAS", StringComparison.CurrentCultureIgnoreCase))
                {
                    _aPositions[new Tuple<int, int>(currentPosition.Item1 + 1, currentPosition.Item2 + 1)] += 1;
                }
                return true;
            }
            if (!searchString.Contains(stringBuilder.ToString()))
            {
                return false;
            }
            if (currentPosition.Item1 == 0 || currentPosition.Item2 == 0)
            {
                return false;
            }
            currentPosition = TraverseLeftUp(currentPosition);
        }
    }
    
    private bool SearchRightUp(Tuple<int, int> startingPosition, string searchString)
    {
        Tuple<int, int> currentPosition = startingPosition;
        StringBuilder stringBuilder = new();
        while (true)
        {
            stringBuilder.Append(_grid[currentPosition.Item1][currentPosition.Item2]);
            if (stringBuilder.ToString().Equals(searchString))
            {
                _searchResults[startingPosition] += 1;
                if (searchString.Equals("MAS", StringComparison.CurrentCultureIgnoreCase))
                {
                    _aPositions[new Tuple<int, int>(currentPosition.Item1 + 1, currentPosition.Item2 - 1)] += 1;
                }
                return true;
            }
            if (!searchString.Contains(stringBuilder.ToString()))
            {
                return false;
            }
            if (currentPosition.Item1 == 0 || currentPosition.Item2 == _grid[currentPosition.Item1].Count - 1)
            {
                return false;
            }
            currentPosition = TraverseRightUp(currentPosition);
        }
    }
    
    private bool SearchLeftDown(Tuple<int, int> startingPosition, string searchString)
    {
        Tuple<int, int> currentPosition = startingPosition;
        StringBuilder stringBuilder = new();
        while (true)
        {
            stringBuilder.Append(_grid[currentPosition.Item1][currentPosition.Item2]);
            if (stringBuilder.ToString().Equals(searchString))
            {
                _searchResults[startingPosition] += 1;
                if (searchString.Equals("MAS", StringComparison.CurrentCultureIgnoreCase))
                {
                    _aPositions[new Tuple<int, int>(currentPosition.Item1 - 1, currentPosition.Item2 + 1)] += 1;
                }
                return true;
            }
            if (!searchString.Contains(stringBuilder.ToString()))
            {
                return false;
            }
            if (currentPosition.Item1 == _grid.Count - 1 || currentPosition.Item2 == 0)
            {
                return false;
            }
            currentPosition = TraverseLeftDown(currentPosition);
        }
    }
    
    private bool SearchRightDown(Tuple<int, int> startingPosition, string searchString)
    {
        Tuple<int, int> currentPosition = startingPosition;
        StringBuilder stringBuilder = new();
        while (true)
        {
            stringBuilder.Append(_grid[currentPosition.Item1][currentPosition.Item2]);
            if (stringBuilder.ToString().Equals(searchString))
            {
                _searchResults[startingPosition] += 1;
                if (searchString.Equals("MAS", StringComparison.CurrentCultureIgnoreCase))
                {
                    _aPositions[new Tuple<int, int>(currentPosition.Item1 - 1, currentPosition.Item2 - 1)] += 1;
                }
                return true;
            }
            if (!searchString.Contains(stringBuilder.ToString()))
            {
                return false;
            }
            if (currentPosition.Item1 == _grid.Count - 1 || currentPosition.Item2 == _grid[currentPosition.Item1].Count - 1)
            {
                return false;
            }
            currentPosition = TraverseRightDown(currentPosition);
        }
    }
    
    private Tuple<int, int> TraverseLeft(Tuple<int, int> fromPosition)
    {
        if (fromPosition.Item2 == 0)
        {
            throw new InvalidOperationException("Cannot traverse left from the leftmost position.");
        }
        return new Tuple<int, int>(fromPosition.Item1, fromPosition.Item2 - 1);
    }
    
    private Tuple<int, int> TraverseRight(Tuple<int, int> fromPosition)
    {
        if (fromPosition.Item2 == _grid[fromPosition.Item1].Count - 1)
        {
            throw new InvalidOperationException("Cannot traverse right from the rightmost position.");
        }
        return new Tuple<int, int>(fromPosition.Item1, fromPosition.Item2 + 1);
    }
    
    private Tuple<int, int> TraverseUp(Tuple<int, int> fromPosition)
    {
        if (fromPosition.Item1 == 0)
        {
            throw new InvalidOperationException("Cannot traverse up from the topmost position.");
        }
        return new Tuple<int, int>(fromPosition.Item1 - 1, fromPosition.Item2);
    }
    
    private Tuple<int, int> TraverseDown(Tuple<int, int> fromPosition)
    {
        if (fromPosition.Item1 == _grid.Count - 1)
        {
            throw new InvalidOperationException("Cannot traverse down from the bottommost position.");
        }
        return new Tuple<int, int>(fromPosition.Item1 + 1, fromPosition.Item2);
    }
    
    private Tuple<int, int> TraverseLeftUp(Tuple<int, int> fromPosition)
    {
        return TraverseUp(TraverseLeft(fromPosition));
    }
    
    private Tuple<int, int> TraverseRightUp(Tuple<int, int> fromPosition)
    {
        return TraverseUp(TraverseRight(fromPosition));
    }
    
    private Tuple<int, int> TraverseLeftDown(Tuple<int, int> fromPosition)
    {
        return TraverseDown(TraverseLeft(fromPosition));
    }
    
    private Tuple<int, int> TraverseRightDown(Tuple<int, int> fromPosition)
    {
        return TraverseDown(TraverseRight(fromPosition));
    }
}