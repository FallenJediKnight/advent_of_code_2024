namespace DiskFragmenter;

public class DiskFragmenter
{
    private readonly List<string> _disk = [];
    private readonly Queue<int> _freeBlocks = new();
    private readonly List<Queue<int>> _contiguousFreeBlocks = [];
    private readonly Dictionary<string, Tuple<int, int>> _fileBlocks = [];
    private bool _stillFree;
    
    public void AllocateSpace(int numBlocks, bool isFile, string id)
    {
        for (int i = 0; i < numBlocks; i++)
        {
            if (!isFile)
            {
                _freeBlocks.Enqueue(_disk.Count);
                if (!_stillFree)
                {
                    Queue<int> queue = [];
                    queue.Enqueue(_disk.Count);
                    _contiguousFreeBlocks.Add(queue);
                }
                else
                {
                    _contiguousFreeBlocks[^1].Enqueue(_disk.Count);
                }
                _stillFree = true;
                _disk.Add(".");
            }
            else
            {
                _stillFree = false;
                _disk.Add(id);
            }
        }

        if (isFile)
        {
            _fileBlocks.Add(id, Tuple.Create(_disk.Count - numBlocks, numBlocks));
        }
    }
    
    public void FragmentDisk()
    {
        for (int i = _disk.Count - 1; i >= 0 ; i--)
        {
            try
            {
                if (_disk[i] == ".")
                {
                    continue;
                }
                int freeBlock = _freeBlocks.Dequeue();
                if (freeBlock < i)
                {
                    Swap(freeBlock, i);
                }
            }
            catch (InvalidOperationException)
            {
                break;
            }
        }
    }
    
    public void DefragmentDisk()
    {
        foreach (string id in _fileBlocks.Keys.OrderByDescending(double.Parse))
        {
            int fileSize = _fileBlocks[id].Item2;
            foreach (Queue<int> freeBlock in _contiguousFreeBlocks.Where(freeBlock => freeBlock.Count >= fileSize))
            {
                for (int j = _fileBlocks[id].Item1; j < _fileBlocks[id].Item1 + fileSize; j++)
                {
                    if (freeBlock.Peek() > j) break;
                    int free = freeBlock.Dequeue();
                    Swap(free, j);
                }

                break;
            }
        }
    }

    public double CalculateFileSystemChecksum()
    {
        return _disk.Select((t, i) => t == "." ? 0 : i * double.Parse(t)).Sum();
    }
    
    public void PrintDisk()
    {
        Console.WriteLine(string.Join("", _disk));
    }
    
    public void PrintFreeBlocks()
    {
        Console.WriteLine(string.Join(", ", _freeBlocks));
    }
    
    public void PrintContiguousFreeBlocks()
    {
        foreach (Queue<int> queue in _contiguousFreeBlocks)
        {
            Console.WriteLine(string.Join(", ", queue));
        }
    }
    
    public void PrintFileBlocks()
    {
        foreach (string key in _fileBlocks.Keys)
        {
            Console.WriteLine($"{key}: {_fileBlocks[key].Item1}, {_fileBlocks[key].Item2}");
        }
    }
    
    private void Swap(int i, int j)
    {
        (_disk[i], _disk[j]) = (_disk[j], _disk[i]);
    }
}