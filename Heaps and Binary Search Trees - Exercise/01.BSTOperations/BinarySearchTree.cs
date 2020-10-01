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
                            current = current.LeftChild;
                        }
                        else
                        {
                            current.LeftChild = new Node<T>(element, null, null);
                            current.Count++;
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
                            current.RightChild = new Node<T>(element, null, null);
                            current.Count++;
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
            throw new NotImplementedException();
        }

        public List<T> Range(T lower, T upper)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        private void CheckIfEmpty()
        {
            if (this.Root == null)
            {
                throw new InvalidOperationException();
            }
        }
    }
}
