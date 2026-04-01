using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Diagnostics;

namespace CodingAndEncoding
{
    public static class Program
    {
        static void Main(string[] args)
        {


            double t = 2; //Степень дерева
            double k = 1; //Индекс вершины

            double lk = 0; //Уровень вершины
            double pk = 0; //Позиция вершины на уровне

            lk = Math.Floor(Math.Log2(k * (t - 1) + 1));
            

            //https://www.programmerall.com/article/50757716/ Работа с TLV
            Console.WriteLine("Hello, World!");
            //https://metanit.com/sharp/tutorial/5.2.php Работа с файлами и папками

            bool needCD = true;
            bool needSearch = true;

            bool needResearch = false;

            //Папка с файлами
            string dirName = "C:\\files";

            //Папка с файлами для глобального поиска
            string dirNameForBigSearch = "C:\\files research";

            //Папка с декодированными файлами
            string answers = "C:\\filess\\ANSWERS";

            //Папка для найденных результатов
            string answers_search = "C:\\filess\\ANSWERS_SEARCH";

            //Папка для проверенных результатов
            string answers_signature_verification = "C:\\filess\\ANSWERS_SIGNATURE_VERIFICATION";

            //Запросы
            List<string> requests = new List<string> 
            {
                "C:\\files\\pictures\\Лиса.JPG" 
                ,"C:\\files\\text\\Реферат доработка.pdf"
                
                #region Большой поиск
                /*
                "C:\\files research\\pictures normal\\Поиск.JPG", 
                
                "C:\\files research\\pictures normal\\Поиск.JPG", "C:\\files research\\pictures normal\\Поиск.JPG",
                "C:\\files research\\pictures normal\\Поиск.JPG", "C:\\files research\\pictures normal\\Поиск.JPG",
                "C:\\files research\\pictures normal\\Поиск.JPG", "C:\\files research\\pictures normal\\Поиск.JPG",
                "C:\\files research\\pictures normal\\Поиск.JPG", "C:\\files research\\pictures normal\\Поиск.JPG",
                "C:\\files research\\pictures normal\\Поиск.JPG", "C:\\files research\\pictures normal\\Поиск.JPG",
                "C:\\files research\\pictures normal\\Поиск.JPG", "C:\\files research\\pictures normal\\Поиск.JPG",
                "C:\\files research\\pictures normal\\Поиск.JPG", "C:\\files research\\pictures normal\\Поиск.JPG",
                "C:\\files research\\pictures normal\\Поиск.JPG", "C:\\files research\\pictures normal\\Поиск.JPG",
                "C:\\files research\\pictures normal\\Поиск.JPG", "C:\\files research\\pictures normal\\Поиск.JPG",
                "C:\\files research\\pictures normal\\Поиск.JPG", "C:\\files research\\pictures normal\\Поиск.JPG",
                "C:\\files research\\pictures normal\\Поиск.JPG", "C:\\files research\\pictures normal\\Поиск.JPG",
                "C:\\files research\\pictures normal\\Поиск.JPG", "C:\\files research\\pictures normal\\Поиск.JPG",
                "C:\\files research\\pictures normal\\Поиск.JPG", "C:\\files research\\pictures normal\\Поиск.JPG",
                "C:\\files research\\pictures normal\\Поиск.JPG", "C:\\files research\\pictures normal\\Поиск.JPG",
                "C:\\files research\\pictures normal\\Поиск.JPG", "C:\\files research\\pictures normal\\Поиск.JPG",
                "C:\\files research\\pictures normal\\Поиск.JPG", "C:\\files research\\pictures normal\\Поиск.JPG",
                "C:\\files research\\pictures normal\\Поиск.JPG", "C:\\files research\\pictures normal\\Поиск.JPG",
                "C:\\files research\\pictures normal\\Поиск.JPG", "C:\\files research\\pictures normal\\Поиск.JPG",
                "C:\\files research\\pictures normal\\Поиск.JPG", "C:\\files research\\pictures normal\\Поиск.JPG",
                "C:\\files research\\pictures normal\\Поиск.JPG", "C:\\files research\\pictures normal\\Поиск.JPG",
                "C:\\files research\\pictures normal\\Поиск.JPG", "C:\\files research\\pictures normal\\Поиск.JPG",
                "C:\\files research\\pictures normal\\Поиск.JPG", "C:\\files research\\pictures normal\\Поиск.JPG",
                "C:\\files research\\pictures normal\\Поиск.JPG", "C:\\files research\\pictures normal\\Поиск.JPG",
                "C:\\files research\\pictures normal\\Поиск.JPG", "C:\\files research\\pictures normal\\Поиск.JPG",
                "C:\\files research\\pictures normal\\Поиск.JPG", "C:\\files research\\pictures normal\\Поиск.JPG",
                "C:\\files research\\pictures normal\\Поиск.JPG", "C:\\files research\\pictures normal\\Поиск.JPG",
                "C:\\files research\\pictures normal\\Поиск.JPG", "C:\\files research\\pictures normal\\Поиск.JPG",
                "C:\\files research\\pictures normal\\Поиск.JPG", "C:\\files research\\pictures normal\\Поиск.JPG",
                "C:\\files research\\pictures normal\\Поиск.JPG", "C:\\files research\\pictures normal\\Поиск.JPG",
                "C:\\files research\\pictures normal\\Поиск.JPG", "C:\\files research\\pictures normal\\Поиск.JPG",
                "C:\\files research\\pictures normal\\Поиск.JPG", "C:\\files research\\pictures normal\\Поиск.JPG",
                "C:\\files research\\pictures normal\\Поиск.JPG", "C:\\files research\\pictures normal\\Поиск.JPG",
                "C:\\files research\\pictures normal\\Поиск.JPG", "C:\\files research\\pictures normal\\Поиск.JPG",
                "C:\\files research\\pictures normal\\Поиск.JPG", "C:\\files research\\pictures normal\\Поиск.JPG",
                "C:\\files research\\pictures normal\\Поиск.JPG", "C:\\files research\\pictures normal\\Поиск.JPG",
                "C:\\files research\\pictures normal\\Поиск.JPG", "C:\\files research\\pictures normal\\Поиск.JPG",

                "C:\\files research\\pictures normal\\Реферат доработка.pdf", "C:\\files research\\pictures normal\\Реферат доработка.pdf",
                "C:\\files research\\pictures normal\\Реферат доработка.pdf", "C:\\files research\\pictures normal\\Реферат доработка.pdf",
                "C:\\files research\\pictures normal\\Реферат доработка.pdf", "C:\\files research\\pictures normal\\Реферат доработка.pdf",


                "C:\\files research\\pictures big\\Реферат доработка.pdf", "C:\\files research\\pictures big\\Реферат доработка.pdf",
                "C:\\files research\\pictures big\\Реферат доработка.pdf", "C:\\files research\\pictures big\\Реферат доработка.pdf",
                "C:\\files research\\pictures big\\Реферат доработка.pdf", "C:\\files research\\pictures big\\Реферат доработка.pdf",


                "C:\\files research\\pictures small\\Реферат доработка.pdf", "C:\\files research\\pictures small\\Реферат доработка.pdf",
                "C:\\files research\\pictures small\\Реферат доработка.pdf", "C:\\files research\\pictures small\\Реферат доработка.pdf",
                "C:\\files research\\pictures small\\Реферат доработка.pdf", "C:\\files research\\pictures small\\Реферат доработка.pdf",
                */
                #endregion

            };

            if (needResearch)
            {
                string big = "C:\\files research\\pictures big";
                string normal = "C:\\files research\\pictures normal";
                string small = "C:\\files research\\pictures small";
                List<string> folders = new List<string>() { big, normal, small };
                foreach(var folder in folders)
                    TEST_EncodingData_Time(folder);
            }
            else
            {

                if (needCD)
                {
                    //***Тестирование кодирования и декодирования файлов***//
                    TEST_Main_CD_Function(dirName, answers);
                }
                else if (needSearch)
                {

                    //Создание индекса
                    TLVIndex index = FileIndexer.CreateIndex();

                    //***Тестирование поиска файлов (в блоках TLV ищем нужные)***//
                    TEST_Main_Search_Function(dirName, answers_search, requests, index);
                }
                else
                {
                    TEST_Main_Check_Content_Using_Signature(dirName, answers_signature_verification);
                }
            }

            #region Закомментировано

            #region Старое
            // Если папка существует
            //if (Directory.Exists(dirName))
            //{
            //Поиск и формирование блоков TLV из файлов
            //tlvs = Create_Files_Objects(dirName);

            //Вывод в консоль информации о блоках TLV
            //Output_Tlvs_Data_All(tlvs);

            #region Вывод в консоль (закомментировано)
            /*
            Console.WriteLine("Подкаталоги:");
            string[] dirs = Directory.GetDirectories(dirName);
            foreach (string s in dirs)
            {
                Console.WriteLine(s);
            }
            Console.WriteLine();
            Console.WriteLine("Файлы:");
            string[] files = Directory.GetFiles(dirName, "*", SearchOption.AllDirectories);
            foreach (string s in files)
            {
                Console.WriteLine(s);
            }
            */
            #endregion
            //}
            #endregion

            #region Пробы кодирования/раскодирования на строках
            /*
            byte[] bytes = Encoding.Unicode.GetBytes("a.JPG");
            string hexString = Convert.ToHexString(bytes);
            var fromHexString = System.Convert.FromHexString(hexString);
            var decode = System.Text.Encoding.Unicode.GetString(fromHexString);
            */


            /*
            var plainTextBytes = System.Text.Encoding.Unicode.GetBytes("a");
            var encodeExmp = System.Convert.ToBase64String(plainTextBytes);

            var base64EncodedBytes = System.Convert.FromBase64String(encodeExmp);
            var decode = System.Text.Encoding.Unicode.GetString(base64EncodedBytes);
            
            var ddddd = Encoding.Unicode.GetString(plainTextBytes);
            */
            #endregion

            #endregion

            Console.ReadKey();
        }


