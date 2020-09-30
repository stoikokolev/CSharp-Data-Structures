using System.Collections.Generic;
using System.Linq;

namespace _02.MaxHeap
{
    using System;

    public class MaxHeap<T> : IAbstractHeap<T>
        where T : IComparable<T>
    {
        private List<T> _elements;

        public MaxHeap()
        {
            this._elements=new List<T>();
        }

        public int Size => this._elements.Count;

        public void Add(T element)
        {
            this._elements.Add(element);

            this.HeapifyUp();
        }

        private void HeapifyUp()
        {
            int indexOfElement = this._elements.Count - 1;
            int parentIndex = this.GetParentIndex(indexOfElement);

            while (this.IndexIsValid(indexOfElement)
                   && this.IsGreater(indexOfElement,parentIndex))
            {
                this.Swap(indexOfElement, parentIndex);

                indexOfElement = parentIndex;
                parentIndex = this.GetParentIndex(indexOfElement);
            }
            
        }

        public T Peek()
        {
            return this._elements[0];
        }

        private void Swap(int indexOfElement, int parentIndex)
        {
            var temp = this._elements[indexOfElement];
            this._elements[indexOfElement] = this._elements[parentIndex];
            this._elements[parentIndex] = temp;
        }

        private bool IndexIsValid(int index)
        {
            return index > 0;
        }

        private int GetParentIndex(int childIndex)
        {
            return (childIndex - 1) / 2;
        }

        private bool IsGreater(int childIndex, int parenIndex)
        {
            return this._elements[childIndex].CompareTo(this._elements[parenIndex]) > 0;
        }
    }
}
