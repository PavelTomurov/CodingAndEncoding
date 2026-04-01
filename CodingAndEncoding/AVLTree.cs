using static System.Math;
//using System.Diagnostics;

namespace CodingAndEncoding
{
    #region №1
    /*
    public class Node
    {
        public int data;
        public Node left;
        public Node right;
        public int height;
        public int size;

        public Node(int value)
        {
            data = value;
            left = right = null;
            height = 1;
        }
    }
    

    
    public class AVLTree
    {

        private Node leftRotate(Node rootNode)
        {

            Node newRoot = rootNode.right;
            rootNode.right = rootNode.right.left;
            newRoot.left = rootNode;
            rootNode.height = setHeight(rootNode);
            rootNode.size = setSize(rootNode);
            newRoot.height = setHeight(newRoot);
            newRoot.size = setSize(newRoot);
            return newRoot;
        }

        private Node rightRotate(Node rootNode)
        {

            Node newRoot = rootNode.left;
            rootNode.left = rootNode.left.right;
            newRoot.right = rootNode;
            rootNode.height = setHeight(rootNode);
            rootNode.size = setSize(rootNode);
            newRoot.height = setHeight(newRoot);
            newRoot.size = setSize(newRoot);
            return newRoot;
        }

        private int setSize(Node rootNode)
        {

            if (rootNode == null)
            {
                return 0;
            }
            return 1 + Math.Max((rootNode.left != null ? rootNode.left.size : 0), (rootNode.right != null ? rootNode.right.size : 0));
        }

        public Node insert(Node rootNode, int data)
        {

            if (rootNode == null)
            {
                rootNode = new Node(data);
                return rootNode;
            }

            if (data < rootNode.data)
                rootNode.left = insert(rootNode.left, data);
            else if (data > rootNode.data)
                rootNode.right = insert(rootNode.right, data);


            int balanceFactor = balance(rootNode.left, rootNode.right);

            if (balanceFactor > 1)
            {

                if (height(rootNode.left.left) >= height(rootNode.left.right))
                {

                    rootNode = rightRotate(rootNode);
                }
                else
                {

                    rootNode.left = leftRotate(rootNode.left);
                    rootNode = rightRotate(rootNode);
                }
            }
            else if (balanceFactor < -1)
            {

                if (height(rootNode.right.right) >= height(rootNode.right.left))
                {

                    rootNode = leftRotate(rootNode);
                }
                else
                {

                    rootNode.right = rightRotate(rootNode.right);
                    rootNode = leftRotate(rootNode);
                }
            }
            else
            {

                rootNode.height = setHeight(rootNode);
                rootNode.size = setSize(rootNode);
            }
            return rootNode;
        }

        private int balance(Node rootNodeLeft, Node rootNodeRight)
        {
            return height(rootNodeLeft) - height(rootNodeRight);
        }

        private int setHeight(Node rootNode)
        {

            if (rootNode == null)
            {
                return 0;
            }
            return 1 + Math.Max((rootNode.left != null ? rootNode.left.height : 0), (rootNode.right != null ? rootNode.right.height : 0));
        }

        private int height(Node rootNode)
        {

            if (rootNode == null)
            {
                return 0;
            }
            else
            {
                return rootNode.height;
            }
        }

    }

    public class TreeTraversal
    {

        public Node root;

        public TreeTraversal()
        {

            root = null;
        }

        public void inOrder(Node node)
        {

            if (node != null)
            {

                inOrder(node.left);
                System.Console.Write(node.data + "  ");
                inOrder(node.right);
            }
        }
    }
    */
    #endregion

    #region №2 Этот вариант дерева AVL используется
    
    interface ISet<T>
    {
        bool Add(T val); // Add a value. Returns true if added, false if already present.
        bool Remove(T val); // Remove a value. Returns true if removed, false if not present.
    }
    
    
    class AVLTree<T> : ISet<T> where T : System.IComparable<T>
    {
        public class Node : System.IComparable<Node>
        {
            public T Value;
            public Node Left;
            public Node Right;
            public int Height;

            public Node(T val, int height)
            {
                this.Value = val;
                this.Height = height;
            }

            public int CompareTo(Node v) => this.Value.CompareTo(v.Value);

            string ToStringUtil(Node t, int height)
            {
                if (t == null) return "";
                string s = "";
                s += ToStringUtil(t.Right, height + 2);
                s += new string(' ', height) + t.Value;
                //s += t.Val + " ";
                //s += " (" + BalanceFactor(t) + ")";
                s += "\n";
                s += ToStringUtil(t.Left, height + 2);
                return s;
            }

            public override string ToString()
              => ToStringUtil(this, 0);
        }

        Node Root;
        public int Count { get; set; }

        public AVLTree() { Root = null; Count = 0; }

        static int Height(Node v) => (v == null) ? 0 : v.Height;

        static int BalanceFactor(Node v)
          => (v == null) ? 0 : (Height(v.Left) - Height(v.Right));


