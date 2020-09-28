namespace Problem03.Queue
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class Queue<T> : IAbstractQueue<T>
    {
        private Node<T> _head;

        public Queue()
        {
            this._head = null;
            this.Count = 0;
        }

        public Queue(Node<T> node)
        {
            this._head = node;
            this.Count = 1;
        }

        public int Count { get; private set; }

        public bool Contains(T item)
        {
            var current = this._head;
            while (current != null)
            {
                if (current.Value.Equals(item))
                {
                    return true;
                }
                current = current.Next;
            }

            return false;
        }

        public T Dequeue()
        {
            this.CheckIfEmpty();
            var toReturn = this._head.Value;
            this._head = this._head.Next;
            this.Count--;
            return toReturn;
        }

        public void Enqueue(T item)
        {
            var toInsert = new Node<T>(item);
            if (this.Count == 0)
            {
                this._head = toInsert;
            }
            else
            {
                var current = this._head;
                while (current.Next != null)
                {
                    current = current.Next;
                }

                current.Next = toInsert;
            }
            this.Count++;
        }

        public T Peek()
        {
            this.CheckIfEmpty();
            return this._head.Value;
        }

        public IEnumerator<T> GetEnumerator()
        {
            var current = this._head;
            while (current.Next != null)
            {
                yield return current.Value;
                current = current.Next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
            => this.GetEnumerator();

        private void CheckIfEmpty()
        {
            if (this.Count == 0)
            {
                throw new InvalidOperationException();
            }
        }
    }
}