using System;
using System.Collections.Generic;
using System.Linq;

namespace CodingAndEncoding
{
    public static class Output
    {
        #region Вывод информации о всех TLV-блоках
        /// <summary>
        /// Вывод информации о всех TLV-блоках
        /// </summary>
        public static void Output_Tlvs_Data_All(List<TLV> tlvs)
        {
            if (tlvs.Count == 0) return;
            Console.WriteLine("----------------------База данных блоков TLV----------------------\n");
            for (int i = 0; i < tlvs.Count; i++)
            {
                Console.WriteLine("");
                Console.WriteLine("[ПАКЕТ ДАННЫХ] Блок TLV для файла № " + (i + 1) + "[ПАКЕТ ДАННЫХ]");
                Console.WriteLine("Тип: " + tlvs[i].Type);
                Console.WriteLine("Длина: " + tlvs[i].Length);

                var sub_tlvs = tlvs[i].SubTLVs;
                Console.WriteLine("\nВложения основного блока TLV: \n");
                for (int k = 0; k < sub_tlvs.Count; k++)
                {
                    Console.WriteLine("Тип: " + sub_tlvs[k].Type);
                    Console.WriteLine("Длина: " + sub_tlvs[k].Length);
                    Console.WriteLine("");

                    if (sub_tlvs[k].SubTLVs.Count > 0)
                    {
                        Console.WriteLine("Количество вложенных блоков TLV: " + sub_tlvs[k].SubTLVs.Count + "\n");
                        Console.WriteLine("Вложения пакета данных: " + sub_tlvs[k].SubTLVs.Count);
                        for (int j = 0; j < sub_tlvs[k].SubTLVs.Count; j++)
                        {
                            Output_Tlv_Inside(sub_tlvs[k].SubTLVs[j], j, 3);
                        }
                    }
                    else
                    {
                        Console.WriteLine("Объём контента (в байтах): " + sub_tlvs[k].Value.Length);
                    }
                    Console.WriteLine("");
                }
            }
        }
        #endregion

        #region Вывод информации о TLV-блоке
        /// <summary>
        /// Вывод информации о TLV-блоке
        /// </summary>
        public static void Output_Tlv_Inside(TLV tlv, int j, int multiplier)
        {
            string begin = string.Join(" ", Enumerable.Repeat("", multiplier));
            Console.WriteLine(begin + "Вложенный блок TLV № " + (j + 1));
            Console.WriteLine(begin + "Тип: " + tlv.Type);
            Console.WriteLine(begin + "Длина: " + tlv.Length);
            if (tlv.SubTLVs.Count > 0)
            {
                Console.WriteLine(begin + "Вложения: " + tlv.SubTLVs.Count);
                for (var w = 0; w < tlv.SubTLVs.Count; w++)
                {
                    Output_Tlv_Inside(tlv.SubTLVs[w], w, multiplier + 2);
                }
            }
            else
            {
                Console.WriteLine(begin + "Объём контента (в байтах): " + tlv.Value.Length);
            }
            Console.WriteLine("");
        }
        #endregion
    }
}