        #region Функции для исследований

        #region Функция для подсчёта времени на кодирование данных

        static void TEST_EncodingData_Time(string folder)
        {
            Console.WriteLine("\n");
            Console.WriteLine("Папка {0}", folder);
            for (int i = 0; i < 10; i++)
            {
                double time = 0.0;
                var sw = Stopwatch.StartNew();

                //**Формирование пакетов данных из файлов**//
                //Коллекция пакетов данных
                List<DataPacket> data_packets = new List<DataPacket>();
                TEST_Create_Data_Packets(folder, data_packets);

                //**Кодирование пакетов данных в блоки TLV**//
                //Коллекции закодированных в TLV-блоки файлов
                List<TLV> tlvs = new List<TLV>();
                TEST_Encoding_DataPackets_To_TLVs(data_packets, tlvs, false);

                sw.Stop();
                time = sw.ElapsedMilliseconds;
                Console.WriteLine("Итерация № {0}, затраченное время для кодирования: {1}", i+1, time);
                //Console.WriteLine("\n");
            }
        }

        #endregion

        #endregion


        #region Функции для тестирования проверки контента посредством подписи производителя

        #region Основная функция для тестирования проверки контента посредством подписи производителя
        /// <summary>
        /// Основная функция для тестирования проверки контента посредством подписи производителя
        /// </summary>
        static void TEST_Main_Check_Content_Using_Signature(string dirName, string answers_signature_verification)
        {
            //***Тестирование проверки контента посредством подписи производителя***//

            Console.WriteLine("Путь к файлам: " + dirName);
            Console.WriteLine("\n");

            //**Формирование пакетов данных из файлов**//
            //Коллекция пакетов данных
            List<DataPacket> data_packets = new List<DataPacket>();
            TEST_Create_Data_Packets(dirName, data_packets);

            //**Кодирование пакетов данных в блоки TLV**//
            //Коллекции закодированных в TLV-блоки файлов
            List<TLV> tlvs = new List<TLV>();
            TEST_Encoding_DataPackets_To_TLVs(data_packets, tlvs, false);

            if (tlvs.Count > 0)
            {
                //Кодирование блоков TLV
                List<EncodeData> encodes = TEST_Encoding_TLVs_To_Bytes(tlvs);

                //Проверка сериализации и десериализации
                List<Array> encodes_bytes = new List<Array>();
                foreach (var enc in encodes) { try { encodes_bytes.Add(EncodeAndDecodeHelper.Serialization(enc)); } catch (Exception e) { continue; } }

                //***************************** Происходит порча пакетов после сериализации *****************************//
                //TEST_ReverseBytes((byte[])encodes_bytes[0]);
                //TEST_ReverseBytes((byte[])encodes_bytes.Last());

                List<EncodeData> encodes_normal = new List<EncodeData>();
                foreach (var enc in encodes_bytes) { try { encodes_normal.Add(EncodeAndDecodeHelper.Deserialization((byte[])enc)); } catch (Exception e) { continue; } }

                //***************************** Происходит порча пакетов в Encodes *****************************//
                //TEST_ReverseBytes(encodes[0].Encode);
                //TEST_ReverseBytes(encodes.Last().Encode);

                List<EncodeData> encodes_verification = new List<EncodeData>();
                foreach (var enc in encodes_normal)
                {
                    //***************************** Вычисляются хеши у каждого массива байтов у Encodes *****************************//
                    byte[] hash = RSAProvider.GetHash(enc.Encode);
                    byte[] signature = enc.Signature;
                    bool verification = RSAProvider.VerifySignature(hash, signature, RSAProvider.GetPublicKey(enc.PublicKey));
                    if (verification)
                    {
                        encodes_verification.Add(enc);
                    }
                    else
                    {
                        //***************************** Пакеты, чей хеш не совпадает с только что вычисленным блокируются *****************************//

                    }
                }
                //Декодирование блоков TLV
                List<TLV> decoded_tlvs = TEST_Decoding_Bytes_To_TLVs(encodes_verification);

                //Тестирование файлов - нормально ли раскодируются и пригодны ли для передачи
                TEST_Testing_Decoding_Files(answers_signature_verification, decoded_tlvs);
            }
        }
        #endregion

