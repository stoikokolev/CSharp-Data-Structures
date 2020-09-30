using System.Collections.Generic;
using System.Linq;

namespace _02.MaxHeap
{
    using System;

    public class MaxHeap<T> : IAbstractHeap<T>
        where T : IComparable<T>
    {
        private List<T> _elements = new List<T>();

        public int Size
        {
            get { return this._elements.Count; }
        }

        public void Add(T element)
        {
            
            this._elements.Add(element);
        }

        public T Peek()
        {
            this._elements.Sort();
            return this._elements[_elements.Count-1];
        }
    }
}
