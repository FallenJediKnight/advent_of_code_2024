using System.Diagnostics;
using System.Text;

namespace PrintQueue
{
    internal static class Program
    {
        public static void Main(string[] args)
        {
            DayFive.Puzzle1();
            // DayFive.Puzzle2();
        }
    }
    
    internal static class DayFive
    {
        private const int BufferSize = 128;
    
        public static void Puzzle1()
        {
            Stopwatch watch = Stopwatch.StartNew();
            PrintQueue printQueue = new();
            using (FileStream fileStream = File.OpenRead("05-input.txt"))
            using (StreamReader streamReader = new(fileStream, Encoding.UTF8, true, BufferSize)) {
                bool parseSplit = false;
                while (streamReader.ReadLine() is { } line)
                {
                    if (!parseSplit && line.Trim().Equals(string.Empty))
                    {
                        parseSplit = true;
                        continue;
                    }

                    if (!parseSplit)
                    {
                        printQueue.ParsePageOrdering(line);
                        continue;
                    }
                    printQueue.CheckUpdateOrdering(line.Split(",").Select(int.Parse).ToList());
                }
            }
            
            printQueue.PrintMidPointSum();
            Console.WriteLine(printQueue.FixInvalidOrderings());
            watch.Stop();
            Console.WriteLine($"Puzzle 1 execution time: {watch.ElapsedMilliseconds} ms");
        }
        
        // public static void Puzzle2()
        // {
        //     Stopwatch watch = Stopwatch.StartNew();
        //     CeresSearch ceresSearch = new();
        //     using (FileStream fileStream = File.OpenRead("04-input.txt"))
        //     using (StreamReader streamReader = new(fileStream, Encoding.UTF8, true, BufferSize)) {
        //         while (streamReader.ReadLine() is { } line)
        //         {
        //             ceresSearch.Add(line, "m");
        //         }
        //     }
        //     
        //     Console.WriteLine(ceresSearch.SearchPuzzle2("MAS"));
        //     watch.Stop();
        //     Console.WriteLine($"Puzzle 2 execution time: {watch.ElapsedMilliseconds} ms");
        // }
    }
}