        #region Порча массива байтов
        /// <summary>
        /// Порча массива байтов
        /// </summary>
        static void TEST_ReverseBytes(byte[] array)
        {
            for (int i = 0; i < array.Length / 2; i++)
            {
                byte temp = array[i];
                array[i] = array[array.Length - i - 1];
                array[array.Length - i - 1] = temp;
            }

        }
        #endregion

        #endregion

        #region Функции для тестирования поиска файлов

        #region Основная функция для тестирования поиска файлов
        /// <summary>
        /// Основная функция для тестирования поиска файлов
        /// </summary>
        static void TEST_Main_Search_Function(string dirName, string answers_search, List<string> requests, TLVIndex index)
        {
            //Папка, в которой происходит поиск контента
            Console.WriteLine("Путь к файлам: " + dirName);
            Console.WriteLine("\n");

            //Если нет папки для результатов, то она будет создана
            Directory.CreateDirectory(answers_search);

            //**Формирование пакетов данных из файлов**//
            //Коллекция пакетов данных
            List<DataPacket> data_packets = new List<DataPacket>();
            TEST_Create_Data_Packets(dirName, data_packets);

            //**Кодирование пакетов данных в блоки TLV**//
            //Коллекции закодированных в TLV-блоки файлов
            List<TLV> tlvs = new List<TLV>();
            TEST_Encoding_DataPackets_To_TLVs(data_packets, tlvs, false);

            //**Формирование пакетов интереса**//
            List<InterestPacket> interest_packets = new List<InterestPacket>();
            TEST_Create_Interest_Packets(interest_packets, requests);

            //**Кодирование пакетов интереса в блоки TLV**//
            //Коллекции закодированных в TLV-блоки файлов
            List<TLV> tlvs_interests = new List<TLV>();
            TEST_Encoding_InterestPackets_To_TLVs(tlvs_interests, interest_packets);



            //**Результаты поиска в формате TLV**//
            List<TLV> answers_tlvs = new List<TLV>();

            bool nis = true;

            var sw = Stopwatch.StartNew();

            int index_found = 0;
            int cashe_found = 0;

            //**Поиск контента по запросам**//
            for (int i = 0; i < tlvs_interests.Count; i++)
            {
                //**Поиск контента по индексу**//
                string key = FileIndexer.CreateKey(tlvs_interests[i]);
                var index_result = index.Search(key);
                if (nis && index_result != null)
                {
                    answers_tlvs.Add(index_result);
                    Console.WriteLine("Затраченное время на поиск в индексе: {0}, мс, № запроса: {1}", sw.ElapsedMilliseconds, i+1);
                    index_found += 1;
                }
                else
                {
                    //**Поиск контента по запросам**//
                    var content = Search.Search_By_Request(tlvs_interests[i], tlvs);
                    if(content != null)
                    {
                        answers_tlvs.Add(content);
                        Console.WriteLine("Затраченное время на поиск в кэше: {0}, мс, № запроса: {1}", sw.ElapsedMilliseconds, i+1);
                        cashe_found += 1;
                    }
                        
                    
                }

            }

            /*
            foreach(var f in answers_tlvs)
            {
                FileIndexer.CreateIndexFile(f);
            }
            */

            //Передача файлов
            TEST_Testing_Decoding_Files(answers_search, answers_tlvs);

            sw.Stop();
            Console.WriteLine("Найдено в индексе: {0}, файлов", index_found);
            Console.WriteLine("Найдено в кэше: {0}, файлов", cashe_found);

        }
        #endregion

