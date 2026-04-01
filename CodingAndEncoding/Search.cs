using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace CodingAndEncoding
{
    public static class Search
    {
        #region Сравнение двух блоков TLV для осуществления поиска
        /// <summary>
        /// Сравнение двух блоков TLV для осуществления поиска
        /// </summary>
        public static TLV Search_By_Request(TLV tlv_interest, TLV tlv_content)
        {
            //Именной блок пакета данных (внутри 4 TLV с типом 8 "D:\\files\\pictures\\a.JPG")
            var content = tlv_content.SubTLVs[0];

            //Именной блок пакета интереса (запроса)
            var interest = tlv_interest.SubTLVs[0];

            //Количество элементов имени (GenericNameComponent) должно быть одинаковым
            //Пакет интереса:   C:\\files\\pictures
            //Пакет данных:     C:\\files\\pictures
            if (content.SubTLVs.Count != interest.SubTLVs.Count)
                return null;
            for (int j = 0; j < interest.SubTLVs.Count; j++)
            {
                //Сравнение D-D, files-files, pictures-pictures, a.JPG-Кот.JPG
                var res = MemcmpCompare(interest.SubTLVs[j].Value, content.SubTLVs[j].Value);
                //Если массивы байтов полностью одинаковые (не только по длине, но и по значению),
                //То возвращается [0],
                //Иначе,
                //Если левый больше, то возвращается [1], если правый - то [-1]
                if (res != 0)
                    break;
                //Если цикл завершается НЕ ПРЕРЫВАНИЕМ, значит найден нужный контент
                if (j == tlv_interest.SubTLVs[0].SubTLVs.Count - 1)
                {
                    return tlv_content;
                }
            }

            return null;
        }
        #endregion

        #region Поиск контента в базе данных по запросу
        /// <summary>
        /// Поиск контента в базе данных по запросу
        /// </summary>
        public static TLV Search_By_Request(TLV tlv_interest, List<TLV> tlvs)
        {
            for (int i = 0; i < tlvs.Count; i++)
            {

                var res = Search_By_Request(tlv_interest, tlvs[i]);
                if (res != null)
                    return res;
            }
            //В этой точке поиск заканчивается.
            //Если ранее не было ничего найдено, то функция возвращает пустоту - null.
            return null;
        }
        #endregion

        #region Поиск контента по байтам №1
        [DllImport("msvcrt.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern int memcmp(byte[] b1, byte[] b2, UIntPtr count);
        /// <summary>
        /// Если массивы байтов полностью одинаковые (не только по длине, но и по значению),
        /// То возвращается [0],
        /// Иначе,
        /// Если левый больше, то возвращается [1], если правый - то [-1]
        /// </summary>
        public static int MemcmpCompare(byte[] b1, byte[] b2)
        {
            var retval = memcmp(b1, b2, new UIntPtr((uint)b1.Length));
            return retval;
        }
        #endregion

        #region Поиск контента по байтам №2
        /// <summary>
        /// Если массивы байтов полностью одинаковые (не только по длине, но и по значению),
        /// то возвращается true,
        /// иначе, при любом отличии возвращается false
        /// </summary>
        public static class ByteArrayExtensions
        {
            [DllImport("msvcrt.dll", CallingConvention = CallingConvention.Cdecl)]
            private static extern int memcmp(byte[] b1, byte[] b2, UIntPtr count);

            /// <summary>
            /// Если массивы байтов полностью одинаковые (не только по длине, но и по значению),
            /// То возвращается true,
            /// Иначе, при любом отличии возвращается false
            /// </summary>
            public static bool SequenceEqual(byte[] b1, byte[] b2)
            {
                if (b1 == b2) return true; //reference equality check

                if (b1 == null || b2 == null || b1.Length != b2.Length) return false;

                return memcmp(b1, b2, new UIntPtr((uint)b1.Length)) == 0;
            }
        }
        #endregion
    }
}


