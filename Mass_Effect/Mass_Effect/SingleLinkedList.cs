using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mass_Effect
{
    class SLinkedListNode<T>
    {
        T value;
        SLinkedListNode<T> next;
        public SLinkedListNode(T value, SLinkedListNode<T> next)
        {
            this.value = value;
            this.next = next;
        }

        public T Value
        {
            get
            {
                return value;
            }
            set
            {
                this.value = value;
            }
        }

        public SLinkedListNode<T> Next
        {
            get { return next; }
            set { next = value; }
        }
    }

    abstract class SingleLinkedList<T> : IEnumerable<T>
    {
        protected SLinkedListNode<T> head;
        protected int count;

        protected SingleLinkedList()
        {
            this.head = null;
            this.count = 0;
        }

        public int Count
        {
            get { return count; }
        }

        public SLinkedListNode<T> Head
        {
            get { return head; }
        }

        public abstract void Add(T item);

        public void Clear()
        {
            if (head != null)
            {
                SLinkedListNode<T> previousNode = head;
                SLinkedListNode<T> currentNode = head.Next;
                previousNode.Next = null;
                while (currentNode != null)
                {
                    previousNode = currentNode;
                    currentNode = currentNode.Next;
                    previousNode.Next = null;
                }
            }

            head = null;
            count = 0;
        }
        public bool Remove(T item)
        {
            if (head == null) return false;
            else if (head.Value.Equals(item))
            {
                head = head.Next;
                count--;
                return true;
            }
            else
            {
                SLinkedListNode<T> previousNode = head;
                SLinkedListNode<T> currentNode = head.Next;
                while (currentNode != null && !currentNode.Value.Equals(item))
                {
                    previousNode = currentNode;
                    currentNode = currentNode.Next;
                }

                if (currentNode == null) return false;
                else
                {
                    previousNode.Next = currentNode.Next;
                    count--;
                    return true;
                }
            }
        }
        public SLinkedListNode<T> Find(T item)
        {
            SLinkedListNode<T> currentNode = head;
            while (currentNode != null && !currentNode.Value.Equals(item))
            {
                currentNode = currentNode.Next;
            }

            if (currentNode != null) return currentNode;
            else return null;
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            SLinkedListNode<T> currentNode = head;
            int nodeCount = 0;
            builder.Append("[");
            while (currentNode != null)
            {
                nodeCount++;
                builder.Append(currentNode.Value.ToString());
                if (nodeCount < count)
                {
                    builder.Append(",");
                }
                currentNode = currentNode.Next;
            }
            builder.Append("]");
            return builder.ToString();
        }

        public delegate void Operation(T item);
        public void Traverse(Operation o)
        {
            SLinkedListNode<T> node = head;
            while (node != null)
            {
                o(node.Value);
                node = node.Next;
            }
        }

        public T this[int index]
        {
            get
            {
                if (index < count && index >= 0)
                {
                    SLinkedListNode<T> currentNode = head;
                    int idx = 0;
                    while (idx < count && currentNode != null)
                    {
                        if (idx == index) return currentNode.Value;
                        currentNode = currentNode.Next;
                        idx++;
                    }
                }
                return default(T);
            }
            set
            {
                if (index < count && index >= 0)
                {
                    SLinkedListNode<T> currentNode = head;
                    int idx = 0;
                    while (idx < count && currentNode != null)
                    {
                        if (idx == index) break;
                        currentNode = currentNode.Next;
                        idx++;
                    }
                    if (currentNode != null && idx == index) currentNode.Value = value;
                }
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new ListEnumerator(head);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        class ListEnumerator : IEnumerator<T>
        {
            SLinkedListNode<T> node;
            SLinkedListNode<T> cursor;

            public ListEnumerator(SLinkedListNode<T> node)
            {
                this.node = node;
                this.cursor = null;
            }

            public T Current => cursor.Value;

            object IEnumerator.Current => this.Current;

            public void Dispose()
            {
                node = null;
                cursor = null;
            }

            public bool MoveNext()
            {
                if (cursor == null)
                {
                    cursor = node;
                }
                else
                {
                    cursor = cursor.Next;
                }
                return cursor != null;
            }

            public void Reset()
            {
                cursor = null;
            }
        }
    }

    class UnsortedLinkedList<T> : SingleLinkedList<T>
    {
        public UnsortedLinkedList() : base()
        {
        }

        public override void Add(T item)
        {
            if (head == null) head = new SLinkedListNode<T>(item, null);
            else head = new SLinkedListNode<T>(item, head);
            count++;
        }
    }

    class SortedLinkedList<T> : SingleLinkedList<T> where T : IComparable
    {
        public SortedLinkedList() : base()
        {
        }

        public override void Add(T item)
        {
            if (head == null) head = new SLinkedListNode<T>(item, null);
            else if (head.Value.CompareTo(item) > 0) head = new SLinkedListNode<T>(item, head);
            else
            {
                SLinkedListNode<T> previousNode = null;
                SLinkedListNode<T> currentNode = head;
                while (currentNode != null && currentNode.Value.CompareTo(item) < 0)
                {
                    previousNode = currentNode;
                    currentNode = currentNode.Next;
                }
                previousNode.Next = new SLinkedListNode<T>(item, currentNode);
            }
            count++;
        }
    }
}