namespace Problem02.DoublyLinkedList
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class DoublyLinkedList<T> : IAbstractLinkedList<T>
    {
        private Node<T> _head;

        private Node<T> _tail;

        public DoublyLinkedList()
        {
            this._head = null;

            this._tail = null;

            this.Count = 0;
        }

        public DoublyLinkedList(Node<T> item)
        {
            this._head = item;

            this._tail = item;

            this.Count = 1;
        }

        public int Count { get; private set; }

        public void AddFirst(T item)
        {
            var toInsert = new Node<T>(item);

            if (this.Count == 0)
            {
                this._head = this._tail = toInsert;
            }
            else if (this.Count == 1)
            {
                this._head = toInsert;
                this._head.Next = this._tail;
                this._tail.Previous = this._head;
            }
            else
            {
                this._head.Previous = toInsert;
                toInsert.Next = this._head;
                this._head = toInsert;
            }

            this.Count++;

        }

        public void AddLast(T item)
        {
            var toInsert = new Node<T>(item);

            if (this.Count == 0)
            {
                this._head = this._tail = toInsert;
            }
            else if (this.Count == 1)
            {
                this._head.Next = toInsert;
                this._tail = toInsert;
                this._tail.Previous = this._head;
            }
            else
            {
                this._tail.Next = toInsert;
                toInsert.Previous = this._tail;
                this._tail = toInsert;
            }

            this.Count++;
        }

        public T GetFirst()
        {
            this.EnsureNotEmpty();
            return this._head.Item;
        }

        public T GetLast()
        {
            this.EnsureNotEmpty();
            return this._tail.Item;
        }

        public T RemoveFirst()
        {
            this.EnsureNotEmpty();
            var toReturn = this._head.Item;
            if (this.Count == 1)
            {
                this._head = this._tail = null;
            }
            else
            {
                this._head = this._head.Next;
                this._head.Previous = null;
            }

            this.Count--;
            return toReturn;

        }

        public T RemoveLast()
        {
            this.EnsureNotEmpty();
            var toReturn = this._tail.Item;
            if (this.Count == 1)
            {
                this._head = this._tail = null;
            }
            else
            {
                this._tail = this._tail.Previous;
                this._tail.Next = null;
            }

            this.Count--;
            return toReturn;
        }

        public IEnumerator<T> GetEnumerator()
        {
            var current = this._head;

            while (current != null)
            {
                yield return current.Item;
                current = current.Next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();

        private void EnsureNotEmpty()
        {
            if (this.Count == 0)
                throw new InvalidOperationException();
        }

    }
}