        #region Формирую TLV-блоки из пакетов интереса
        /// <summary>
        /// Формирую TLV-блоки из пакетов интереса
        /// </summary>
        public static void TEST_Encoding_InterestPackets_To_TLVs(List<TLV> result, List<InterestPacket> interest_packets)
        {
            foreach (var interest in interest_packets)
            {
                TLV tlv_interest = interest.GetInterestPacketTLV(interest);
                result.Add(tlv_interest);
            }
        }
        #endregion

        #region Формирую пакеты интереса

        public static void TEST_Create_Interest_Packets(List<InterestPacket> result, List<string> requests)
        {
            foreach(var req in requests)
            {
                InterestPacket interest = CreatorHelper.GetInterestPacket(5, req, 4000, false);
                result.Add(interest);
            }
        }
        #endregion

        #endregion

        #region Функции для тестирования кодирования и декодирования файлов

        #region Основная функция для тестирования кодирования и декодирования файлов
        /// <summary>
        /// Основная функция для тестирования кодирования и декодирования файлов
        /// </summary>
        static void TEST_Main_CD_Function(string dirName, string answers_path)
        {
            Console.WriteLine("Путь к файлам: " + dirName);
            Console.WriteLine("\n");

            //**Формирование пакетов данных из файлов**//
            //Коллекция пакетов данных
            List<DataPacket> data_packets = new List<DataPacket>();
            TEST_Create_Data_Packets(dirName, data_packets);

            //**Кодирование пакетов данных в блоки TLV**//
            //Коллекции закодированных в TLV-блоки файлов
            List<TLV> tlvs = new List<TLV>();
            TEST_Encoding_DataPackets_To_TLVs(data_packets, tlvs, false);

            if (tlvs.Count > 0)
            {
                //Вывод в консоль информации о блоках TLV
                Output.Output_Tlvs_Data_All(tlvs);

                //Кодирование блоков TLV
                List<EncodeData> encodes = TEST_Encoding_TLVs_To_Bytes(tlvs);

                #region Проверка на сериализацию/десериализацию
                //Проверка на сериализацию/десериализацию
                
                List<Array> encodes_serialized = new List<Array>();
                List<EncodeData> encodes_deserialized = new List<EncodeData>();
                foreach (var enc in encodes) { encodes_serialized.Add(EncodeAndDecodeHelper.Serialization(enc)); }
                foreach (var arr in encodes_serialized) { encodes_deserialized.Add(EncodeAndDecodeHelper.Deserialization((byte[])arr)); }
                
                #endregion

                

                //Декодирование блоков TLV
                List<TLV> decoded_tlvs = TEST_Decoding_Bytes_To_TLVs(encodes);

                //Тестирование файлов - нормально ли раскодируются и пригодны ли для передачи
                TEST_Testing_Decoding_Files(answers_path, decoded_tlvs);
            }
        }
        #endregion

