namespace _03.MinHeap
{
    using System;
    using System.Collections.Generic;

    public class MinHeap<T> : IAbstractHeap<T>
        where T : IComparable<T>
    {
        private List<T> _elements;

        public MinHeap()
        {
            this._elements = new List<T>();
        }

        public int Size => this._elements.Count;

        public T Dequeue()
        {
            this.EnsureNotEmpty();
            var toReturn = this._elements[0];
            this._elements.RemoveAt(0);
            return toReturn;
        }

        public void Add(T element)
        {
            this._elements.Add(element);
            this._elements.Sort();
        }

        public T Peek()
        {
            this.EnsureNotEmpty();
            return this._elements[0];
        }

        private void EnsureNotEmpty()
        {
            if (this._elements.Count == 0)
            {
                throw new InvalidOperationException();
            }
        }
    }
}
