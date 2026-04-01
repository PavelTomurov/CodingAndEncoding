using System;
using System.Collections.Generic;
using System.Linq;

namespace CodingAndEncoding
{
    /// <summary>
    /// Класс TLV-формата
    /// </summary>
    [Serializable]
    public class TLV : IComparable<TLV>
    {
        /// <summary>
        /// Тип контента
        /// </summary>
        public object Type { get; set; }

        /// <summary>
        /// Длина контента
        /// </summary>
        public object Length { get; set; }

        /// <summary>
        /// Контент
        /// </summary>
        public byte[] Value { get; set; }

        /// <summary>
        /// Вложенные блоки TLV
        /// </summary>
        public List<TLV> SubTLVs { get; set; }

        /// <summary>
        /// Конструктор TLV
        /// </summary>
        public TLV(object type, object length, byte[] value)
        {
            this.Type = type;
            this.Length = length;
            this.Value = value;
            this.SubTLVs = new List<TLV>();
        }

        /// <summary>
        /// Конструктор TLV
        /// </summary>
        public TLV(object type, object length, List<TLV> subTLVs)
        {
            this.Type = type;
            this.Length = length;
            this.Value = new byte[0];
            this.SubTLVs = subTLVs;
        }

        /// <summary>
        /// Метод сравнения блоков TLV по имени (по закодированным именным элементам)
        /// Используется для построения дерева
        /// Используется для поиска в дереве
        /// </summary>
        int IComparable<TLV>.CompareTo(TLV other)
        {
            int result;
            var stackTrace = new System.Diagnostics.StackTrace();
            var method = stackTrace.GetFrames().Take(stackTrace.GetFrames().Length - 1).LastOrDefault().GetMethod();
            //var caller = method.DeclaringType.Name;
            //var caller = new System.Diagnostics.StackTrace().ToString();
            result = Name.GetNameLength(this).CompareTo(Name.GetNameLength(other));
            if (!method.Name.Contains("Contains") && result == 0)
                result = ComparePoint(other);
            return result;
        }

        int ComparePoint(TLV other)
        {
            return Convert.ToInt32(this.Length).CompareTo(Convert.ToInt32(other.Length));
        }


    }

}
