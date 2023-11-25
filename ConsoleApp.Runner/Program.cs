using System;
using System.Collections.Generic;


// Create an instance of the solution class
var solution = new Solution();

// Example lists to merge
var lists = new[]
{
    CreateList(new int[] { 1, 4, 5 }),
    CreateList(new int[] { 1, 3, 4 }),
    CreateList(new int[] { 2, 6 })
};

// Merge the lists
var mergedList = solution.MergeKLists(lists);

// Print the merged list
Console.WriteLine("Merged List: ");
PrintList(mergedList);




// Helper method to create a linked list from an array of integers
static ListNode CreateList(int[] values)
{
    if (values == null || values.Length == 0) return null;

    var dummy = new ListNode(0);
    var current = dummy;
    foreach (var val in values)
    {
        current.next = new ListNode(val);
        current = current.next;
    }

    return dummy.next;
}

// Helper method to print the elements of a linked list
static void PrintList(ListNode head)
{
    var current = head;
    while (current != null)
    {
        Console.Write(current.val + " -> ");
        current = current.next;
    }

    Console.WriteLine("null");
}