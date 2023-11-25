
// Definition for singly-linked list.
public class ListNode {
    public int val;
    public ListNode next;
    public ListNode(int val = 0, ListNode next = null) {
        this.val = val;
        this.next = next;
    }
}

public class Solution {
    /// <summary>
    /// Merges a list of sorted linked lists into a single sorted linked list.
    /// </summary>
    /// <remarks>
    /// This method uses a min-priority queue to efficiently merge the lists.
    /// Time Complexity: O(N log k), where N is the total number of nodes across all lists and k is the number of linked lists.
    /// Each insertion and deletion operation in the priority queue takes O(log k) time.
    /// Space Complexity: O(k), which is the space used by the priority queue.
    /// At any point, the priority queue holds at most one node from each list.
    /// </remarks>
    /// <param name="lists">An array of ListNode, where each ListNode is the head of a sorted linked list.</param>
    /// <returns>The head of the merged sorted linked list.</returns>
    public ListNode MergeKLists(ListNode[] lists) {
        // Validate input to handle edge cases
        if (lists == null || lists.Length == 0) return null;

        // PriorityQueue to store the nodes, ordered by their values
        var minHeap = new PriorityQueue<ListNode, int>();

        // Enqueue the first node of each list (if not null) to the priority queue
        foreach (var list in lists) {
            if (list != null) {
                minHeap.Enqueue(list, list.val);
            }
        }

        // Dummy head to simplify the merging process
        var dummy = new ListNode(0);
        var tail = dummy;

        // While the queue is not empty, dequeue the smallest node and add it to the merged list
        while (minHeap.Count > 0) {
            var node = minHeap.Dequeue();
            tail.next = node;
            tail = tail.next;

            // If the next node in the current list is not null, enqueue it
            if (node.next != null) {
                minHeap.Enqueue(node.next, node.next.val);
            }
        }

        // Return the head of the merged list, skipping the dummy head
        return dummy.next;
    }
}