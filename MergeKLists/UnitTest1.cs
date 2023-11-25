using NUnit.Framework;

[TestFixture]
public class MergeKListsTests
{
    private Solution _solution;

    [SetUp]
    public void SetUp() {
        _solution = new Solution();
    }

    private ListNode CreateList(int[] values) {
        if (values == null || values.Length == 0) return null;

        ListNode dummy = new ListNode(0);
        ListNode current = dummy;
        foreach (var val in values) {
            current.next = new ListNode(val);
            current = current.next;
        }
        return dummy.next;
    }

    private bool AreListsEqual(ListNode l1, ListNode l2) {
        while (l1 != null && l2 != null) {
            if (l1.val != l2.val) return false;
            l1 = l1.next;
            l2 = l2.next;
        }
        return l1 == null && l2 == null;
    }

    [Test]
    public void Test_MergeKLists_EmptyLists() {
        ListNode[] lists = new ListNode[] { CreateList(new int[] { }), CreateList(new int[] { }) };
        ListNode result = _solution.MergeKLists(lists);
        Assert.IsNull(result);
    }

    [Test]
    public void Test_MergeKLists_SingleElementLists() {
        ListNode[] lists = new ListNode[] { CreateList(new int[] { 1 }), CreateList(new int[] { 2 }) };
        ListNode result = _solution.MergeKLists(lists);
        Assert.IsTrue(AreListsEqual(result, CreateList(new int[] { 1, 2 })));
    }

    [Test]
    public void Test_MergeKLists_MultipleLists() {
        ListNode[] lists = new ListNode[] { CreateList(new int[] { 1, 4, 5 }), CreateList(new int[] { 1, 3, 4 }), CreateList(new int[] { 2, 6 }) };
        ListNode result = _solution.MergeKLists(lists);
        Assert.IsTrue(AreListsEqual(result, CreateList(new int[] { 1, 1, 2, 3, 4, 4, 5, 6 })));
    }
    
    [Test]
    public void Test_MergeKLists_NullInput() {
        ListNode result = _solution.MergeKLists(null);
        Assert.IsNull(result);
    }

    [Test]
    public void Test_MergeKLists_SingleList() {
        ListNode[] lists = new ListNode[] { CreateList(new int[] { 1, 2, 3 }) };
        ListNode result = _solution.MergeKLists(lists);
        Assert.IsTrue(AreListsEqual(result, CreateList(new int[] { 1, 2, 3 })));
    }

    [Test]
    public void Test_MergeKLists_ListsWithNullElements() {
        ListNode[] lists = new ListNode[] { null, CreateList(new int[] { 1, 3 }), null, CreateList(new int[] { 2, 4 }) };
        ListNode result = _solution.MergeKLists(lists);
        Assert.IsTrue(AreListsEqual(result, CreateList(new int[] { 1, 2, 3, 4 })));
    }

    [Test]
    public void Test_MergeKLists_ListsWithLargeNumbers() {
        ListNode[] lists = new ListNode[] { CreateList(new int[] { 1000, 1001 }), CreateList(new int[] { 1002, 1003 }), CreateList(new int[] { 999 }) };
        ListNode result = _solution.MergeKLists(lists);
        Assert.IsTrue(AreListsEqual(result, CreateList(new int[] { 999, 1000, 1001, 1002, 1003 })));
    }

    [Test]
    public void Test_MergeKLists_ListsWithNegativeNumbers() {
        ListNode[] lists = new ListNode[] { CreateList(new int[] { -3, -2, -1 }), CreateList(new int[] { -4, -3, -2 }) };
        ListNode result = _solution.MergeKLists(lists);
        Assert.IsTrue(AreListsEqual(result, CreateList(new int[] { -4, -3, -3, -2, -2, -1 })));
    }

    [Test]
    public void Test_MergeKLists_EmptyAndNonEmptyLists() {
        ListNode[] lists = new ListNode[] { CreateList(new int[] { }), CreateList(new int[] { 1, 2, 3 }) };
        ListNode result = _solution.MergeKLists(lists);
        Assert.IsTrue(AreListsEqual(result, CreateList(new int[] { 1, 2, 3 })));
    }
    [Test]
    public void Test_MergeKLists_AllListsEmpty() {
        ListNode[] lists = new ListNode[] { CreateList(new int[] { }), CreateList(new int[] { }), CreateList(new int[] { }) };
        ListNode result = _solution.MergeKLists(lists);
        Assert.IsNull(result);
    }

    [Test]
    public void Test_MergeKLists_SingleNodeLists() {
        ListNode[] lists = new ListNode[] { CreateList(new int[] { 1 }), CreateList(new int[] { 2 }), CreateList(new int[] { 3 }) };
        ListNode result = _solution.MergeKLists(lists);
        Assert.IsTrue(AreListsEqual(result, CreateList(new int[] { 1, 2, 3 })));
    }

    

    [Test]
    public void Test_MergeKLists_ListsWithDuplicates() {
        ListNode[] lists = new ListNode[] { CreateList(new int[] { 1, 3, 5 }), CreateList(new int[] { 1, 3, 5 }), CreateList(new int[] { 1, 3, 5 }) };
        ListNode result = _solution.MergeKLists(lists);
        Assert.IsTrue(AreListsEqual(result, CreateList(new int[] { 1, 1, 1, 3, 3, 3, 5, 5, 5 })));
    }
}