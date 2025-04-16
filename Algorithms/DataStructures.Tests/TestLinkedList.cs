namespace DataStructures.Tests;
public class TestLinkedList
{
    [Fact]
    public void TestL()
    {
        int[] testList = { 200, 256, -312, 500 };

        DataStructures.LinkedList<int> ll = new();

        for (int j = testList.Length - 1; j >= 0; j--)
        {
            ll.AddFirst(testList[j]);
        }

        Assert.True(testList.SequenceEqual(ll));

        // remove a value from the list
        ll.Remove(-312);

        Assert.True(ll.SequenceEqual(new int[] { 200, 256, 500 }));

        // remove by idx
        ll.RemoveAt(1);
        
        Assert.True(ll.SequenceEqual(new int[] { 200, 500 }));

        // remove till nothing left

        ll.RemoveAt(1);
        ll.RemoveAt(0);
        Assert.True(ll.Count() == 0);

        // add back stuff
        for (int j = testList.Length - 1; j >= 0; j--)
        {
            ll.AddFirst(testList[j]);
        }
        ll.Clear();
        Assert.True(ll.Count() == 0);

        Assert.Throws<IndexOutOfRangeException>(() => ll[0]);

        ll.Clear();

        // test adding to the end
        ll.Add(50);
        ll.Add(25);

        Assert.True(ll.Count() == 2);
        Assert.True(ll[1] == 25);

        ll[0] = 34;
        Assert.True(ll[0] == 34);

        int[] tester = new int[3];
        ll.CopyTo(tester, 1);
        Assert.True(tester[1] == 34 && tester[2] == 25);

    }
}