        void UpdateHeightsAfterRotation(Node n)
        {
            if (n == null) return;
            if (n.Left != null)
                n.Left.Height = 1 + Max(Height(n.Left.Left),
                                        Height(n.Left.Right));
            if (n.Right != null)
                n.Right.Height = 1 + Max(Height(n.Right.Left),
                                         Height(n.Right.Right));
            n.Height = 1 + Max(Height(n.Left),
                               Height(n.Right));

        }

        Node RotateRight(Node n)
        {
            //Debug.WriteLine($"~>RotateRight({n.Value})");
            Node newSubRoot = n.Left;
            n.Left = n.Left.Right;
            newSubRoot.Right = n;
            UpdateHeightsAfterRotation(newSubRoot);
            return newSubRoot;
        }

        Node RotateLeft(Node n)
        {
            //Debug.WriteLine($"~>RotateLeft({n.Value})");
            Node newSubRoot = n.Right;
            n.Right = n.Right.Left;
            newSubRoot.Left = n;
            UpdateHeightsAfterRotation(newSubRoot);
            return newSubRoot;
        }

        void Rebalance(ref Node root)
        {
            int bF = BalanceFactor(root);
            /*
            void debugMessage(string s, T val)
            {
                Debug.WriteLine($"CASE: {s} at {val}");
            }
            */
            // LeftLeft
            if (bF > 1 && BalanceFactor(root.Left) >= 0)
            {
                //debugMessage("LeftLeft", root.Value);
                //Debug.Indent();
                root = RotateRight(root);
                //Debug.Unindent();
            }
            // RightRight
            if (bF < -1 && BalanceFactor(root.Right) <= 0)
            {
                //debugMessage("RightRight", root.Value);
                //Debug.Indent();
                root = RotateLeft(root);
                //Debug.Unindent();
            }
            // LeftRight
            if (bF > 1 && BalanceFactor(root.Left) < 0)
            {
                //debugMessage("LeftRight", root.Value);
                //Debug.Indent();
                root.Left = RotateLeft(root.Left);
                root = RotateRight(root);
                //Debug.Unindent();
            }
            // RightLeft
            if (bF < -1 && BalanceFactor(root.Right) > 0)
            {
                //debugMessage("RightLeft", root.Value);
                //Debug.Indent();
                root.Right = RotateRight(root.Right);
                root = RotateLeft(root);
                //Debug.Unindent();
            }
        }

        // Recursive utility to insert val into the Binary Search Tree.
        // Once finished, ref Node currRoot will be the root of BST.
        bool AddTo(ref Node root, T val)
        {
            bool result = true;
            if (root == null)
            {
                root = new Node(val, 1);
                Count++;
                return true;
            }
            else if (val.CompareTo(root.Value) < 0)
                result = AddTo(ref root.Left, val);
            else if (val.CompareTo(root.Value) > 0)
                result = AddTo(ref root.Right, val);
            else
                return false; // duplicates not allowed
            root.Height = 1 + Max(Height(root.Left),
                                  Height(root.Right));
            Rebalance(ref root);
            return result;
        }

        public bool Add(T theVal) => AddTo(ref Root, theVal);

        // delete and return min val in tree rooted at root
        T RemoveMin(ref Node root)
        {
            if (root == null) throw new System.NullReferenceException();
            if (root.Left == null)
            {
                T min = root.Value;
                root = root.Right;
                return min;
            }
            else
                return RemoveMin(ref root.Left);
        }

        bool RemoveFrom(ref Node root, T val)
        {
            if (root == null) return false; // not found
            bool result = false; // arbitrary initialisation
            if (val.CompareTo(root.Value) < 0)
                result = RemoveFrom(ref root.Left, val); // recurse left
            else if (val.CompareTo(root.Value) > 0)
                result = RemoveFrom(ref root.Right, val); // recurse right
            else if (val.CompareTo(root.Value) == 0)
            {
                if (root.Left != null && root.Right != null)
                {
                    root.Value = RemoveMin(ref root.Right); // two children
                }
                else root = root.Left ?? root.Right; // at most one child
                result = true;
                Count--;
            }
            if (root == null) return true; // deleted node had no children
            root.Height = 1 + Max(Height(root.Left),
                                  Height(root.Right));
            Rebalance(ref root);
            return result;
        }

        public bool Remove(T val) => RemoveFrom(ref Root, val);

        Node ContainsIn(Node root, T val, out Node v)
        {
            v = root;
            if (root == null)
                return null;
            if (val.CompareTo(root.Value) == 0)
                return v;
            if (val.CompareTo(root.Value) < 0)
                return ContainsIn(root.Left, val, out v);
            if (val.CompareTo(root.Value) > 0)
                return ContainsIn(root.Right, val, out v);
            return null;
        }

        public Node Contains(T val) => ContainsIn(Root, val, out Node v);

        public void Clear()
        {
            Root = null;
            Count = 0;
        }

        public override string ToString()
          => (Root == null) ? "" : Root.ToString();

    }
    
