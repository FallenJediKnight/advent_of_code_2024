using System.Globalization;
using System.Text;

namespace BridgeRepair;

public static class BridgeRepair
{
    public static double LineCanBeMadeTrue(string line, int numValidOperators)
    {
        string[] parts = line.Split(" ");
        double result = double.Parse(parts[0].Replace(":", ""));
        double[] operands = parts[1..].Select(double.Parse).ToArray();
        // Console.WriteLine(result);
        // Console.WriteLine(string.Join(", ", operands));
        return EquationCanBeMadeTrue(result, numValidOperators, operands);
    }
    
    private static double EquationCanBeMadeTrue(double result, int numValidOperators, params double[] operands)
    {
        int numOperators = operands.Length - 1;
        List<string> operatorPermutations = [];
        for (int i = 0; i < Math.Pow(numValidOperators, numOperators); i++)
        {
            string numInBinary = ConvertIntToBase(i, numValidOperators).PadLeft(numOperators, '0');
            if (numValidOperators == 2)
            {
                numInBinary = numInBinary.Replace('0', '+').Replace('1', '*');
            }
            else
            {
                numInBinary = numInBinary.Replace('0', '+').Replace('1', '*').Replace('2', '|');
            }
            operatorPermutations.Add(numInBinary);
        }
        // Console.WriteLine(string.Join(", ", operatorPermutations));

        foreach (string operatorPermutation in operatorPermutations)
        {
            double currentResult = operands[0];
            for (int i = 0; i < numOperators; i++)
            {
                switch (operatorPermutation[i])
                {
                    case '+':
                        currentResult += operands[i + 1];
                        break;
                    case '*':
                        currentResult *= operands[i + 1];
                        break;
                    case '|':
                        string currentResultStr = currentResult.ToString(CultureInfo.InvariantCulture) + operands[i + 1].ToString(CultureInfo.InvariantCulture);
                        currentResult = double.Parse(currentResultStr, CultureInfo.InvariantCulture);
                        break;
                }
            }
            if (currentResult == result)
            {
                return result;
            }
        }

        return 0;
    }

    private static string ConvertIntToBase(int i, int newBase){
        StringBuilder builder = new();
        do {
            builder.Append(i % newBase);
            i /= newBase;
        } while(i > 0);

        List<char> result = builder.ToString().ToList();
        result.Reverse();
        return string.Join("", result);
    }
}
