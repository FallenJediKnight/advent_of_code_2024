namespace ListDistanceService.Tests
{
    public class AboutGetSmallestNumbers
    {
        [Fact]
        public void lists_with_no_numbers_will_return_null()
        {
            var listDistanceService = new ListDistanceService([], []);
            var results = listDistanceService.GetSmallestNumbers();

            Assert.Null(results.Item1);
            Assert.Null(results.Item2);
        }

        [Theory]
        [InlineData(new[] { 1 }, 1, new[] { 1 }, 1)]
        [InlineData(new[] { 100 }, 100, new[] { 100 }, 100)]
        [InlineData(new[] { 1 }, 1, new[] { 100 }, 100)]
        public void lists_with_one_number_will_return_that_number(int[] list1, int expected1, int[] list2, int expected2)
        {
            var listDistanceService = new ListDistanceService(list1, list2);
            var results = listDistanceService.GetSmallestNumbers();

            Assert.Equal(expected1, results.Item1);
            Assert.Equal(expected2, results.Item2);
        }
        
        [Theory]
        [InlineData(new[] { 1, 2 }, 1, new[] { 2, 1 }, 1)]
        [InlineData(new[] { 100, 100 }, 100, new[] { 1, 1 }, 1)]
        [InlineData(new[] { 1000, 2000, 200 }, 200, new[] { 1000, 200, 2000 }, 200)]
        public void the_smallest_number_in_the_list_is_returned(int[] list1, int expected1, int[] list2, int expected2)
        {
            var listDistanceService = new ListDistanceService(list1, list2);
            var results = listDistanceService.GetSmallestNumbers();

            Assert.Equal(expected1, results.Item1);
            Assert.Equal(expected2, results.Item2);
        }
        
        [Theory]
        [InlineData(new[] { 1 }, new[] { 1 })]
        [InlineData(new[] { 1, 1 }, new[] { 1, 1 })]
        [InlineData(new[] { 1, 1, 1, 1, 1 }, new[] { 1, 1, 1, 1, 1 })]
        [InlineData(new[] { 1, 1, 1, 1 }, new[] { 1, 1, 1, 1, 1 })]
        [InlineData(new[] { 1, 1, 1, 1, 1 }, new[] { 1, 1, 1, 1 })]
        public void after_getting_the_smallest_number_the_lists_are_reduced_in_size(int[] list1, int[] list2)
        {
            var listDistanceService = new ListDistanceService(list1, list2);
            _ = listDistanceService.GetSmallestNumbers();
        
            Assert.Equal(list1.Length - 1, listDistanceService.Queue1.Count);
            Assert.Equal(list2.Length - 1, listDistanceService.Queue2.Count);
        }
    }
}