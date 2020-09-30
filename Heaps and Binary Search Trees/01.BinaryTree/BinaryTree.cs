using System.Text;

namespace _01.BinaryTree
{
    using System;
    using System.Collections.Generic;

    public class BinaryTree<T> : IAbstractBinaryTree<T>
    {
        public BinaryTree(T value
            , IAbstractBinaryTree<T> leftChild
            , IAbstractBinaryTree<T> rightChild)
        {
            this.Value = value;
            this.LeftChild = leftChild;
            this.RightChild = rightChild;
        }

        public T Value { get; private set; }

        public IAbstractBinaryTree<T> LeftChild { get; private set; }

        public IAbstractBinaryTree<T> RightChild { get; private set; }

        public string AsIndentedPreOrder(int indent)
        {
            var sb = new StringBuilder();
            int currentIndent = 0;
            this.AsIndentedPreOrderDfs(this, sb, indent);


            return sb.ToString();
        }

        public List<IAbstractBinaryTree<T>> InOrder()
        {
            var list = new List<IAbstractBinaryTree<T>>();

            this.InOrderDfs(this, list);

            return list;
        }

        public List<IAbstractBinaryTree<T>> PostOrder()
        {
            var list = new List<IAbstractBinaryTree<T>>();

            if (this.LeftChild != null)
            {
                list.AddRange(this.LeftChild.PostOrder());
            }

            if (this.RightChild != null)
            {
                list.AddRange(this.RightChild.PostOrder());
            }

            list.Add(this);

            return list;
        }

        public List<IAbstractBinaryTree<T>> PreOrder()
        {
            var list = new List<IAbstractBinaryTree<T>>();

            list.Add(this);

            if (this.LeftChild != null)
            {
                list.AddRange(this.LeftChild.PreOrder());
            }

            if (this.RightChild != null)
            {
                list.AddRange(this.RightChild.PreOrder());
            }

            return list;
        }

        public void ForEachInOrder(Action<T> action)
        {
            this.InOrderDfsAction(this, action);
        }

        private void AsIndentedPreOrderDfs(IAbstractBinaryTree<T> binaryTree, StringBuilder sb, int indent)
        {
            sb.AppendLine($"{new string(' ', indent)}{binaryTree.Value}");
            if (binaryTree.LeftChild != null)
            {
                this.AsIndentedPreOrderDfs(binaryTree.LeftChild, sb, indent + 2);
            }

            if (binaryTree.RightChild != null)
            {
                this.AsIndentedPreOrderDfs(binaryTree.RightChild, sb, indent + 2);
            }
        }

        private void InOrderDfsAction(IAbstractBinaryTree<T>binaryTree, Action<T> action)
        {
            if (binaryTree.LeftChild != null)
            {
                this.InOrderDfsAction(binaryTree.LeftChild,action);
            }

            action.Invoke(binaryTree.Value);

            if (binaryTree.RightChild != null)
            {
                this.InOrderDfsAction(binaryTree.RightChild,action);
            }

        }

        private void InOrderDfs(IAbstractBinaryTree<T> binaryTree, List<IAbstractBinaryTree<T>> list)
        {
            if (binaryTree.LeftChild != null)
            {
                this.InOrderDfs(binaryTree.LeftChild, list);
            }

            list.Add(binaryTree);

            if (binaryTree.RightChild != null)
            {
                this.InOrderDfs(binaryTree.RightChild, list);
            }

        }

        }
}
