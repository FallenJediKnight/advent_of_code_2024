using System.Text;
using MullItOver;

int puzzle1Sum;
int puzzle2Sum;
using (StreamReader streamReader = new("03-input.txt", Encoding.UTF8))
{
    string corruptedMemory = streamReader.ReadToEnd();
    puzzle1Sum = ParseCorruptedMemoryFile.Parse(corruptedMemory);
    puzzle2Sum = ParseCorruptedMemoryFile.ParsePuzzle2(corruptedMemory);
}

Console.WriteLine(puzzle1Sum);
Console.WriteLine(puzzle2Sum);
