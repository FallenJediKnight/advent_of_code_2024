namespace ListDistanceService.Tests;

public class AboutGetTotalDistance
{
    [Fact]
    public void returns_zero_if_queues_are_empty()
    {
        var listDistanceService = new ListDistanceService([], []);
        var result = listDistanceService.GetTotalDistance();

        Assert.Equal(0, result);
    }
    
    [Fact]
    public void returns_distance_between_smallest_numbers_if_only_length_1()
    {
        var listDistanceService = new ListDistanceService([23], [4]);
        var distanceBetweenSmallestNumbers = listDistanceService.GetDistanceBetweenSmallestNumbers();
        listDistanceService = new ListDistanceService([23], [4]);
        var totalDistance = listDistanceService.GetTotalDistance();

        Assert.Equal(distanceBetweenSmallestNumbers, totalDistance);
    }
    
    [Theory]
    [InlineData(new[] { 3, 4, 2, 1, 3, 3 }, new[] { 4, 3, 5, 3, 9, 3 }, 11)]
    public void the_sum_of_the_absolute_distance_between_the_smallest_numbers_is_returned(int[] list1, int[] list2, int expectedTotalDistance)
    {
        var listDistanceService = new ListDistanceService(list1, list2);
        var result = listDistanceService.GetTotalDistance();

        Assert.Equal(expectedTotalDistance, result);
    }
}