        #region Формирую пакеты данных из файлов
        /// <summary>
        /// Формирую пакеты данных из файлов
        /// </summary>
        public static void TEST_Create_Data_Packets(string dirName, List<DataPacket> result)
        {
            if (Directory.Exists(dirName))
            {
                string[] files = Directory.GetFiles(dirName, "*", SearchOption.AllDirectories);
                foreach (string s in files)
                {
                    DataPacket data_packet = CreatorHelper.GetDataPacket(6, s, 24, 4000, File.ReadAllBytes(s));
                    result.Add(data_packet);
                }
            }
            else
            {
                throw new Exception("Ошибка!" + "Указан путь: " + dirName + ". " + "Такой папки нет.");
            }

        }
        #endregion

        #region Кодирование пакетов данных в блоки TLV
        /// <summary>
        /// Кодирование пакетов данных в блоки TLV
        /// </summary>
        public static void TEST_Encoding_DataPackets_To_TLVs(List<DataPacket> data_packets, List<TLV> tlvs, bool needSaveTLV)
        {
            if(data_packets.Count > 0)
            {
                //Кодирование пакетов данных в блоки TLV
                foreach (var dP in data_packets)
                {
                    tlvs.Add(dP.GetDataPacketTLV(dP));
                    if(needSaveTLV)
                    {
                        string path = dP.NameString.Replace("files", "files_tlvs");
                        path = path.Remove(path.LastIndexOfAny(new char[] { '\\' }, path.LastIndexOf('\\'))) + '\\';
                        FileIndexer.CreateIndexFile(dP.GetDataPacketTLV(dP), path);
                    }
                }
            }
            else
            {
                throw new Exception("Ошибка! Нет пакетов данных.");
            }
        }
        #endregion

