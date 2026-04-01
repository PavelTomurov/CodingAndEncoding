using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;

namespace CodingAndEncoding
{
    /// <summary>
    /// Инструмент для работы с индексом
    /// </summary>
    public static class FileIndexer
    {
        /// <summary>
        /// Расширение файлов-элемнтов индекса
        /// </summary>
        public const string TLVExtension = ".tlv";

        /// <summary>
        /// Местоположение индекса
        /// </summary>
        public const string IndexPath = "C:\\files\\indexes\\";

        /// <summary>
        /// Создание файла индекса
        /// </summary>
        public static void CreateIndexFile(TLV tlv, string indexPath)
        {
            string fileName = CreateKey(tlv) + TLVExtension;
            if (indexPath == null) indexPath = IndexPath;
            File.WriteAllBytes(indexPath + fileName, EncodeAndDecodeHelper.SerializationTLV(tlv));
        }

        /// <summary>
        /// Создание индекса
        /// </summary>
        public static TLVIndex CreateIndex()
        {
            TLVIndex tlv_index = new TLVIndex();
            if (Directory.Exists(IndexPath))
            {
                string[] files = Directory.GetFiles(IndexPath, "*", SearchOption.TopDirectoryOnly);
                foreach (string s in files)
                {
                    string key = Path.GetFileNameWithoutExtension(s);
                    TLV tlv = EncodeAndDecodeHelper.DeserializationTLV(File.ReadAllBytes(s));
                    tlv_index.AddTLV(key, tlv);
                }
            }
            return tlv_index;
        }

        public static TLV SearchInIndex(TLV request)
        {
            if(Directory.Exists(IndexPath))
            {
                string[] files = Directory.GetFiles(IndexPath, "*", SearchOption.TopDirectoryOnly);
                foreach (string s in files)
                {
                    TLV content = EncodeAndDecodeHelper.DeserializationTLV(File.ReadAllBytes(s));
                    if (request.SubTLVs.First().Length != content.SubTLVs.First().Length)
                        return null;

                    for (int j = 0; j < request.SubTLVs.Count; j++)
                    {
                        //Сравнение D-D, files-files, pictures-pictures, a.JPG-Кот.JPG
                        var res = Search.MemcmpCompare(request.SubTLVs[j].Value, content.SubTLVs[0].SubTLVs[j].Value);
                        //Если массивы байтов полностью одинаковые (не только по длине, но и по значению),
                        //То возвращается [0],
                        //Иначе,
                        //Если левый больше, то возвращается [1], если правый - то [-1]
                        if (res != 0)
                            break;
                        //Если цикл завершается НЕ ПРЕРЫВАНИЕМ, значит найден нужный контент
                        if (j == request.SubTLVs[0].SubTLVs.Count - 1)
                        {
                            return content;
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
            }
            return null;
        }


        /// <summary>
        /// Создание ключа для элемента индекса
        /// Блоки TLV для хеша (block_to_hash) - C->files->exe->repair
        /// Массивы байтов для хеша (list_bytes_to_hash) - из блоков TLV для хеша, значение Value
        /// Байты для хеша (bytes_to_hash) - соединение массивов для хеша в один массив
        /// Алгоритм хеширования MD5
        /// Получение хеша в виде массива байтов
        /// Перевод хеша в HEX-строку (формат string)
        /// </summary>
        public static string CreateKey(TLV tlv)
        {
            var block_to_hash = tlv.SubTLVs.First().SubTLVs;
            var list_bytes_to_hash = block_to_hash.Where(v => v.Value.Length != 0).Select(v => v.Value)/*.Where(v => v != tlv.SubTLVs.First().SubTLVs.Last().Value)*/.ToList();
            List<byte> bytes_to_hash = new List<byte>();
            foreach(var b in list_bytes_to_hash)
                bytes_to_hash.AddRange(b.ToArray());
            using var algo = HashAlgorithm.Create(nameof(MD5));
            var hash = algo!.ComputeHash(bytes_to_hash.ToArray());
            return Convert.ToHexString(hash);
        }
        
    }

    /// <summary>
    /// Класс и методы индекса для блоков TLV
    /// </summary>
    public class TLVIndex
    {
        /// <summary>
        /// Индекс для блоков TLV
        /// </summary>
        public Dictionary<string, TLV> index = new Dictionary<string, TLV>();

        /// <summary>
        /// Добавление элемента индекса в индекс
        /// </summary>
        public void AddTLV(string key, TLV tlv)
        {
            if (!index.ContainsKey(key))
            {
                index[key] = tlv;
            }
            //index[key].Add(tlv);
        }

        /// <summary>
        /// Поиск в индексе по ключу
        /// </summary>
        public TLV Search(string key)
        {
            return index.ContainsKey(key) ? index[key] : null;
        }
    }
}


