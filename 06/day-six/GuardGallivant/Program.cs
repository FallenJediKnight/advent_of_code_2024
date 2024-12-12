using System.Diagnostics;
using System.Text;

namespace GuardGallivant
{
    internal static class Program
    {
        public static void Main(string[] args)
        {
            // DaySix.Puzzle1();
            DaySix.Puzzle2();
        }
    }
    
    internal static class DaySix
    {
        private const int BufferSize = 128;
    
        public static void Puzzle1()
        {
            Stopwatch watch = Stopwatch.StartNew();
            GuardGallivant guardGallivant = new();
            using (FileStream fileStream = File.OpenRead("06-input.txt"))
            using (StreamReader streamReader = new(fileStream, Encoding.UTF8, true, BufferSize)) {
                int row = 0;
                while (streamReader.ReadLine() is { } line)
                {
                    guardGallivant.Parse(line, row);
                    row++;
                }
            }
            
            // guardGallivant.PrintObstacles();
            // guardGallivant.PrintCurrentGuardPositionAndFacing();
            // Console.WriteLine(guardGallivant._maxNumberOfRows);
            // Console.WriteLine(guardGallivant._maxNumberOfColumns);
            Console.WriteLine($"Number of positions visited: {guardGallivant.GetNumberOfPositionsVisited()}");
            // guardGallivant.PrintPositionsVisited();
            watch.Stop();
            Console.WriteLine($"Puzzle 1 execution time: {watch.ElapsedMilliseconds} ms");
        }
        
        public static void Puzzle2()
        {
            Stopwatch watch = Stopwatch.StartNew();
            GuardGallivant guardGallivant = new();
            using (FileStream fileStream = File.OpenRead("06-input.txt"))
            using (StreamReader streamReader = new(fileStream, Encoding.UTF8, true, BufferSize)) {
                int row = 0;
                while (streamReader.ReadLine() is { } line)
                {
                    guardGallivant.Parse2(line, row);
                    row++;
                }
            }
            
            // guardGallivant.PrintObstacles();
            // guardGallivant.PrintCurrentGuardPositionAndFacing();
            // Console.WriteLine(guardGallivant._maxNumberOfRows);
            // Console.WriteLine(guardGallivant._maxNumberOfColumns);
            Console.WriteLine($"Number of positions that would create an infinite loop: {guardGallivant.GetNumberOfPositionsToCreateInfiniteLoop()}");
            // guardGallivant.PrintPositionsVisited();
            watch.Stop();
            Console.WriteLine($"Puzzle 1 execution time: {watch.ElapsedMilliseconds} ms");
        }
    }
}