        #region Кодирование блоков TLV в байты
        /// <summary>
        /// Кодирование блоков TLV в байты
        /// </summary>
        static List<EncodeData> TEST_Encoding_TLVs_To_Bytes(List<TLV> tlvs)
        {
            //Создание провайдера
            RSAProvider rsa_provider = new RSAProvider();

            //Генерация ключей
            rsa_provider.AssignNewKey();

            List<EncodeData> encodes = new List<EncodeData>();
            foreach (var tlv in tlvs)
            {
                //Информация для декодирования массива байтов
                List<Inside> insides = new List<Inside>();

                //Создание массива байтов из блока TLV
                byte[] encode = EncodeAndDecodeHelper.Encoding_TLV(tlv, insides);

                //Получение хеша контента (по алгоритму SHA256)
                byte[] hash = RSAProvider.GetHash(encode);
                
                //Получение подписи
                byte[] signature_byte = rsa_provider.SignData(hash);

                //В Encodes передаю:
                //1) закодированный контент,
                //2) вспомогательный объект Insides
                //3) саму подпись - это массив байтов
                //4) открытый ключ
                encodes.Add(new EncodeData(encode, insides, signature_byte, new RSAParametersSerializable(rsa_provider.GetPublicKey())));                
            }
            return encodes;
        }
        #endregion

        #region Декодирование байтов в блоки TLV
        /// <summary>
        /// Декодирование байтов в блоки TLV
        /// </summary>
        static List<TLV> TEST_Decoding_Bytes_To_TLVs(List<EncodeData> encodes)
        {
            List<TLV> decoded_tlvs = new List<TLV>();
            foreach (var enc in encodes)
            {
                decoded_tlvs.Add(EncodeAndDecodeHelper.Decoding_To_TLV(enc.Encode, enc.InsideInfo));
            }
            return decoded_tlvs;
        }
        #endregion

        #region Тестирование файлов - нормально ли раскодируются и пригодны ли для передачи
        /// <summary>
        /// Тестирование файлов - нормально ли раскодируются и пригодны ли для передачи
        /// </summary>
        public static void TEST_Testing_Decoding_Files(string answers_path, List<TLV> decoded_tlvs)
        {
            Directory.CreateDirectory(answers_path);
            if (decoded_tlvs.Count > 0)
            {
                for (int i = 0; i < decoded_tlvs.Count; i++)
                {
                    string final_block = Encoding.Unicode.GetString(Convert.FromHexString(BitConverter.ToString(decoded_tlvs[i].SubTLVs[1].SubTLVs.Last().Value).Replace("-", "")));
                    File.WriteAllBytes(answers_path + "\\" + "-" + final_block, decoded_tlvs[i].SubTLVs.Last().Value);
                }
            }
            else
            {
                return;
            }
        }
        #endregion

