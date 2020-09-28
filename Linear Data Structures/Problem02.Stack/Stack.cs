namespace Problem02.Stack
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class Stack<T> : IAbstractStack<T>
    {
        private Node<T> _top;

        public Stack()
        {
            this._top = null;
            this.Count = 0;
        }

        public Stack(Node<T> node)
        {
            this._top = node;
            this.Count = 1;
        }

        public int Count { get; private set; }

        public bool Contains(T item)
        {
            this.CheckIfEmpty();
            var current = this._top;
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

        public T Peek()
        {
            this.CheckIfEmpty();
            return this._top.Value;
        }
        
        public T Pop()
        {
            this.CheckIfEmpty();
            var toReturn = this._top.Value;
            this._top = this._top.Next;
            this.Count--;
            return toReturn;
        }

        public void Push(T item)
        {
            var toInsert = new Node<T>(item);
            toInsert.Next = this._top;
            this._top = toInsert;
            this.Count++;
        }

        public IEnumerator<T> GetEnumerator()
        {
            var current = this._top;
            while (current.Next!= null)
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