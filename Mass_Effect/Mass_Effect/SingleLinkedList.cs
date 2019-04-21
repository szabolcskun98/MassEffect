using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mass_Effect
{
    class LNode<T>
    {
        private T data;
        private LNode<T> next;

        public LNode(T data)
        {
            this.data = data;
        }

        public void setNext(LNode<T> next)
        {
            this.next = next;
        }

        public LNode<T> getNext()
        {
            return this.next;
        }

        public T getValue()
        {
            return this.data;
        }

        public void setValue(T data)
        {
            this.data = data;
        }
    }

    class SingleLinkedList<T> : IEnumerable<T>
    {
        public LNode<T> Head
        {
            get
            {
                return head;
            }
        }
        public LNode<T> Tail
        {
            get
            {
                return tail;
            }
        }

        private LNode<T> head;
        private LNode<T> tail;

        public void addFirst(LNode<T> node)
        {
            if (head == null)
            {
                head = tail = node;
                head.setNext(tail);
            }
            else
            {
                node.setNext(head);
                head = node;
            }
        }

        public void addLast(LNode<T> node)
        {
            if (tail == null)
            {
                tail = head = node;
                head.setNext(tail);
            }
            else
            {
                tail.setNext(node);
                tail = node;
            }
        }

        public void insertAfter(LNode<T> nodeBefore, LNode<T> node)
        {
            LNode<T> nodeAfter = nodeBefore.getNext();
            nodeBefore.setNext(node);
            node.setNext(nodeAfter);
        }

        public LNode<T> findNodeBefore(LNode<T> node)
        {
            if (object.ReferenceEquals(head, node)) return null;
            LNode<T> nodeBefore = head;
            LNode<T> cursor = head;
            while (cursor != null)
            {
                if (object.ReferenceEquals(node, cursor)) return nodeBefore;
                nodeBefore = cursor;
                cursor = cursor.getNext();
            }
            return null;
        }

        public void removeLink(LNode<T> node)
        {
            LNode<T> nodeBefore = findNodeBefore(node);
            if (nodeBefore == null) return;
            if (object.ReferenceEquals(head, node)) head = node;
            if (object.ReferenceEquals(tail, node)) tail = nodeBefore;
            nodeBefore.setNext(node.getNext());
        }

        public void remove(T item)
        {
            LNode<T> h = head;
            LNode<T> e = null;

            while (h != null && !h.getValue().Equals(item))
            {
                e = h;
                h = h.getNext();
            }
            if (h != null)
            {
                if (h.getNext() == null)
                {
                    tail = e;
                    e.setNext(null);
                }
                else if (e == null)
                {
                    head = head.getNext();
                }
                else
                {
                    e.setNext(h.getNext());
                }
            }
        }

        public void displayAllNodes()
        {
            LNode<T> cursor = head;
            while (cursor != null)
            {
                Console.WriteLine(cursor.getValue().ToString());
                cursor = cursor.getNext();
            }
        }

        public delegate void Operation(T item);

        public void Traverse(Operation o)
        {
            LNode<T> node = head;
            while (node != null)
            {
                o(node.getValue());
                node = node.getNext();
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
            LNode<T> node;
            LNode<T> cursor;

            public ListEnumerator(LNode<T> node)
            {
                this.node = node;
                this.cursor = null;
            }

            public T Current => cursor.getValue();

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
                    cursor = cursor.getNext();
                }
                return cursor != null;
            }

            public void Reset()
            {
                cursor = null;
            }
        }
    }
}
