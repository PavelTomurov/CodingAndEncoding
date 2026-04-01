using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingAndEncoding
{
    #region Двоичное дерево и методы работы с ним
    /// <summary>
    /// Двоичное дерево
    /// </summary>
    public class BinaryTree<T>
    {
        /// <summary>
        /// Узел дерева
        /// </summary>
        public TreeNode<T> Root { get; private set; }

        /// <summary>
        /// Конструктор дерева
        /// </summary>
        public BinaryTree(T rootValue)
        {
            Root = new TreeNode<T>(rootValue);
        }

        #region Методы работы с деревом

        #region Добавление узла в дерево

        #region Вставка узла в дерево
        /// <summary>
        /// Получение значения, формирование узла и вставка узла в дерево
        /// </summary>
        public void Insert(T value)
        {
            Root = InsertRec(Root, value);
        }
        #endregion

        #region Получение значения, формирование узла и вставка узла в дерево
        /// <summary>
        /// Вставка узла в дерево
        /// Есть узел, у него 2 дочерних узла: левый и правый
        /// Если входящее значение меньше значения узла, то входящее значение вставится в левый узел
        /// Иначе - в правый
        /// </summary>
        private TreeNode<T> InsertRec(TreeNode<T> node, T value)
        {
            if (node == null)
            {
                node = new TreeNode<T>(value);
                return node;
            }

            int comparison = Comparer<T>.Default.Compare(value, node.Value);
            //int comparison = this.Compare(value, node.Value);
            if (comparison < 0)
            {
                node.Left = InsertRec(node.Left, value);
            }
            else if (comparison > 0)
            {
                node.Right = InsertRec(node.Right, value);
            }
            return node;
        }

        #endregion

        #endregion

        #region Методы поиска и удаления узла (для вставки в дерево) 

        #region Поиск в дереве
        /// <summary>
        /// Поиск по значению
        /// </summary>
        public TreeNode<T> Contains(T value)
        {
            return ContainsRec(Root, value);
        }

        /// <summary>
        /// Узел - тот в котором ищу
        /// Значение - то, что ищу
        /// Если есть, то получаю 0, true
        /// Если нет, то ищу в левом и правом узлах
        /// Происходит повтор этой функции уже на левом и правом узлах
        /// </summary>
        private TreeNode<T> ContainsRec(TreeNode<T> node, T value)
        {
            if (node == null) return null;

            int comparison = Comparer<T>.Default.Compare(value, node.Value);
            if (comparison == 0) { node.SearchDataCount++; return node; }

            return comparison < 0 ? ContainsRec(node.Left, value) : ContainsRec(node.Right, value);
        }
        #endregion

        #region Функция удаления узла
        /// <summary>
        /// Функция удаления узла
        /// </summary>
        public void Remove(T value)
        {
            Root = RemoveRec(Root, value);
        }
        #endregion

        #region Функция удаления узла подробно
        /// <summary>
        /// Функция удаления узла подробно
        /// </summary>
        private TreeNode<T> RemoveRec(TreeNode<T> node, T value)
        {
            if (node == null) return node;

            int comparison = Comparer<T>.Default.Compare(value, node.Value);
            if (comparison < 0)
            {
                node.Left = RemoveRec(node.Left, value);
            }
            else if (comparison > 0)
            {
                node.Right = RemoveRec(node.Right, value);
            }
            else
            {
                // Узел с одним потомком или без потомков
                if (node.Left == null) return node.Right;
                else if (node.Right == null) return node.Left;

                // Узел с двумя потомками: получение наименьшего значения в правом поддереве
                node.Value = MinValue(node.Right);

                // Удаление наименьшего узла в правом поддереве
                node.Right = RemoveRec(node.Right, node.Value);
            }
            return node;
        }
        #endregion

        #region Узел с двумя потомками: получение наименьшего значения
        /// <summary>
        /// Узел с двумя потомками: получение наименьшего значения
        /// </summary>
        private T MinValue(TreeNode<T> node)
        {
            T minValue = node.Value;
            while (node.Left != null)
            {
                minValue = node.Left.Value;
                node = node.Left;
            }
            return minValue;
        }
        #endregion

        #endregion

        #region Обходы по дереву

        #region Прямой (pre-order) обход: корень -> левое поддерево -> правое поддерево
        /// <summary>
        /// Прямой (pre-order) обход: корень -> левое поддерево -> правое поддерево
        /// </summary>
        public void PreOrderTraversal(Action<int> visit)
        {
            PreOrderTraversalRec(Root, visit);
        }

        /// <summary>
        /// Прямой (pre-order) обход: корень -> левое поддерево -> правое поддерево
        /// </summary>
        private void PreOrderTraversalRec(TreeNode<T> node, Action<int> visit)
        {
            if (node != null)
            {
                visit(node.SearchDataCount);
                PreOrderTraversalRec(node.Left, visit);
                PreOrderTraversalRec(node.Right, visit);
            }
        }
        #endregion

        #region Центрированный (in-order) обход: левое поддерево -> корень -> правое поддерево
        /// <summary>
        /// Центрированный (in-order) обход: левое поддерево -> корень -> правое поддерево
        /// </summary>
        public void InOrderTraversal(Action<int> visit)
        {
            InOrderTraversalRec(Root, visit);
        }

        /// <summary>
        /// Центрированный (in-order) обход: левое поддерево -> корень -> правое поддерево
        /// </summary>
        private void InOrderTraversalRec(TreeNode<T> node, Action<int> visit)
        {
            if (node != null)
            {
                InOrderTraversalRec(node.Left, visit);
                visit(node.SearchDataCount);
                InOrderTraversalRec(node.Right, visit);
            }
        }
        #endregion

        #region Обратный (post-order) обход: левое поддерево -> правое поддерево -> корень
        /// <summary>
        /// Обратный (post-order) обход: левое поддерево -> правое поддерево -> корень
        /// </summary>
        public void PostOrderTraversal(Action<int> visit)
        {
            PostOrderTraversalRec(Root, visit);
        }

        /// <summary>
        /// Обратный (post-order) обход: левое поддерево -> правое поддерево -> корень
        /// </summary>
        private void PostOrderTraversalRec(TreeNode<T> node, Action<int> visit)
        {
            if (node != null)
            {
                PostOrderTraversalRec(node.Left, visit);
                PostOrderTraversalRec(node.Right, visit);
                visit(node.SearchDataCount);
            }
        }
        #endregion

        #endregion

        #endregion
    }
    #endregion

    #region Узел
    /// <summary>
    /// Узел
    /// </summary>
    public class TreeNode<T>
    {
        public T Value { get; set; }
        public TreeNode<T> Left { get; set; }
        public TreeNode<T> Right { get; set; }
        public int SearchDataCount { get; set; }

        public TreeNode(T value)
        {
            Value = value;
            Left = null;
            Right = null;
            SearchDataCount = 0;
        }
    }
    #endregion
}