    #endregion

    #region №3
    /*
    class AVL
    {
        class Node
        {
            public int data;
            public Node left;
            public Node right;
            public Node(int data)
            {
                this.data = data;
            }
        }
        Node root;
        public AVL()
        {
        }
        public void Add(int data)
        {
            Node newItem = new Node(data);
            if (root == null)
            {
                root = newItem;
            }
            else
            {
                root = RecursiveInsert(root, newItem);
            }
        }
        private Node RecursiveInsert(Node current, Node n)
        {
            if (current == null)
            {
                current = n;
                return current;
            }
            else if (n.data < current.data)
            {
                current.left = RecursiveInsert(current.left, n);
                current = balance_tree(current);
            }
            else if (n.data > current.data)
            {
                current.right = RecursiveInsert(current.right, n);
                current = balance_tree(current);
            }
            return current;
        }
        private Node balance_tree(Node current)
        {
            int b_factor = balance_factor(current);
            if (b_factor > 1)
            {
                if (balance_factor(current.left) > 0)
                {
                    current = RotateLL(current);
                }
                else
                {
                    current = RotateLR(current);
                }
            }
            else if (b_factor < -1)
            {
                if (balance_factor(current.right) > 0)
                {
                    current = RotateRL(current);
                }
                else
                {
                    current = RotateRR(current);
                }
            }
            return current;
        }
        public void Delete(int target)
        {//and here
            root = Delete(root, target);
        }
        private Node Delete(Node current, int target)
        {
            Node parent;
            if (current == null)
            { return null; }
            else
            {
                //left subtree
                if (target < current.data)
                {
                    current.left = Delete(current.left, target);
                    if (balance_factor(current) == -2)//here
                    {
                        if (balance_factor(current.right) <= 0)
                        {
                            current = RotateRR(current);
                        }
                        else
                        {
                            current = RotateRL(current);
                        }
                    }
                }
                //right subtree
                else if (target > current.data)
                {
                    current.right = Delete(current.right, target);
                    if (balance_factor(current) == 2)
                    {
                        if (balance_factor(current.left) >= 0)
                        {
                            current = RotateLL(current);
                        }
                        else
                        {
                            current = RotateLR(current);
                        }
                    }
                }
                //if target is found
                else
                {
                    if (current.right != null)
                    {
                        //delete its inorder successor
                        parent = current.right;
                        while (parent.left != null)
                        {
                            parent = parent.left;
                        }
                        current.data = parent.data;
                        current.right = Delete(current.right, parent.data);
                        if (balance_factor(current) == 2)//rebalancing
                        {
                            if (balance_factor(current.left) >= 0)
                            {
                                current = RotateLL(current);
                            }
                            else { current = RotateLR(current); }
                        }
                    }
                    else
                    {   //if current.left != null
                        return current.left;
                    }
                }
            }
            return current;
        }
        public void Find(int key)
        {
            if (Find(key, root).data == key)
            {
                Console.WriteLine("{0} was found!", key);
            }
            else
            {
                Console.WriteLine("Nothing found!");
            }
        }
        private Node Find(int target, Node current)
        {

            if (target < current.data)
            {
                if (target == current.data)
                {
                    return current;
                }
                else
                    return Find(target, current.left);
            }
            else
            {
                if (target == current.data)
                {
                    return current;
                }
                else
                    return Find(target, current.right);
            }

        }
        public void DisplayTree()
        {
            if (root == null)
            {
                Console.WriteLine("Tree is empty");
                return;
            }
            InOrderDisplayTree(root);
            Console.WriteLine();
        }
        private void InOrderDisplayTree(Node current)
        {
            if (current != null)
            {
                InOrderDisplayTree(current.left);
                Console.Write("({0}) ", current.data);
                InOrderDisplayTree(current.right);
            }
        }
        private int max(int l, int r)
        {
            return l > r ? l : r;
        }
        private int getHeight(Node current)
        {
            int height = 0;
            if (current != null)
            {
                int l = getHeight(current.left);
                int r = getHeight(current.right);
                int m = max(l, r);
                height = m + 1;
            }
            return height;
        }
        private int balance_factor(Node current)
        {
            int l = getHeight(current.left);
            int r = getHeight(current.right);
            int b_factor = l - r;
            return b_factor;
        }
        private Node RotateRR(Node parent)
        {
            Node pivot = parent.right;
            parent.right = pivot.left;
            pivot.left = parent;
            return pivot;
        }
        private Node RotateLL(Node parent)
        {
            Node pivot = parent.left;
            parent.left = pivot.right;
            pivot.right = parent;
            return pivot;
        }
        private Node RotateLR(Node parent)
        {
            Node pivot = parent.left;
            parent.left = RotateRR(pivot);
            return RotateLL(parent);
        }
        private Node RotateRL(Node parent)
        {
            Node pivot = parent.right;
            parent.right = RotateLL(pivot);
            return RotateRR(parent);
        }
    }
    */
    #endregion
}
