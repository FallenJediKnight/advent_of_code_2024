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
    private Tuple<Tuple<int, int>, Facing> _currentGuardPositionAndFacing;
    private readonly HashSet<Tuple<int, int>> _positionsVisited = [];
    public int _maxNumberOfRows = 0;
    public int _maxNumberOfColumns = 0;
    
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
        _currentGuardPositionAndFacing = new Tuple<Tuple<int, int>, Facing>(Tuple.Create(row, column), facing);
    }
}