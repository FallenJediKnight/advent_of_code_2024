using System.Diagnostics;
using System.Text;

namespace DiskFragmenter
{
    internal static class Program
    {
        public static void Main(string[] args)
        {
            // DayNine.Puzzle1();
            DayNine.Puzzle2();
        }
    }
    
    internal static class DayNine
    {
        private const int BufferSize = 128;
    
        public static void Puzzle1()
        {
            Stopwatch watch = Stopwatch.StartNew();
            DiskFragmenter diskFragmenter = new();
            using (FileStream fileStream = File.OpenRead("09-test-input.txt"))
            using (StreamReader streamReader = new(fileStream, Encoding.UTF8, true, BufferSize))
            {
                bool isFile = true;
                int id = 0;
                while (streamReader.Peek() >= 0)
                {
                    char numBlocks = (char)streamReader.Read();
                    try
                    {
                        diskFragmenter.AllocateSpace(int.Parse(numBlocks.ToString()), isFile, id.ToString());
                    }
                    catch (FormatException)
                    {
                        break;
                    }
                    if (isFile)
                    {
                        id++;
                    }
                    isFile = !isFile;
                }
            }
            
            // diskFragmenter.PrintDisk();
            // diskFragmenter.PrintFreeBlocks();
            Console.WriteLine($"Checksum: {diskFragmenter.CalculateFileSystemChecksum()}");
            
            diskFragmenter.FragmentDisk();
            // diskFragmenter.PrintDisk();
            // diskFragmenter.PrintFreeBlocks();
            Console.WriteLine($"Checksum: {diskFragmenter.CalculateFileSystemChecksum()}");
            watch.Stop();
            Console.WriteLine($"Puzzle 1 execution time: {watch.ElapsedMilliseconds} ms");
        }
        
        public static void Puzzle2()
        {
            Stopwatch watch = Stopwatch.StartNew();
            DiskFragmenter diskFragmenter = new();
            using (FileStream fileStream = File.OpenRead("09-input.txt"))
            using (StreamReader streamReader = new(fileStream, Encoding.UTF8, true, BufferSize))
            {
                bool isFile = true;
                int id = 0;
                while (streamReader.Peek() >= 0)
                {
                    char numBlocks = (char)streamReader.Read();
                    try
                    {
                        diskFragmenter.AllocateSpace(int.Parse(numBlocks.ToString()), isFile, id.ToString());
                    }
                    catch (FormatException)
                    {
                        break;
                    }
                    if (isFile)
                    {
                        id++;
                    }
                    isFile = !isFile;
                }
            }
            
            // diskFragmenter.PrintDisk();
            // diskFragmenter.PrintContiguousFreeBlocks();
            // diskFragmenter.PrintFileBlocks();
            Console.WriteLine($"Checksum: {diskFragmenter.CalculateFileSystemChecksum()}");
            
            diskFragmenter.DefragmentDisk();
            // diskFragmenter.PrintDisk();
            Console.WriteLine($"Checksum: {diskFragmenter.CalculateFileSystemChecksum()}");
            watch.Stop();
            Console.WriteLine($"Puzzle 2 execution time: {watch.ElapsedMilliseconds} ms");
        }
    }
}
