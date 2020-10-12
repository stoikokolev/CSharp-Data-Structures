namespace Problem04.SinglyLinkedList
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class SinglyLinkedList<T> : IAbstractLinkedList<T>
    {
        private Node<T> _head;

        public SinglyLinkedList()
        {
            this._head = null;
            this.Count = 0;
        }

        public SinglyLinkedList(Node<T> node)
        {
            this._head = node;
            this.Count = 1;
        }

        public int Count { get; private set; }

        public void AddFirst(T item)
        {
            var toInsert = new Node<T>(item);
            if (this.Count == 0)
            {
                this._head = toInsert;
            }
            else
            {
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

        public T GetFirst()
        {
            this.CheckIfEmpty();
            return this._head.Value;
        }

        public T GetLast()
        {
            this.CheckIfEmpty();
            var currnet = this._head;
            T toReturn = default;

            if (this.Count == 1)
            {
                return this._head.Value;
            }
            for (int i = 0; i < this.Count; i++)
            {
                toReturn = currnet.Value;
                currnet = currnet.Next;
            }


            return toReturn;

        }

        public T RemoveFirst()
        {
            this.CheckIfEmpty();
            var toReturn = this._head;
            if (this.Count == 1)
            {
                this._head = null;

            }
            else
            {
                this._head = this._head.Next;
            }

            this.Count--;
            return toReturn.Value;
        }

        public T RemoveLast()
        {
            this.CheckIfEmpty();
            var currnet = this._head;
            T toReturn = default;
            Node<T> previous = null;
            if (this.Count == 1)
            {
                toReturn = this._head.Value;
                this._head = null;
            }
            else
            {
                for (int i = 0; i < this.Count; i++)
                {
                    previous = currnet;
                    currnet = currnet.Next;
                }

                toReturn = previous.Value;
                previous.Next = null;
            }

            this.Count--;
            return toReturn;
        }

        public IEnumerator<T> GetEnumerator()
        {
            var currnet = this._head;
            while (currnet.Next != null)
            {
                yield return currnet.Value;
                currnet = currnet.Next;
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