using System.Linq;
using System.Text;

namespace Tree
{
    using System;
    using System.Collections.Generic;

    public class Tree<T> : IAbstractTree<T>
    {
        private readonly List<Tree<T>> _children;

        public Tree(T key, params Tree<T>[] children)
        {
            this.Key = key;
            this._children=new List<Tree<T>>();

            foreach (var child in children)
            {
                this.AddChild(child);
                child.Parent = this;
            }
        }

        public T Key { get; private set; }

        public Tree<T> Parent { get; private set; }


        public IReadOnlyCollection<Tree<T>> Children
            => this._children.AsReadOnly();

        public void AddChild(Tree<T> child)
        {
            this._children.Add(child);
        }

        public void AddParent(Tree<T> parent)
        {
            this.Parent = parent;
        }

        public string GetAsString()
        {
            var sb = new StringBuilder();
            int indent = 0;
            var root = this;

            this.DFStoString(root,sb,indent);

            return sb.ToString().Trim();
        }

       

        public Tree<T> GetDeepestLeftomostNode()
        {
            var leafs = this.OrderBFS(this).Where(x => this.IsLeaf(x)).ToList();

            int maxDepth = int.MinValue;
            Tree<T> deepestNode = null;

            foreach (var leaf in leafs)
            {
                int currentDepth = 0;
                var current = leaf;
                while (current.Parent!=null)
                {
                    currentDepth++;
                    current = current.Parent;
                }

                if (currentDepth > maxDepth)
                {
                    maxDepth = currentDepth;
                    deepestNode = leaf;
                }
            }

            return deepestNode;
        }


        private List<Tree<T>> OrderBFS(Tree<T> current)
        {
            var result = new List<Tree<T>>();
            var queue = new Queue<Tree<T>>();
            queue.Enqueue(current);

            while (queue.Count > 0)
            {
                var subTree = queue.Dequeue();
                result.Add(subTree);

                foreach (var child in subTree.Children)
                {
                    queue.Enqueue(child);
                }
            }

            return result;
        }

        public List<T> GetLeafKeys()
        {
            var result = new List<T>();
            var queue = new Queue<Tree<T>>();
            queue.Enqueue(this);

            while (queue.Count > 0)
            {
                var current = queue.Dequeue();
                if (this.IsLeaf(current))
                {
                    result.Add(current.Key);
                }

                foreach (var child in current.Children)
                {
                    queue.Enqueue(child);
                }
            }

            result.Sort();
            return result;
        }
        
        public List<T> GetMiddleKeys()
        {
            var result = new List<T>();
            var queue = new Queue<Tree<T>>();
            queue.Enqueue(this);

            while (queue.Count > 0)
            {
                var current = queue.Dequeue();
                if (!this.IsLeaf(current) && !this.IsRoot(current))
                {
                    result.Add(current.Key);
                }

                foreach (var child in current.Children)
                {
                    queue.Enqueue(child);
                }
            }

            result.Sort();
            return result;
        }

        public List<T> GetLongestPath()
        {
            var stack = new Stack<T>();
            var current = this.GetDeepestLeftomostNode();
            while (current != null)
            {
                stack.Push(current.Key);
                current = current.Parent;
            }

            return stack.ToList();
        }

        public List<List<T>> PathsWithGivenSum(int sum)
        {
            var leafs = this.OrderBFS(this).Where(x => this.IsLeaf(x)).ToList();
            var result = new List<List<T>>();

            foreach (var leaf in leafs)
            {
                var current = leaf;
                var stack = new Stack<T>();
                int currentSum = 0;
                while (current!=null)
                {
                    stack.Push(current.Key);
                    currentSum += Convert.ToInt32(current.Key);
                    current = current.Parent;
                }

                if (currentSum == sum)
                {
                    var list = stack.ToList();
                    result.Add(list);
                }
            }

            return result;
        }

        public List<Tree<T>> SubTreesWithGivenSum(int sum)
        {
            var result = new List<Tree<T>>();
            var trees = OrderBFS(this).ToList();

            foreach (var tree in trees)
            {
                int currentSum = 0;
                currentSum += Convert.ToInt32(tree.Key);
                foreach (var child in tree.Children)
                {
                    currentSum += Convert.ToInt32(child.Key);
                }

                if (currentSum == sum)
                {
                    result.Add(tree);
                    
                }
            }

            return result;

        }

        private void DFStoString(Tree<T> current, StringBuilder sb, int indent)
        {
            sb.AppendLine($"{new string(' ', indent)}{current.Key}");
            foreach (var child in current._children)
            {
                this.DFStoString(child, sb, indent + 2);
            }
        }

        private bool IsLeaf(Tree<T> current)
        {
            return current.Children.Count == 0;
        }

        private bool IsRoot(Tree<T> current)
        {
            return current.Parent == null;
        }
    }
}
