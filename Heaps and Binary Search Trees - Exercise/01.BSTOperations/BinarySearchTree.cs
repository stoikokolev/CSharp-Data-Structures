namespace _01.BSTOperations
{
    using System;
    using System.Collections.Generic;

    public class BinarySearchTree<T> : IAbstractBinarySearchTree<T>
        where T : IComparable<T>
    {
        public BinarySearchTree()
        {
            this.Root = null;
            this.LeftChild = null;
            this.RightChild = null;

        }

        public BinarySearchTree(Node<T> root)
        {
            this.Root = root;
            this.RightChild = root.RightChild;
            this.LeftChild = root.LeftChild;
        }

        public Node<T> Root { get; private set; }

        public Node<T> LeftChild { get; private set; }

        public Node<T> RightChild { get; private set; }

        public T Value => this.Root.Value;

        public int Count => this.Root.Count;

        public bool Contains(T element)
        {
            var current = this.Root;
            while (true)
            {
                if (element.CompareTo(current.Value) < 0)
                {
                    if (current.LeftChild != null)
                    {
                        current = current.LeftChild;
                    }
                    else
                    {
                        return false;
                    }
                }
                else if (element.CompareTo(current.Value) > 0)
                {
                    if (current.RightChild != null)
                    {
                        current = current.RightChild;
                    }
                    else
                    {
                        return false;
                    }
                }
                else if (element.CompareTo(current.Value) == 0)
                {
                    return true;
                }
            }
        }

        public void Insert(T element)
        {
            if (this.Root == null)
            {
                this.Root = new Node<T>(element, null, null);


            }
            else
            {
                var current = this.Root;
                while (true)
                {
                    if (element.CompareTo(current.Value) < 0)
                    {
                        if (current.LeftChild != null)
                        {
                            current.Count++;
                            current = current.LeftChild;
                        }
                        else
                        {
                            current.LeftChild = new Node<T>(element, null, null);
                        }
                    }
                    else if (element.CompareTo(current.Value) > 0)
                    {
                        if (current.RightChild != null)
                        {
                            current.Count++;
                            current = current.RightChild;
                        }
                        else
                        {
                            current.RightChild = new Node<T>(element, null, null);
                        }
                    }
                    else if (element.CompareTo(current.Value) == 0)
                    {
                        return;
                    }
                }

            }
        }

        public IAbstractBinarySearchTree<T> Search(T element)
        {
            var current = this.Root;
            while (true)
            {
                if (element.CompareTo(current.Value) < 0)
                {
                    if (current.LeftChild != null)
                    {
                        current = current.LeftChild;
                    }
                    else
                    {
                        return null;
                    }
                }
                else if (element.CompareTo(current.Value) > 0)
                {
                    if (current.RightChild != null)
                    {
                        current = current.RightChild;
                    }
                    else
                    {
                        return null;
                    }
                }
                else if (element.CompareTo(current.Value) == 0)
                {
                    return new BinarySearchTree<T>(current);
                }

            }
        }

        public void EachInOrder(Action<T> action)
        {
            Node<T> current = this.Root;
            this.EachInOrderDfs(current, action);
        }

        private void EachInOrderDfs(Node<T> current, Action<T> action)
        {
            if (current.LeftChild != null)
            {
                this.EachInOrderDfs(current.LeftChild, action);
            }
            action.Invoke(current.Value);
            if (current.RightChild != null)
            {
                this.EachInOrderDfs(current.RightChild, action);
            }
        }

        public List<T> Range(T lower, T upper)
        {
            var result = new List<T>();
            var queue = new Queue<Node<T>>();
            queue.Enqueue(this.Root);

            while (queue.Count > 0)
            {
                var current = queue.Dequeue();
                if (!this.IsLess(current.Value, lower) && !this.IsGreater(current.Value, upper))
                {
                    result.Add(current.Value);
                }

                if (current.LeftChild != null)
                {
                    queue.Enqueue(current.LeftChild);
                }

                if (current.RightChild != null)
                {
                    queue.Enqueue(current.RightChild);
                }
            }

            return result;
        }
        
        public void DeleteMin()
        {
            this.CheckIfEmpty();
            var current = this.Root;
            Node<T> previous = null;
            if (current.LeftChild == null && current.RightChild != null)
            {
                this.Root = current.RightChild;
                current = null;
                return;
            }

            while (current.LeftChild != null)
            {
                current.Count--;
                previous = current;
                current = current.LeftChild;
            }

            if (current.RightChild != null)
            {
                var toInsert = current.RightChild;
                previous.LeftChild = null;
                current = null;
                this.Insert(toInsert.Value);
            }
            else
            {
                previous.LeftChild = null;
                current = null;
            }


        }

        public void DeleteMax()
        {
            this.CheckIfEmpty();
            var current = this.Root;
            Node<T> previous = null;
            if (current.RightChild == null && current.LeftChild != null)
            {
                this.Root = current.LeftChild;
                current = null;
                return;
            }

            while (current.RightChild != null)
            {
                current.Count--;
                previous = current;
                current = current.RightChild;
            }

            if (current.LeftChild != null)
            {
                var toInsert = current.LeftChild;
                previous.RightChild = null;
                current = null;
                this.Insert(toInsert.Value);
            }
            else
            {
                previous.RightChild = null;
                current = null;
            }
        }

        public int GetRank(T element)
        {
            return this.GetRankDfs(this.Root, element);
        }

        private int GetRankDfs(Node<T> current, T element)
        {
            if (current == null)
            {
                return 0;
            }

            if (this.IsLess(element, current.Value))
            {
                return this.GetRankDfs(current.LeftChild, element);
            }
            else if (this.AreEqual(element, current.Value))
            {
                return this.GetNodeCount(current);
            }

            return this.GetNodeCount(current.LeftChild) + 1 + this.GetRankDfs(current.RightChild, element);
        }

        private int GetNodeCount(Node<T> curent)
        {
            return curent == null ? 0 : curent.Count;
        }

        private void CheckIfEmpty()
        {
            if (this.Root == null)
            {
                throw new InvalidOperationException();
            }
        }

        private bool IsLess(T first, T second)
        {
            return first.CompareTo(second) < 0;
        }

        private bool AreEqual(T first, T second)
        {
            return first.CompareTo(second) == 0;
        }

        private bool IsGreater(T first, T second)
        {
            return first.CompareTo(second) > 0;
        }


    }
}
