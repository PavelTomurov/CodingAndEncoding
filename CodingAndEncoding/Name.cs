using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CodingAndEncoding
{
    /// <summary>
    /// Именной компонент
    /// </summary>
    public static class Name
    {
        /// <summary>
        /// Конструктор подэлементов именного компонента пакета интереса
        /// </summary>
        public static List<TLV> GetNameElements(string full_name)
        {
            List<string> path_elements = full_name.Split('\\').ToList();
            List<TLV> name_tlvs = new List<TLV>();
            foreach (var p in path_elements)
                name_tlvs.Add(CreatorHelper.Create_TLV(8, p));
            return name_tlvs;
        }

        /// <summary>
        /// Получить длину в байтах у имени (совокупность всех именных элементов типа 8) блока TLV
        /// </summary>
        public static int GetNameLength(TLV tlv)
        {
            List<byte[]> list_bytes_to_hash = new List<byte[]>();
            list_bytes_to_hash = tlv.SubTLVs.First().SubTLVs.Where(v => v.Value.Length != 0).Select(v => v.Value).ToList();
            List<byte> bytes_to_hash = new List<byte>();
            foreach (var b in list_bytes_to_hash)
                bytes_to_hash.AddRange(b.ToArray());
            return bytes_to_hash.Count;
        }
        
    }
}
