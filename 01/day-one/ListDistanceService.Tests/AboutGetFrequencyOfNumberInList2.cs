namespace ListDistanceService.Tests;

public class AboutGetFrequencyOfNumberInList2
{
    [Fact]
    public void returns_zero_if_queues_are_empty()
    {
        var listDistanceService = new ListDistanceService([], []);
        var result = listDistanceService.GetFrequencyOfNumberInList2(null);

        Assert.Equal(0, result);
    }
    
    [Fact]
    public void returns_zero_if_does_not_appear_in_list2()
    {
        var listDistanceService = new ListDistanceService([1], [2, 3]);
        var result = listDistanceService.GetFrequencyOfNumberInList2(1);

        Assert.Equal(0, result);
    }
    
    [Theory]
    [InlineData(new[] { 3, 4, 2, 1, 3, 3 }, new[] { 4, 3, 5, 3, 9, 3 }, 3, 3)]
    [InlineData(new[] { 3, 4, 2, 1, 3, 3 }, new[] { 4, 3, 5, 3, 9, 3 }, 1, 0)]
    [InlineData(new[] { 3, 4, 2, 1, 3, 3 }, new[] { 4, 3, 5, 3, 9, 3 }, 4, 1)]
    public void returns_number_of_times_number_appears_in_list2(int[] list1, int[] list2, int numberToCheck, int expectedFrequency)
    {
        var listDistanceService = new ListDistanceService(list1, list2);
        var result = listDistanceService.GetFrequencyOfNumberInList2(numberToCheck);

        Assert.Equal(expectedFrequency, result);
    }
}