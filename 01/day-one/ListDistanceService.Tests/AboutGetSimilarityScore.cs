namespace ListDistanceService.Tests;

public class AboutGetSimilarityScore
{
    [Fact]
    public void returns_zero_if_queues_are_empty()
    {
        var listDistanceService = new ListDistanceService([], []);
        var result = listDistanceService.GetSimilarityScore();

        Assert.Equal(0, result);
    }

    [Fact]
    public void returns_zero_if_lists_are_completely_different()
    {
        var listDistanceService = new ListDistanceService([1], [2, 3]);
        var result = listDistanceService.GetSimilarityScore();

        Assert.Equal(0, result);
    }
    
    [Theory]
    [InlineData(new[] { 3, 4, 2, 1, 3, 3 }, new[] { 4, 3, 5, 3, 9, 3 }, 31)]
    public void returns_number_of_times_number_appears_in_list2(int[] list1, int[] list2, int expectedSimilarityScore)
    {
        var listDistanceService = new ListDistanceService(list1, list2);
        var result = listDistanceService.GetSimilarityScore();

        Assert.Equal(expectedSimilarityScore, result);
    }
}