using System.Text.RegularExpressions;

namespace MullItOver;

public static partial class ParseCorruptedMemoryFile
{
    public static int Parse(string corruptedString)
    {
        Regex r = MulRegex();
        Match m = r.Match(corruptedString);
        int totalSum = 0;
        while (m.Success)
        {
            int x = int.Parse(m.Groups[1].Value);
            int y = int.Parse(m.Groups[2].Value);
            totalSum += x * y;
            m = m.NextMatch();
        }

        return totalSum;
    }
    
    public static int ParsePuzzle2(string corruptedString)
    {
        bool mulEnabled = true;
        int currentDoPosition = 0;
        int currentDontPosition = 0;
        Regex mulRegex = MulRegex();
        Regex doRegex = DoRegex();
        Regex dontRegex = DontRegex();
        Match mulMatches = mulRegex.Match(corruptedString);
        int totalSum = 0;
        int startAt = 0;
        while (mulMatches.Success)
        {
            if (mulEnabled)
            {
                int x = int.Parse(mulMatches.Groups[1].Value);
                int y = int.Parse(mulMatches.Groups[2].Value);
                totalSum += x * y;
            }
            
            if (currentDoPosition < mulMatches.Index)
            {
                Match doMatch = doRegex.Match(corruptedString, startAt);
                if (doMatch.Success)
                {
                    currentDoPosition = doMatch.Index;
                }
            }

            if (currentDontPosition < mulMatches.Index)
            {
                Match dontMatch = dontRegex.Match(corruptedString, startAt);
                if (dontMatch.Success)
                {
                    currentDontPosition = dontMatch.Index;
                }
            }
            startAt = mulMatches.Index + mulMatches.Length + 1;
            
            mulMatches = mulMatches.NextMatch();

            if (currentDontPosition != 0 && currentDontPosition < mulMatches.Index && (currentDoPosition > mulMatches.Index || currentDontPosition > currentDoPosition))
            {
                mulEnabled = false;
            }
            if (currentDoPosition != 0 && currentDoPosition < mulMatches.Index && (currentDontPosition > mulMatches.Index || currentDoPosition > currentDontPosition))
            {
                mulEnabled = true;
            }
        }

        return totalSum;
    }

    [GeneratedRegex(@"mul\((\d+),(\d+)\)", RegexOptions.IgnoreCase, "en-NZ")]
    private static partial Regex MulRegex();
    
    [GeneratedRegex(@"do\(\)", RegexOptions.IgnoreCase, "en-NZ")]
    private static partial Regex DoRegex();
    
    [GeneratedRegex(@"don't\(\)", RegexOptions.IgnoreCase, "en-NZ")]
    private static partial Regex DontRegex();
}