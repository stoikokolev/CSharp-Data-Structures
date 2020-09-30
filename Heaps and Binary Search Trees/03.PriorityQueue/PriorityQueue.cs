using System.Collections.Generic;
using System.Linq;

namespace _03.PriorityQueue
{
    using System;

    public class PriorityQueue<T> : IAbstractHeap<T>
        where T : IComparable<T>
    {
        private List<T> _elements;

        public PriorityQueue()
        {
            this._elements=new List<T>();
        }

        public int Size => this._elements.Count;

        public T Dequeue()
        {
            this.CheckIfEmpty();
            var element = this._elements[this.Size - 1];
            this._elements.RemoveAt(this.Size - 1);
            return element;
        }

        public void Add(T element)
        {
            this._elements.Add(element);
            this._elements.Sort();
        }

        public T Peek()
        {
            this.CheckIfEmpty();
            return this._elements.Last();
        }

        private void CheckIfEmpty()
        {
            if (this._elements.Count<=0)
            {
                throw new InvalidOperationException();
            }
        }
    }
}
