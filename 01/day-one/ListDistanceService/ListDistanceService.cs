namespace ListDistanceService
{
    public class ListDistanceService
    {
        public Queue<int> Queue1 { get; }
        public Queue<int> Queue2 { get; }
        
        public ListDistanceService(int[] list1, int[] list2)
        {
            Array.Sort(list1);
            Array.Sort(list2);
            Queue1 = new Queue<int>(list1);
            Queue2 = new Queue<int>(list2);
        }
        
        public int GetTotalDistance()
        {
            int totalDistance = 0;
            while (Queue1.Count > 0 && Queue2.Count > 0)
            {
                var smallestDistance = GetDistanceBetweenSmallestNumbers() ?? 0;
                totalDistance += smallestDistance;
            }
            return totalDistance;
        }
        
        public int GetSimilarityScore()
        {
            int similarityScore = 0;
            foreach (var number in Queue1.ToArray())
            {
                similarityScore += number * GetFrequencyOfNumberInList2(number);
            }
            return similarityScore;
        }
        
        public int GetFrequencyOfNumberInList2(int? number)
        {
            return Queue2.Count(n => n == number);
        }
        
        public int? GetDistanceBetweenSmallestNumbers()
        {
            var smallestNumbers = GetSmallestNumbers();
            if (smallestNumbers.Item1 == null || smallestNumbers.Item2 == null)
            {
                return null;
            }
            return Math.Abs((int)smallestNumbers.Item1 - (int)smallestNumbers.Item2);
        }

        public Tuple<int?, int?> GetSmallestNumbers()
        {
            try
            {
                return new Tuple<int?, int?>(Queue1.Dequeue(), Queue2.Dequeue());
            }
            catch (InvalidOperationException)
            {
                return new Tuple<int?, int?>(null, null);
            }
        }
    }
}