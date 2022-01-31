using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IsometrixTest.LinkedList
{
    /// <summary>A custom generic linked list.</summary>
    /// <typeparam name="T">Type of elements in the list.</typeparam>
    public class CustomLinkedList<T>
    {
        private CustomLinkedListNode<T> _firstNode;

        public void Insert(T item, int index)
        {
            if (index < 0)
                throw new IndexOutOfRangeException();

            CustomLinkedListNode<T> newNode = new CustomLinkedListNode<T>(item);

            // special case - inserting at the beginning
            if (index == 0)
            {
                newNode.NextNode = this._firstNode;
                this._firstNode = newNode;
            }
            else
            {
                // navigate through list until either at position, or out of index bounds
                CustomLinkedListNode<T> lastNode = this._firstNode;
                for (int i = 1; i < index; i++)
                {
                    // null last node means we reached the end, so we're out of bounds now
                    if (lastNode == null)
                        throw new IndexOutOfRangeException();

                    lastNode = lastNode.NextNode;
                }

                // point new node to previous and next nodes
                newNode.NextNode = lastNode.NextNode;
                newNode.PreviousNode = lastNode;
                // unlink next and previous nodes from each other, and point to new node instead
                if (newNode.NextNode != null)
                    newNode.NextNode.PreviousNode = newNode;
                lastNode.NextNode = newNode;
            }
        }

        public void Delete(int index)
        {
            if (index < 0)
                throw new IndexOutOfRangeException();

            // special case - deleting first one
            if (index == 0)
            {
                CustomLinkedListNode<T> nextNode = this._firstNode.NextNode;
                // unlink first node from 2nd one
                this._firstNode.NextNode = null;
                nextNode.PreviousNode = null;
                // set 2nd node as first one
                this._firstNode = nextNode;
            }
            else
            {
                // navigate through list until either at position, or out of index bounds
                CustomLinkedListNode<T> lastNode = this._firstNode;
                for (int i = 1; i < index; i++)
                {
                    // null last node means we reached the end, so we're out of bounds now
                    if (lastNode == null)
                        throw new IndexOutOfRangeException();

                    lastNode = lastNode.NextNode;
                }

                // check the node at index actually exists
                CustomLinkedListNode<T> deletingNode = lastNode.NextNode;
                if (deletingNode == null)
                    throw new IndexOutOfRangeException();
                // link last node to one after deleted
                lastNode.NextNode = deletingNode.NextNode;
                if (lastNode.NextNode != null)
                    lastNode.NextNode.PreviousNode = lastNode;
                // unlink the deleted node from any other node
                deletingNode.PreviousNode = null;
                deletingNode.NextNode = null;
            }
        }

        public void PrintList()
        {
            if (_firstNode == null)
                Console.WriteLine("Empty!");

            CustomLinkedListNode<T> lastNode = this._firstNode;
            while (lastNode != null)
            {
                Console.WriteLine(lastNode.ToString());
                lastNode = lastNode.NextNode;
            }
        }
    }
}
