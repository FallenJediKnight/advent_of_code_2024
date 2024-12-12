namespace GuardGallivant;

public class GuardGallivant
{
    private const string Obstacle = "#";
    private const string GuardFacingUp = "^";
    private const string GuardFacingDown = "v";
    private const string GuardFacingLeft = "<";
    private const string GuardFacingRight = ">";
    private enum Facing
    {
        Up,
        Down,
        Left,
        Right
    }
    private readonly Dictionary<Tuple<int, int>, bool> _obstacles = new();
    private Tuple<Tuple<int, int>, Facing> _guardStartingPositionAndFacing;
    private Tuple<Tuple<int, int>, Facing> _currentGuardPositionAndFacing;
    private readonly HashSet<Tuple<int, int>> _positionsVisited = [];
    private int _maxNumberOfRows = 0;
    private int _maxNumberOfColumns = 0;
    
    public void Parse(string line, int row)
    {
        _maxNumberOfRows++;
        _maxNumberOfColumns = line.Length;
        for (int column = 0; column < line.Length; column++)
        {
            string currentChar = line[column].ToString();
            switch (currentChar)
            {
                case Obstacle:
                    _obstacles[Tuple.Create(row, column)] = true;
                    break;
                case GuardFacingUp:
                case GuardFacingDown:
                case GuardFacingLeft:
                case GuardFacingRight:
                    _positionsVisited.Add(Tuple.Create(row, column));
                    SetInitialGuardPositionAndFacing(row, column, currentChar);
                    break;
            }
        }
    }
    
    public void Parse2(string line, int row)
    {
        _maxNumberOfRows++;
        _maxNumberOfColumns = line.Length;
        for (int column = 0; column < line.Length; column++)
        {
            string currentChar = line[column].ToString();
            switch (currentChar)
            {
                case Obstacle:
                    _obstacles[Tuple.Create(row, column)] = true;
                    break;
                case GuardFacingUp:
                case GuardFacingDown:
                case GuardFacingLeft:
                case GuardFacingRight:
                    SetInitialGuardPositionAndFacing(row, column, currentChar);
                    break;
            }
        }
    }

    public int GetNumberOfPositionsVisited()
    {
        while (true)
        {
            try
            {
                FindNextGuardPosition();
            }
            catch (Exception e)
            {
                return _positionsVisited.Count;
            }
        }
    }

    public int GetNumberOfPositionsToCreateInfiniteLoop()
    {
        int numberOfPositionsThatWouldCreateInfiniteLoop = 0;
        GetNumberOfPositionsVisited();
        foreach (Tuple<int, int> positionVisited in _positionsVisited)
        {
            _currentGuardPositionAndFacing = _guardStartingPositionAndFacing;
            _obstacles[positionVisited] = true;
            // Console.WriteLine($"New obstacle position at {positionVisited.Item1}, {positionVisited.Item2}");
            // PrintCurrentGuardPositionAndFacing();
            Dictionary<Tuple<int, int, Facing>, int> positionFacingCount = new();
            bool infiniteLoopFound = false;
            while (!infiniteLoopFound)
            {
                try
                {
                    FindNextGuardPosition2(positionFacingCount);
                }
                catch (Exception e)
                {
                    break;
                }

                if (!positionFacingCount.Values.Any(v => v > 1)) continue;
                numberOfPositionsThatWouldCreateInfiniteLoop++;
                infiniteLoopFound = true;
            }
            // foreach (KeyValuePair<Tuple<int, int, Facing>, int> positionFacing in positionFacingCount)
            // {
            //     Console.WriteLine($"Position {positionFacing.Key.Item1}, {positionFacing.Key.Item2}, facing {positionFacing.Key.Item3} visited {positionFacing.Value} times");
            // }
            _obstacles.Remove(positionVisited);
        }

        return numberOfPositionsThatWouldCreateInfiniteLoop;
    }
    
