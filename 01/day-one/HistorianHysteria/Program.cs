StreamReader reader = File.OpenText("01-input.txt");
List<int> list1 = [];
List<int> list2 = [];
while (reader.ReadLine() is { } line) 
{
    string[] items = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
    int item1 = int.Parse(items[0]);
    int item2 = int.Parse(items[1]);
    list1.Add(item1);
    list2.Add(item2);
}
reader.Close();

var listDistanceService = new ListDistanceService.ListDistanceService(list1.ToArray(), list2.ToArray());

// Day 1, puzzle 1 solution: 2430334
// Console.WriteLine(listDistanceService.GetTotalDistance());

// Day 1, puzzle 2 solution: 28786472
Console.WriteLine(listDistanceService.GetSimilarityScore());
