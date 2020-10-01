namespace _04.BinarySearchTree
{
    using System;

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

        public T Value => this.Value;

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
    }
}
