namespace Problem03.ReversedList
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class ReversedList<T> : IAbstractList<T>
    {
        private const int DefaultCapacity = 4;

        private T[] _items;

        public ReversedList()
            : this(DefaultCapacity) { this._items = new T[DefaultCapacity]; }

        public ReversedList(int capacity)
        {
            if (capacity < 0)
                throw new ArgumentOutOfRangeException(nameof(capacity));

            this._items = new T[capacity];
        }

        public T this[int index]
        {
            get
            {
                this.ValidateIndex(index);
                return this._items[this.Count - 1 - index];
            }
            set
            {
                this.ValidateIndex(index);
                this._items[index] = value;
            }
        }

        public int Count { get; private set; }

        public void Add(T item)
        {
            this.ValidateLength();
            this._items[this.Count] = item;
            this.Count++;
        }

        public bool Contains(T item)
        {
            return this.IndexOf(item) != -1;
        }

        public int IndexOf(T item)
        {
            for (int i = this.Count - 1; i >= 0; i--)
            {
                if (this._items[i].Equals(item))
                {
                    return this.Count - i - 1;
                }
            }

            return -1;
        }

        public void Insert(int index, T item)
        {
            this.ValidateLength();
            this.ValidateIndex(index);
            for (int i = this.Count; i > index; i--)
            {
                this._items[i] = this._items[i - 1];
            }

            this._items[index] = item;
            this.Count++;
        }

        public bool Remove(T item)
        {
            int indexToRemove = this.IndexOf(item);
            if (indexToRemove == -1)
            {
                return false;
            }
            else
            {
                this.RemoveAt(indexToRemove);
                return true;
            }
        }

        public void RemoveAt(int index)
        {

            this.ValidateIndex(index);
            int indexToRemove = this.Count - index - 1;
            for (int i = indexToRemove; i < this.Count; i++)
            {
                this._items[i] = this._items[i + 1];
            }

            this._items[this.Count - 1] = default;
            this.Count--;
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (int i = this.Count - 1; i >= 0; i--)
            {
                yield return this._items[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        private void ValidateLength()
        {
            if (this.Count == this._items.Length)
            {
                this.Grow();
            }
        }

        private void Grow()
        {
            var newArr = new T[this.Count * 2];
            Array.Copy(this._items, newArr, this.Count);
            this._items = newArr;
        }

        private void ValidateIndex(int index)
        {
            if (index < 0 || index >= this.Count)
            {
                throw new IndexOutOfRangeException();
            }
        }

    }
}