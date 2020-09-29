namespace Tree
{
    using System;
    using System.Collections.Generic;

    public class Tree<T> : IAbstractTree<T>
    {
        private readonly List<Tree<T>> _children;

        private bool isRootDeleted;

        public Tree(T value)
        {
            this.Value = value;

            this.Parent = null;

            this._children = new List<Tree<T>>();
        }

        public Tree(T value, params Tree<T>[] children)
            : this(value)
        {
            foreach (Tree<T> child in children)
            {
                child.Parent = this;
                this._children.Add(child);
            }
        }

        public T Value { get; private set; }
        public Tree<T> Parent { get; private set; }
        public IReadOnlyCollection<Tree<T>> Children => this._children.AsReadOnly();

        public ICollection<T> OrderBfs()
        {
            var result = new List<T>();

            if (isRootDeleted)
            {
                return result;
            }

            var queue = new Queue<Tree<T>>();

            queue.Enqueue(this);

            while (queue.Count != 0)
            {
                var subTree = queue.Dequeue();
                result.Add(subTree.Value);
                foreach (var child in subTree.Children)
                {
                    queue.Enqueue(child);
                }
            }

            return result;
        }

        public ICollection<T> OrderDfs()
        {
            var result = new List<T>();
            if (isRootDeleted)
            {
                return result;
            }


            this.DFS(this, result);

            return result;

        }

        public void AddChild(T parentKey, Tree<T> child)
        {
            var serchedNode = this.FindBfs(parentKey);
            child.Parent = serchedNode;
            serchedNode._children.Add(child);
        }

        public void RemoveNode(T nodeKey)
        {
            var searchedNode = this.FindBfs(nodeKey);
            foreach (var child in searchedNode.Children)
            {
                child.Parent = null;
            }
            searchedNode._children.Clear();

            if (searchedNode.Parent == null)
            {
                this.isRootDeleted = true;
            }
            else
            {
                var parent = searchedNode.Parent;
                parent._children.Remove(searchedNode);
            }

            searchedNode.Value = default;
        }

        public void Swap(T firstKey, T secondKey)
        {
            var searched1 = this.FindBfs(firstKey);
            var searched2 = this.FindBfs(secondKey);
            this.CheckEmpty(searched1);
            this.CheckEmpty(searched2);
            var parent1 = searched1.Parent;
            var parent2 = searched2.Parent;
            if (parent1 == null)
            {
                this.SwapRoot(searched2);
                return;
            }
            
            if (parent2 == null)
            {
                this.SwapRoot(searched1);
                return;
            }

            var indexFirst = parent1._children.IndexOf(searched1);
            var indexSecond = parent2._children.IndexOf(searched2);
            parent1._children[indexFirst] = searched2;
            parent2._children[indexSecond] = searched1;
        }

        private void DFS(Tree<T> subTree, List<T> result)
        {
            foreach (var child in subTree.Children)
            {
                this.DFS(child, result);
            }
            result.Add(subTree.Value);
        }

        private Tree<T> FindBfs(T searchedkey)
        {
            var queue = new Queue<Tree<T>>();
            queue.Enqueue(this);

            while (queue.Count != 0)
            {

                var subTree = queue.Dequeue();
                if (subTree.Value.Equals(searchedkey))
                {
                    return subTree;
                }

                foreach (var child in subTree.Children)
                {
                    queue.Enqueue(child);
                }
            }

            throw new ArgumentNullException();

        }

        private void SwapRoot(Tree<T> node)
        {
            this.Value = node.Value;
            this._children.Clear();
            foreach (var child in node.Children)
            {
                this._children.Add(child);
            }
        }

        private void CheckEmpty(Tree<T> node)
        {
            if (node == null)
            {
                throw new ArgumentNullException();
            }
        }
    }
}
