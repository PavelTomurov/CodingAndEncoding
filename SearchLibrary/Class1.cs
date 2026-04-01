using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace TLVLibrary
{
    /// <summary>
    /// Класс TLV-формата
    /// </summary>
    public class TLV
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
    }
}
