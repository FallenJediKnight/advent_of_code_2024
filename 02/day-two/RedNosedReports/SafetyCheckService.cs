namespace RedNosedReports;

public static class SafetyCheckService
{
    public static bool IsSafe(List<int> reportLevels)
    {
        var ascendingReportLevels = reportLevels.OrderBy(x => x);
        var descendingReportLevels = reportLevels.OrderByDescending(x => x);

        if (!reportLevels.SequenceEqual(ascendingReportLevels) && !reportLevels.SequenceEqual(descendingReportLevels)) return false;
        
        for (int i = 1; i < reportLevels.Count(); i++)
        {
            var firstReportLevel = reportLevels.ElementAt(i - 1);
            var secondReportLevel = reportLevels.ElementAt(i);
            var difference = Math.Abs(secondReportLevel - firstReportLevel);
            if (difference is > 3 or < 1)
            {
                return false;
            }
        }
        return true;
    }
}