namespace ListDistanceService.Tests;

public class AboutGetDistanceBetweenSmallestNumbers
{
    [Fact]
    public void returns_null_if_queues_are_empty()
    {
        var listDistanceService = new ListDistanceService([], []);
        var result = listDistanceService.GetDistanceBetweenSmallestNumbers();

        Assert.Null(result);
    }
    
    [Fact]
    public void returns_zero_if_numbers_are_the_same()
    {
        var listDistanceService = new ListDistanceService([3, 1, 2], [1, 2, 3]);
        var result = listDistanceService.GetDistanceBetweenSmallestNumbers();

        Assert.Equal(0, result);
    }
    
    [Theory]
    [InlineData(new[] { 3, 4, 2, 1, 3, 3 }, new[] { 4, 3, 5, 3, 9, 3 }, 2)]
    public void the_absolute_distance_between_the_smallest_numbers_is_returned(int[] list1, int[] list2, int expectedDistance)
    {
        var listDistanceService = new ListDistanceService(list1, list2);
        var result = listDistanceService.GetDistanceBetweenSmallestNumbers();

        Assert.Equal(expectedDistance, result);
    }
}