        #region Формирую объекты из файлов (старое)
        /// <summary>
        /// Формирую объекты из файлов (старое)
        /// </summary>
        /*
        public static List<TLV> Create_Files_Objects(string dirName)
        {
            List<TLV> result = new List<TLV>();
            string[] files = Directory.GetFiles(dirName, "*", SearchOption.AllDirectories);

            foreach (string s in files)
            {
                //Блоки TLV пакета данных
                List<TLV> datapacket_tlvs = new List<TLV>();

                // 1)********************************************* Формирую блок TLV для поля наименования *********************************************
                List<string> path_elements = s.Split('\\').ToList();
                //path_elements.RemoveAt(path_elements.Count - 1);

                //Блоки TLV для имени
                List<TLV> name_tlvs = new List<TLV>();
                foreach (var p in path_elements)
                    name_tlvs.Add(CreatorHelper.Create_TLV(8, p));

                //Длина
                uint name_field_length = CreatorHelper.Find_TLVs_Length(name_tlvs);
                //Блок TLV
                TLV tlv_name = new TLV(7, name_field_length, name_tlvs);
                datapacket_tlvs.Add(tlv_name);

                //********************************************* 2) Формирую блок TLV для поля метаданных *********************************************

                //Блок TLV для типа контента
                TLV content_type = CreatorHelper.Create_TLV(24, 0);

                //Блок TLV для периода актуальности данных
                TLV freshness_period = CreatorHelper.Create_TLV(25, 4000);

                //Блок TLV для последнего элемента в абсолютном пути к файлу
                TLV final_block_id = CreatorHelper.Create_TLV(26, s.Split('\\').Last());

                //Блоки TLV для метаданных
                List<TLV> metadata_tlvs = new List<TLV>();
                metadata_tlvs.Add(content_type);
                metadata_tlvs.Add(freshness_period);
                metadata_tlvs.Add(final_block_id);

                //Длина
                uint metadata_length = CreatorHelper.Find_TLVs_Length(metadata_tlvs);
                //Блок TLV
                TLV tlv_metadata = new TLV(20, metadata_length, metadata_tlvs);
                datapacket_tlvs.Add(tlv_metadata);

                // 3)********************************************* Формирую блок TLV для поля контента *********************************************
                FileStream file = File.OpenRead(s);

                //Блок TLV
                TLV tlv_content = new TLV(21, file.Length, File.ReadAllBytes(s));
                datapacket_tlvs.Add(tlv_content);

                //Длина
                uint datapacket_length = CreatorHelper.Find_TLVs_Length(datapacket_tlvs);

                //*** Блок TLV для пакета данных ***
                TLV datapacket_tlv = new TLV(6, datapacket_length, datapacket_tlvs);
                result.Add(datapacket_tlv);
            }
            return result;
        }
        */
        #endregion

        #endregion

    }



    #region Классы

    /// <summary>
    /// Класс для базы данных
    /// </summary>
    public class DataBase
    {
        /// <summary>
        /// Блоки TLV для кодирования
        /// </summary>
        public List<TLV> TLVs { get; set; }

        /// <summary>
        /// Закодированные данные
        /// </summary>
        public List<EncodeData> Encodes { get; set; }

        /// <summary>
        /// Блоки TLV после кодирования
        /// </summary>
        public List<TLV> DecodedTLVs { get; set; }
    }

    #endregion

    #region Пока не надо

    /// <summary>
    /// Класс контента
    /// </summary>
    public class Content
    {
        public ContentType Type { get; set; } //Тип контента
        public object Length { get; set; } //Длина контента
    }

    /// <summary>
    /// Классификатор контента:
    /// 1) Тектовые документы
    /// 2) Графические изображения
    /// 3) Видеоматериалы
    /// 4) Аудиоматериалы
    /// </summary>
    public enum ContentType
    {
        Text, //txt, doc, docx, html 
        Picture, //png, jpg, jpeg
        Video, //AVI, MKV, ASF, MP4, FLV, WebM
        Audio //WAV, AIFF, APE, FLAC, MP3, Ogg
    }

    #endregion

}