    private void FindNextGuardPosition2(Dictionary<Tuple<int, int, Facing>, int> positionFacingCount)
    {
        Tuple<int, int> startingPosition = _currentGuardPositionAndFacing.Item1;
        Facing facing = _currentGuardPositionAndFacing.Item2;
        bool obstacleFound = false;
        switch (facing)
        {
            case Facing.Up:
                for (int row = startingPosition.Item1; row > -1; row--)
                {
                    if (_obstacles.ContainsKey(Tuple.Create(row, startingPosition.Item2)))
                    {
                        obstacleFound = true;
                        Facing newFacing = GetNextGuardFacing(facing);
                        _currentGuardPositionAndFacing =
                            new Tuple<Tuple<int, int>, Facing>(Tuple.Create(row + 1, startingPosition.Item2),
                                newFacing);
                        break;
                    }
                    positionFacingCount[Tuple.Create(row, startingPosition.Item2, facing)] =
                        positionFacingCount.GetValueOrDefault(Tuple.Create(row, startingPosition.Item2, facing)) + 1;
                }
                break;
            case Facing.Down:
                for (int row = startingPosition.Item1; row < _maxNumberOfRows; row++)
                {
                    if (_obstacles.ContainsKey(Tuple.Create(row, startingPosition.Item2)))
                    {
                        obstacleFound = true;
                        Facing newFacing = GetNextGuardFacing(facing);
                        _currentGuardPositionAndFacing =
                            new Tuple<Tuple<int, int>, Facing>(Tuple.Create(row - 1, startingPosition.Item2),
                                newFacing);
                        break;
                    }
                    positionFacingCount[Tuple.Create(row, startingPosition.Item2, facing)] =
                        positionFacingCount.GetValueOrDefault(Tuple.Create(row, startingPosition.Item2, facing)) + 1;
                }
                break;
            case Facing.Left:
                for (int column = startingPosition.Item2; column > -1; column--)
                {
                    if (_obstacles.ContainsKey(Tuple.Create(startingPosition.Item1, column)))
                    {
                        obstacleFound = true;
                        Facing newFacing = GetNextGuardFacing(facing);
                        _currentGuardPositionAndFacing =
                            new Tuple<Tuple<int, int>, Facing>(Tuple.Create(startingPosition.Item1, column + 1),
                                newFacing);
                        break;
                    }
                    positionFacingCount[Tuple.Create(startingPosition.Item1, column, facing)] =
                        positionFacingCount.GetValueOrDefault(Tuple.Create(startingPosition.Item1, column, facing)) + 1;
                }
                break;
            case Facing.Right:
                for (int column = startingPosition.Item2; column < _maxNumberOfColumns; column++)
                {
                    if (_obstacles.ContainsKey(Tuple.Create(startingPosition.Item1, column)))
                    {
                        obstacleFound = true;
                        Facing newFacing = GetNextGuardFacing(facing);
                        _currentGuardPositionAndFacing =
                            new Tuple<Tuple<int, int>, Facing>(Tuple.Create(startingPosition.Item1, column - 1),
                                newFacing);
                        break;
                    }
                    positionFacingCount[Tuple.Create(startingPosition.Item1, column, facing)] =
                        positionFacingCount.GetValueOrDefault(Tuple.Create(startingPosition.Item1, column, facing)) + 1;
                }
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        if (!obstacleFound)
        {
            throw new Exception("No obstacle found");
        }
    }
    
    public void PrintObstacles()
    {
        foreach (KeyValuePair<Tuple<int, int>, bool> obstacle in _obstacles)
        {
            Console.WriteLine($"Obstacle at {obstacle.Key.Item1}, {obstacle.Key.Item2}");
        }
    }
    
    public void PrintCurrentGuardPositionAndFacing()
    {
        Tuple<int, int> position = _currentGuardPositionAndFacing.Item1;
        Facing facing = _currentGuardPositionAndFacing.Item2;
        Console.WriteLine($"Guard at {position.Item1}, {position.Item2} facing {facing}");
    }
    
    public void PrintPositionsVisited()
    {
        foreach (Tuple<int, int> position in _positionsVisited)
        {
            Console.WriteLine($"Visited position at {position.Item1}, {position.Item2}");
        }
    }

    private void FindNextGuardPosition()
    {
        Tuple<int, int> startingPosition = _currentGuardPositionAndFacing.Item1;
        Facing facing = _currentGuardPositionAndFacing.Item2;
        bool obstacleFound = false;
        switch (facing)
        {
            case Facing.Up:
                for (int row = startingPosition.Item1 - 1; row > -1; row--)
                {
                    if (_obstacles.ContainsKey(Tuple.Create(row, startingPosition.Item2)))
                    {
                        obstacleFound = true;
                        Facing newFacing = GetNextGuardFacing(facing);
                        _currentGuardPositionAndFacing =
                            new Tuple<Tuple<int, int>, Facing>(Tuple.Create(row + 1, startingPosition.Item2),
                                newFacing);
                        break;
                    }
                    _positionsVisited.Add(Tuple.Create(row, startingPosition.Item2));
                }
                break;
            case Facing.Down:
                for (int row = startingPosition.Item1 + 1; row < _maxNumberOfRows; row++)
                {
                    if (_obstacles.ContainsKey(Tuple.Create(row, startingPosition.Item2)))
                    {
                        obstacleFound = true;
                        Facing newFacing = GetNextGuardFacing(facing);
                        _currentGuardPositionAndFacing =
                            new Tuple<Tuple<int, int>, Facing>(Tuple.Create(row - 1, startingPosition.Item2),
                                newFacing);
                        break;
                    }
                    _positionsVisited.Add(Tuple.Create(row, startingPosition.Item2));
                }
                break;
            case Facing.Left:
                for (int column = startingPosition.Item2 - 1; column > -1; column--)
                {
                    if (_obstacles.ContainsKey(Tuple.Create(startingPosition.Item1, column)))
                    {
                        obstacleFound = true;
                        Facing newFacing = GetNextGuardFacing(facing);
                        _currentGuardPositionAndFacing =
                            new Tuple<Tuple<int, int>, Facing>(Tuple.Create(startingPosition.Item1, column + 1),
                                newFacing);
                        break;
                    }
                    _positionsVisited.Add(Tuple.Create(startingPosition.Item1, column));
                }
                break;
            case Facing.Right:
                for (int column = startingPosition.Item2 + 1; column < _maxNumberOfColumns; column++)
                {
                    if (_obstacles.ContainsKey(Tuple.Create(startingPosition.Item1, column)))
                    {
                        obstacleFound = true;
                        Facing newFacing = GetNextGuardFacing(facing);
                        _currentGuardPositionAndFacing =
                            new Tuple<Tuple<int, int>, Facing>(Tuple.Create(startingPosition.Item1, column - 1),
                                newFacing);
                        break;
                    }
                    _positionsVisited.Add(Tuple.Create(startingPosition.Item1, column));
                }
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        if (!obstacleFound)
        {
            throw new Exception("No obstacle found");
        }
    }

    private static Facing GetNextGuardFacing(Facing currentFacing)
    {
        return currentFacing switch
        {
            Facing.Up => Facing.Right,
            Facing.Down => Facing.Left,
            Facing.Left => Facing.Up,
            Facing.Right => Facing.Down,
            _ => throw new ArgumentOutOfRangeException(nameof(currentFacing), currentFacing, null)
        };
    }
    
    private void SetInitialGuardPositionAndFacing(int row, int column, string facingString)
    {
        Facing facing = facingString switch
        {
            GuardFacingUp => Facing.Up,
            GuardFacingDown => Facing.Down,
            GuardFacingLeft => Facing.Left,
            GuardFacingRight => Facing.Right,
            _ => throw new ArgumentException("Invalid facing string")
        };
        _guardStartingPositionAndFacing = new Tuple<Tuple<int, int>, Facing>(Tuple.Create(row, column), facing);
        _currentGuardPositionAndFacing = new Tuple<Tuple<int, int>, Facing>(Tuple.Create(row, column), facing);
    }
}