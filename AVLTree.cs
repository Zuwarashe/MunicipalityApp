using System;

namespace Municipality_App
{
    public class AVLTree<T> where T : IComparable<T>
    {
        // Node class to represent each element in the AVL tree
        private class Node
        {
            public T Data; // The data stored in the node
            public Node Left, Right; // Left and right child nodes
            public int Height; // Height of the node (used for balancing)

            public Node(T data)
            {
                Data = data;
                Height = 1; // Initially, a new node has a height of 1
            }
        }

        private Node root; // Root node of the AVL tree

        // Method to insert a new element into the AVL tree
        public void Insert(T data)
        {
            root = Insert(root, data); // Call the recursive Insert method
        }

        // Recursive method to insert a new element in the tree
        private Node Insert(Node node, T data)
        {
            if (node == null)
                return new Node(data); // If the node is null, create a new node

            // Compare the data to determine where to insert it
            if (data.CompareTo(node.Data) < 0)
                node.Left = Insert(node.Left, data); // Insert in the left subtree
            else
                node.Right = Insert(node.Right, data); // Insert in the right subtree

            // Update the height of the current node
            node.Height = 1 + Math.Max(GetHeight(node.Left), GetHeight(node.Right));

            // Balance the node to maintain the AVL tree properties
            return Balance(node);
        }

        // Method to search for a specific element in the AVL tree using its unique ID
        public T Search(string uniqueId)
        {
            return Search(root, uniqueId); // Start searching from the root node
        }

        // Recursive method to search for an element based on a partial match of its string representation
        private T Search(Node node, string uniqueId)
        {
            if (node == null)
                return default; // Return default if the node is null

            // Check if the node's data matches the search query (partial match)
            if (node.Data.ToString().Contains(uniqueId))
                return node.Data;

            // Recurse to the left or right subtree depending on the search term
            if (string.Compare(uniqueId, node.Data.ToString()) < 0)
                return Search(node.Left, uniqueId); // Search in the left subtree

            return Search(node.Right, uniqueId); // Search in the right subtree
        }

        // Method to traverse the tree in in-order and perform an action on each element
        public void InOrderTraversal(Action<T> action)
        {
            InOrderTraversal(root, action); // Start in-order traversal from the root
        }

        // Recursive in-order traversal method
        private void InOrderTraversal(Node node, Action<T> action)
        {
            if (node == null)
                return; // Base case: if the node is null, stop the traversal

            // Traverse the left subtree
            InOrderTraversal(node.Left, action);

            // Perform the action on the current node's data
            action(node.Data);

            // Traverse the right subtree
            InOrderTraversal(node.Right, action);
        }

        // Method to get the height of a node (returns 0 if node is null)
        private int GetHeight(Node node) => node?.Height ?? 0;

        // Method to balance the tree by checking if any node is unbalanced
        private Node Balance(Node node)
        {
            // Calculate the balance factor (difference between left and right subtree heights)
            int balance = GetHeight(node.Left) - GetHeight(node.Right);

            // Left-heavy case: need to rotate right
            if (balance > 1)
            {
                if (GetHeight(node.Left.Left) < GetHeight(node.Left.Right))
                    node.Left = RotateLeft(node.Left); // Left-Right case: rotate left on left child

                return RotateRight(node); // Rotate right on the unbalanced node
            }

            // Right-heavy case: need to rotate left
            if (balance < -1)
            {
                if (GetHeight(node.Right.Right) < GetHeight(node.Right.Left))
                    node.Right = RotateRight(node.Right); // Right-Left case: rotate right on right child

                return RotateLeft(node); // Rotate left on the unbalanced node
            }

            // If the node is balanced, return it unchanged
            return node;
        }

        // Left rotation method (used to balance the tree)
        private Node RotateLeft(Node x)
        {
            Node y = x.Right; // Set y as the right child of x
            x.Right = y.Left; // Set x's right child to y's left child
            y.Left = x; // Set y's left child to x

            // Update heights of x and y
            x.Height = Math.Max(GetHeight(x.Left), GetHeight(x.Right)) + 1;
            y.Height = Math.Max(GetHeight(y.Left), GetHeight(y.Right)) + 1;

            return y; // Return y as the new root of the subtree
        }

        // Right rotation method (used to balance the tree)
        private Node RotateRight(Node y)
        {
            Node x = y.Left; // Set x as the left child of y
            y.Left = x.Right; // Set y's left child to x's right child
            x.Right = y; // Set x's right child to y

            // Update heights of y and x
            y.Height = Math.Max(GetHeight(y.Left), GetHeight(y.Right)) + 1;
            x.Height = Math.Max(GetHeight(x.Left), GetHeight(x.Right)) + 1;

            return x; // Return x as the new root of the subtree
        }
    }
}
