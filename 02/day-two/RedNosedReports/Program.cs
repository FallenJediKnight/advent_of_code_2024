using RedNosedReports;

StreamReader reader = File.OpenText("02-input.txt");
int safeReports = 0;
while (reader.ReadLine() is { } line) 
{
    string[] items = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
    List<int> reportLevels = items.Select(int.Parse).ToList();
    if (SafetyCheckService.IsSafe(reportLevels))
    {
        safeReports++;
    }
    else
    {
        for (int i = 0; i < reportLevels.Count; i++)
        {
            if (SafetyCheckService.IsSafe(reportLevels.Where((_, index) => index != i).ToList()))
            {
                safeReports++;
                break;
            }
        }
    }
}
reader.Close();

Console.WriteLine(safeReports);
