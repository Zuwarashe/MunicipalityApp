using System;

namespace Municipality_App
{
    
    public class TreeNode<T> where T : IComparable<T>
    {
        public T Data { get; set; }
        public TreeNode<T> Left { get; set; }
        public TreeNode<T> Right { get; set; }

        public TreeNode(T data)
        {
            Data = data;
            Left = null;
            Right = null;
        }
    }

    // Generic Binary Search Tree class
    public class BinarySearchTree<T> where T : IComparable<T>   
    {
        public TreeNode<T> Root { get; private set; }

        // Insert a new item into the BST
        public void Insert(T data)
        {
            Root = InsertRec(Root, data);
        }

        private TreeNode<T> InsertRec(TreeNode<T> node, T data)
        {
            if (node == null)
                return new TreeNode<T>(data);

            if (data.CompareTo(node.Data) < 0)
                node.Left = InsertRec(node.Left, data);
            else if (data.CompareTo(node.Data) > 0)
                node.Right = InsertRec(node.Right, data);

            return node;
        }

        // Search for an item in the BST
        public T Search(T data)
        {
            return SearchRec(Root, data);
        }

        private T SearchRec(TreeNode<T> node, T data)
        {
            if (node == null)
                return default;

            if (data.CompareTo(node.Data) == 0)
                return node.Data;

            if (data.CompareTo(node.Data) < 0)
                return SearchRec(node.Left, data);

            return SearchRec(node.Right, data);
        }

        // In-order traversal of the BST
        public void InOrderTraversal(Action<T> action)
        {
            InOrderTraversalRec(Root, action);
        }

        private void InOrderTraversalRec(TreeNode<T> node, Action<T> action)
        {
            if (node == null)
                return;

            InOrderTraversalRec(node.Left, action);
            action(node.Data);
            InOrderTraversalRec(node.Right, action);
        }

        // Remove a node from the BST
        public bool Remove(T data)
        {
            bool isRemoved = false;
            Root = RemoveRec(Root, data, ref isRemoved);
            return isRemoved;
        }

        private TreeNode<T> RemoveRec(TreeNode<T> node, T data, ref bool isRemoved)
        {
            if (node == null)
                return null;

            // Case 1: The data is smaller than the current node's data, so we search the left subtree
            if (data.CompareTo(node.Data) < 0)
            {
                node.Left = RemoveRec(node.Left, data, ref isRemoved);
            }
            // Case 2: The data is greater than the current node's data, so we search the right subtree
            else if (data.CompareTo(node.Data) > 0)
            {
                node.Right = RemoveRec(node.Right, data, ref isRemoved);
            }
            // Case 3: Found the node to be removed
            else
            {
                isRemoved = true;

                // Case 3a: Node has no children (leaf node)
                if (node.Left == null && node.Right == null)
                {
                    return null;
                }
                // Case 3b: Node has one child
                else if (node.Left == null)
                {
                    return node.Right;
                }
                else if (node.Right == null)
                {
                    return node.Left;
                }
                // Case 3c: Node has two children
                else
                {
                    // Find the in-order successor (smallest node in the right subtree)
                    TreeNode<T> successor = GetMinNode(node.Right);
                    node.Data = successor.Data; // Replace the data of the current node with the successor's data
                    node.Right = RemoveRec(node.Right, successor.Data, ref isRemoved); // Remove the successor node
                }
            }

            return node;
        }

        // Helper method to find the minimum node in a subtree (in-order successor)
        private TreeNode<T> GetMinNode(TreeNode<T> node)
        {
            while (node.Left != null)
            {
                node = node.Left;
            }
            return node;
        }
    }
}
