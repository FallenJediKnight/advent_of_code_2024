using System.Diagnostics;
using System.Text;

namespace CeresSearch
{
    internal static class Program
    {
        public static void Main(string[] args)
        {
            // DayFour.Puzzle1();
            DayFour.Puzzle2();
        }
    }
    
    internal static class DayFour
    {
        private const int BufferSize = 128;
    
        public static void Puzzle1()
        {
            Stopwatch watch = Stopwatch.StartNew();
            CeresSearch ceresSearch = new();
            using (FileStream fileStream = File.OpenRead("04-test-input.txt"))
            using (StreamReader streamReader = new(fileStream, Encoding.UTF8, true, BufferSize)) {
                while (streamReader.ReadLine() is { } line)
                {
                    ceresSearch.Add(line, "x");
                }
            }
            
            Console.WriteLine(ceresSearch.SearchPuzzle1("XMAS"));
            watch.Stop();
            Console.WriteLine($"Puzzle 1 execution time: {watch.ElapsedMilliseconds} ms");
        }
        
        public static void Puzzle2()
        {
            Stopwatch watch = Stopwatch.StartNew();
            CeresSearch ceresSearch = new();
            using (FileStream fileStream = File.OpenRead("04-input.txt"))
            using (StreamReader streamReader = new(fileStream, Encoding.UTF8, true, BufferSize)) {
                while (streamReader.ReadLine() is { } line)
                {
                    ceresSearch.Add(line, "m");
                }
            }
            
            Console.WriteLine(ceresSearch.SearchPuzzle2("MAS"));
            watch.Stop();
            Console.WriteLine($"Puzzle 2 execution time: {watch.ElapsedMilliseconds} ms");
        }
    }
}