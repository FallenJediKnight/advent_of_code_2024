using System.Numerics;

namespace ResonantCollinearity;

public class ResonantCollinearity
{
    private readonly Dictionary<char, HashSet<Vector2>> _frequencyCoordinates = new();
    private readonly Dictionary<char, HashSet<Vector2>> _frequencyAntinodeCoordinates = new();
    private int _maxRows;
    private int _maxColumns;
    
    public void SetMaxRows(int maxRows)
    {
        _maxRows = maxRows;
    }
    
    public void ParseLine(string line, int row)
    {
        if (string.IsNullOrWhiteSpace(line))
        {
            return;
        }

        _maxColumns = Math.Max(_maxColumns, line.Length);
        
        for (int column = 0; column < line.Length; column++)
        {
            char c = line[column];
            if (c == '.')
            {
                continue;
            }

            if (!_frequencyCoordinates.TryGetValue(c, out HashSet<Vector2>? coordinates))
            {
                coordinates = [];
                _frequencyCoordinates[c] = coordinates;
            }
            if (!_frequencyAntinodeCoordinates.TryGetValue(c, out HashSet<Vector2>? antinodeCoordinates))
            {
                antinodeCoordinates = [];
                _frequencyAntinodeCoordinates[c] = antinodeCoordinates;
            }

            Vector2 u = new(row, column + 1);
            CalculateAntinodeCoordinatesForFrequencyVector(c, u);
            coordinates.Add(u);
        }
    }
    
    public void ParseLine2(string line, int row)
    {
        if (string.IsNullOrWhiteSpace(line))
        {
            return;
        }

        _maxColumns = Math.Max(_maxColumns, line.Length);
        
        for (int column = 0; column < line.Length; column++)
        {
            char c = line[column];
            if (c == '.')
            {
                continue;
            }

            if (!_frequencyCoordinates.TryGetValue(c, out HashSet<Vector2>? coordinates))
            {
                coordinates = [];
                _frequencyCoordinates[c] = coordinates;
            }
            if (!_frequencyAntinodeCoordinates.TryGetValue(c, out HashSet<Vector2>? antinodeCoordinates))
            {
                antinodeCoordinates = [];
                _frequencyAntinodeCoordinates[c] = antinodeCoordinates;
            }

            Vector2 u = new(row, column + 1);
            CalculateAntinodeCoordinatesForFrequencyVector2(c, u);
            coordinates.Add(u);
        }
    }
    
    public void PrintFrequencyCoordinates()
    {
        foreach (KeyValuePair<char, HashSet<Vector2>> frequencyCoordinate in _frequencyCoordinates)
        {
            Console.WriteLine($"Frequency {frequencyCoordinate.Key} coordinates:");
            foreach (Vector2 coordinate in frequencyCoordinate.Value)
            {
                Console.WriteLine($"({coordinate.X}, {coordinate.Y})");
            }
        }
    }
    
    public void PrintFrequencyAntinodeCoordinates()
    {
        foreach (KeyValuePair<char, HashSet<Vector2>> antinodeCoordinate in _frequencyAntinodeCoordinates)
        {
            Console.WriteLine($"Frequency {antinodeCoordinate.Key} antinode coordinates:");
            foreach (Vector2 coordinate in antinodeCoordinate.Value.OrderBy(c => c.X).ThenBy(c => c.Y))
            {
                Console.WriteLine($"({coordinate.X}, {coordinate.Y})");
            }
        }
    }
    
    public int GetTotalNumberOfAntinodeCoordinates()
    {
        HashSet<Vector2> combinedAntinodes = [];
        foreach (HashSet<Vector2> coordinateSet in _frequencyAntinodeCoordinates.Values)
        {
            combinedAntinodes.UnionWith(coordinateSet);
        }
        return combinedAntinodes.Count;
    }

    private void CalculateAntinodeCoordinatesForFrequencyVector(char frequency, Vector2 u)
    {
        foreach (Vector2 v in _frequencyCoordinates[frequency])
        {
            Vector2 distanceVectorV = v - u;
            Vector2 distanceVectorU = u - v;

            Vector2 antinodeCoordinateFromV = v + (2 * distanceVectorU);
            Vector2 antinodeCoordinateFromU = u + (2 * distanceVectorV);
            
            if (WithinBounds(antinodeCoordinateFromU) & !_frequencyAntinodeCoordinates[frequency].Contains(antinodeCoordinateFromU))
            {
                _frequencyAntinodeCoordinates[frequency].Add(antinodeCoordinateFromU);
            }
            if (WithinBounds(antinodeCoordinateFromV) & !_frequencyAntinodeCoordinates[frequency].Contains(antinodeCoordinateFromV))
            {
                _frequencyAntinodeCoordinates[frequency].Add(antinodeCoordinateFromV);
            }
        }
    }
    
    private void CalculateAntinodeCoordinatesForFrequencyVector2(char frequency, Vector2 u)
    {
        foreach (Vector2 v in _frequencyCoordinates[frequency])
        {
            Vector2 distanceVector = v - u;

            Vector2 x = u;
            while (WithinBounds(x))
            {
                _frequencyAntinodeCoordinates[frequency].Add(x);
                x += distanceVector;
            }
            
            Vector2 y = u;
            while (WithinBounds(y))
            {
                _frequencyAntinodeCoordinates[frequency].Add(y);
                y -= distanceVector;
            }
        }
    }
    
    private bool WithinBounds(Vector2 coordinate)
    {
        return coordinate.X >= 1 && coordinate.X <= _maxRows && coordinate.Y >= 1 && coordinate.Y <= _maxColumns;
    }
}