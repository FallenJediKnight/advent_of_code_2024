using System.Diagnostics;
using System.Text;

namespace ResonantCollinearity
{
    internal static class Program
    {
        public static void Main(string[] args)
        {
            // DayEight.Puzzle1();
            DayEight.Puzzle2();
        }
    }
    
    internal static class DayEight
    {
        private const int BufferSize = 128;
    
        public static void Puzzle1()
        {
            Stopwatch watch = Stopwatch.StartNew();
            ResonantCollinearity resonantCollinearity = new();
            const string fileName = "08-input.txt";
            int maxNumberOfRows = File.ReadLines(fileName).Count();
            resonantCollinearity.SetMaxRows(maxNumberOfRows);
            using (FileStream fileStream = File.OpenRead(fileName))
            using (StreamReader streamReader = new(fileStream, Encoding.UTF8, true, BufferSize))
            {
                int row = 1;
                while (streamReader.ReadLine() is { } line)
                {
                    resonantCollinearity.ParseLine(line, row);
                    row++;
                }
            }
            
            // resonantCollinearity.PrintFrequencyCoordinates();
            // resonantCollinearity.PrintFrequencyAntinodeCoordinates();
            Console.WriteLine($"Total number of unique antinodes: {resonantCollinearity.GetTotalNumberOfAntinodeCoordinates()}");
            watch.Stop();
            Console.WriteLine($"Puzzle 1 execution time: {watch.ElapsedMilliseconds} ms");
        }
        
        public static void Puzzle2()
        {
            Stopwatch watch = Stopwatch.StartNew();
            ResonantCollinearity resonantCollinearity = new();
            const string fileName = "08-input.txt";
            int maxNumberOfRows = File.ReadLines(fileName).Count();
            resonantCollinearity.SetMaxRows(maxNumberOfRows);
            using (FileStream fileStream = File.OpenRead(fileName))
            using (StreamReader streamReader = new(fileStream, Encoding.UTF8, true, BufferSize))
            {
                int row = 1;
                while (streamReader.ReadLine() is { } line)
                {
                    resonantCollinearity.ParseLine2(line, row);
                    row++;
                }
            }
            
            resonantCollinearity.PrintFrequencyCoordinates();
            resonantCollinearity.PrintFrequencyAntinodeCoordinates();
            Console.WriteLine($"Total number of unique antinodes: {resonantCollinearity.GetTotalNumberOfAntinodeCoordinates()}");
            watch.Stop();
            Console.WriteLine($"Puzzle 2 execution time: {watch.ElapsedMilliseconds} ms");
        }
    }
}
