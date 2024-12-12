using System.Diagnostics;
using System.Text;

namespace BridgeRepair
{
    internal static class Program
    {
        public static void Main(string[] args)
        {
            // DaySeven.Puzzle1();
            DaySeven.Puzzle2();
        }
    }
    
    internal static class DaySeven
    {
        private const int BufferSize = 128;
    
        public static void Puzzle1()
        {
            Stopwatch watch = Stopwatch.StartNew();
            double total = 0;
            using (FileStream fileStream = File.OpenRead("07-test-input.txt"))
            using (StreamReader streamReader = new(fileStream, Encoding.UTF8, true, BufferSize))
            {
                while (streamReader.ReadLine() is { } line)
                {
                    total += BridgeRepair.LineCanBeMadeTrue(line, 2);
                }
            }
            
            Console.WriteLine($"Total: {total}");
            watch.Stop();
            Console.WriteLine($"Puzzle 1 execution time: {watch.ElapsedMilliseconds} ms");
        }
        
        public static void Puzzle2()
        {
            Stopwatch watch = Stopwatch.StartNew();
            double total = 0;
            using (FileStream fileStream = File.OpenRead("07-input.txt"))
            using (StreamReader streamReader = new(fileStream, Encoding.UTF8, true, BufferSize))
            {
                while (streamReader.ReadLine() is { } line)
                {
                    total += BridgeRepair.LineCanBeMadeTrue(line, 3);
                }
            }
            
            Console.WriteLine($"Total: {total}");
            watch.Stop();
            Console.WriteLine($"Puzzle 1 execution time: {watch.ElapsedMilliseconds} ms");
        }
    }
}