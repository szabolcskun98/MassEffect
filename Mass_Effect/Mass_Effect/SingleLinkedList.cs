using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mass_Effect
{
    class Node<T>
    {
        private T data;
        private Node<T> next;

        public Node(T data)
        {
            this.data = data;
        }

        public void setNext(Node<T> next)
        {
            this.next = next;
        }

        public Node<T> getNext()
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
        public Node<T> Head
        {
            get
            {
                return head;
            }
        }
        public Node<T> Tail
        {
            get
            {
                return tail;
            }
        }

        private Node<T> head;
        private Node<T> tail;

        public void addFirst(Node<T> node)
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

        public void addLast(Node<T> node)
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

        public void insertAfter(Node<T> nodeBefore, Node<T> node)
        {
            Node<T> nodeAfter = nodeBefore.getNext();
            nodeBefore.setNext(node);
            node.setNext(nodeAfter);
        }

        public Node<T> findNodeBefore(Node<T> node)
        {
            if (object.ReferenceEquals(head, node)) return null;
            Node<T> nodeBefore = head;
            Node<T> cursor = head;
            while (cursor != null)
            {
                if (object.ReferenceEquals(node, cursor)) return nodeBefore;
                nodeBefore = cursor;
                cursor = cursor.getNext();
            }
            return null;
        }

        public void removeLink(Node<T> node)
        {
            Node<T> nodeBefore = findNodeBefore(node);
            if (nodeBefore == null) return;
            if (object.ReferenceEquals(head, node)) head = node;
            if (object.ReferenceEquals(tail, node)) tail = nodeBefore;
            nodeBefore.setNext(node.getNext());
        }

        public void remove(T item)
        {
            Node<T> h = head;
            Node<T> e = null;

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
            Node<T> cursor = head;
            while (cursor != null)
            {
                Console.WriteLine(cursor.getValue().ToString());
                cursor = cursor.getNext();
            }
        }

        public delegate void Operation(T item);

        public void Traverse(Operation o)
        {
            Node<T> node = head;
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
            Node<T> node;
            Node<T> cursor;

            public ListEnumerator(Node<T> node)
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
