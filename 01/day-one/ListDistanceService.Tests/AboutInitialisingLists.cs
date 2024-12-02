namespace ListDistanceService.Tests;

public class AboutInitialisingLists
{
    [Fact]
    public void on_initialisation_lists_are_stored_as_queues()
    {
        var list1 = new[] { 1, 2, 3, 4, 5 };
        var list2 = new[] { 1, 2, 3, 4, 5 };
        var listDistanceService = new ListDistanceService(list1, list2);
        var queue1 = listDistanceService.Queue1;
        var queue2 = listDistanceService.Queue2;

        Assert.True(queue1.GetType() == typeof(Queue<int>));
        Assert.True(queue2.GetType() == typeof(Queue<int>));
    }
}