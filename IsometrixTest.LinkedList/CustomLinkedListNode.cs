namespace IsometrixTest.LinkedList
{
    /// <summary>A single node in a custom linked list.</summary>
    /// <typeparam name="T">Type of the value.</typeparam>
    internal class CustomLinkedListNode<T>
    {
        /// <summary>Value of the node.</summary>
        public T Value { get; }
        /// <summary>Previous node in the list. Null for first node.</summary>
        public CustomLinkedListNode<T> PreviousNode { get; internal set; }
        /// <summary>Next node in the list. Null for last node.</summary>
        public CustomLinkedListNode<T> NextNode { get; internal set; }

        /// <summary>Whether this node is the first node in the list.</summary>
        public bool IsFirst => this.PreviousNode == null;
        /// <summary>Whether this node is the last node in the list.</summary>
        public bool IsLast => this.NextNode == null;

        public CustomLinkedListNode(T value)
        {
            this.Value = value;
        }

        public override string ToString()
            => this.Value.ToString();
    }
}
