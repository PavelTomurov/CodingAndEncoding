using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingAndEncoding
{
    class Testing
    {
        static void Main(string[] args)
        {
            #region Папки и запросы
            //Папка с файлами
            string dirNameSources = "C:\\files";
            //Папка с файлами TLV для построения по ним дерева
            string dirNameResults = "C:\\files_tlvs";
            //Папка с результатами поиска по дереву
            string answers_search = "C:\\filess\\ANSWERS_SEARCH";

            //Запросы
            List<string> requests = new List<string>
            {
                "C:\\files\\pictures\\Лиса.JPG"
                ,"C:\\files\\text\\Реферат доработка.pdf"
            };
            #endregion

            List<TLV> tlvs = new List<TLV>();

            bool needPrepareData = false;

            #region Подготовка файлов TLV
            if (needPrepareData)
            {
                //Формирование пакетов данных из файлов//
                //Коллекция пакетов данных
                List<DataPacket> data_packets = new List<DataPacket>();
                Program.TEST_Create_Data_Packets(dirNameSources, data_packets);

                //Кодирование пакетов данных в блоки TLV//
                //Коллекции закодированных в TLV-блоки файлов
                Program.TEST_Encoding_DataPackets_To_TLVs(data_packets, tlvs, true);
            }

            

            //Беру файлы TLV и сохраняю их в коллекцию
            if (Directory.Exists(dirNameResults))
            {
                string[] files = Directory.GetFiles(dirNameResults, "*.tlv", SearchOption.AllDirectories);
                foreach (string s in files)
                {
                    string key = Path.GetFileNameWithoutExtension(s);
                    TLV tlv = EncodeAndDecodeHelper.DeserializationTLV(File.ReadAllBytes(s));
                    tlvs.Add(tlv);
                }
            }

            FileIndexer.CreateIndexFile(tlvs[4], null);
            FileIndexer.CreateIndexFile(tlvs[9], null);
            #endregion

            #region Работа с запросами
            //**Формирование пакетов интереса**//
            List<InterestPacket> interest_packets = new List<InterestPacket>();
            Program.TEST_Create_Interest_Packets(interest_packets, requests);

            //**Кодирование пакетов интереса в блоки TLV**//
            //Коллекции закодированных в TLV-блоки файлов
            List<TLV> tlvs_interests = new List<TLV>();
            Program.TEST_Encoding_InterestPackets_To_TLVs(tlvs_interests, interest_packets);
            #endregion

            bool b_tree = true;
            bool avl_tree = true;

            List<TLV> answers_tlvs = new List<TLV>();

            if (b_tree)
            {
                //Строю дерево по коллекции TLV-объектов, полученных из TLV-файлов
                BinaryTree<TLV> tree = new BinaryTree<TLV>(tlvs[0]);
                foreach (var tlv in tlvs) { tree.Insert(tlv); }

                //Поиск по запросам в дереве
                foreach (var req in tlvs_interests)
                {
                    var res = tree.Contains(req);
                    answers_tlvs.Add(res.Value);
                }
            }
            else if(avl_tree)
            {
                #region Тест №1 Оранжевый сайт Learners Lesson
                //№1 Оранжевый сайт Learners Lesson
                /*
                AVLTree avlTree = new AVLTree();
                Node root = null;
                List<int> numbers = new List<int>() { 5, 3, 7, 2, 11, 43, 11, 5, 65, 1, 76, 78, 77, 55, 43, 54, 12, 8, 2, 4, 6, 7 };
                numbers = numbers.Distinct().ToList();
                foreach (var n in numbers)
                    root = avlTree.insert(root, n);


                TreeTraversal tt = new TreeTraversal();
                System.Console.WriteLine("Inorder Traversal : \n");
                tt.inOrder(root);
                */
                #endregion

                #region Тест №2 Github, интерфейсы (выбран этот метод)
                //№2 Github, интерфейсы
                /*
                AVLTree<int> tree_avl = new AVLTree<int>();
                List<int> numbers = new List<int>() { 5, 3, 7, 2, 11, 43, 11, 5, 65, 1, 76, 78, 77, 55, 43, 54, 12, 8, 2, 4, 6, 7 };
                numbers = numbers.Distinct().ToList();
                foreach (var n in numbers)
                    tree_avl.Add(n);
                Console.WriteLine(tree_avl);
                */
                #endregion

                #region Тест №3 Киберфорум
                //№3 Киберфорум
                /*
                AVL tree_avl = new AVL();
                List<int> numbers = new List<int>() { 5, 3, 2, 11, 43, 11, 5, 65, 1, 76, 78, 77, 55, 43, 54, 12, 8, 2, 4, 6, 7 };
                numbers = numbers.Distinct().ToList();
                foreach (var n in numbers)
                    tree_avl.Add(n);
                tree_avl.DisplayTree();
                */
                #endregion

                AVLTree<TLV> tree_avl = new AVLTree<TLV>();
                foreach (var tlv in tlvs) { tree_avl.Add(tlv); }

                //Поиск по запросам в дереве
                foreach (var req in tlvs_interests)
                {
                    var res = tree_avl.Contains(req);
                    answers_tlvs.Add(res.Value);
                }


            }
            else
            {

            }

            

            //Передача найденных файлов
            Program.TEST_Testing_Decoding_Files(answers_search, answers_tlvs);

            //tree.PreOrderTraversal(v => Console.WriteLine(v));
            //tree.InOrderTraversal(v => Console.WriteLine(v));
            //tree.PostOrderTraversal(v => Console.WriteLine(v));

            Console.ReadKey();
        }

    }
}
