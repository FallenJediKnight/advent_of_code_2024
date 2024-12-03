using System.Diagnostics;
using System.Text;
using MullItOver;

DayThree.Puzzle1();
DayThree.Puzzle2();

internal static class DayThree
{
    public static void Puzzle1()
    {
        Stopwatch watch = Stopwatch.StartNew();
        int puzzle1Sum;
        using (StreamReader streamReader = new("03-input.txt", Encoding.UTF8))
        {
            string corruptedMemory = streamReader.ReadToEnd();
            puzzle1Sum = ParseCorruptedMemoryFile.Parse(corruptedMemory);
        }
        Console.WriteLine(puzzle1Sum);
        watch.Stop();
        Console.WriteLine($"Puzzle 1 execution time: {watch.ElapsedMilliseconds} ms");
    }
    
    public static void Puzzle2()
    {
        Stopwatch watch = Stopwatch.StartNew();
        int puzzle2Sum;
        using (StreamReader streamReader = new("03-input.txt", Encoding.UTF8))
        {
            string corruptedMemory = streamReader.ReadToEnd();
            puzzle2Sum = ParseCorruptedMemoryFile.ParsePuzzle2(corruptedMemory);
        }
        Console.WriteLine(puzzle2Sum);
        watch.Stop();
        Console.WriteLine($"Puzzle 2 execution time: {watch.ElapsedMilliseconds} ms");
    }
}
