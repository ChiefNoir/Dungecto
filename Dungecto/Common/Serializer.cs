using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace Dungecto.Common
{
    /// <summary>XML serializer</summary>
    static class Serializer
    {
        /// <summary>Serialize T object to xml</summary>
        /// <typeparam name="T">Object type</typeparam>
        /// <param name="item">Object to serialize</param>
        /// <param name="filename">Filename</param>
        public static void ToXml<T>(T item, string filename)
        {
            if (item == null) { return; }
            if (string.IsNullOrWhiteSpace(filename)) { return; }

            var xmlSerializer = new XmlSerializer(typeof(T));
            using (var writer = new StreamWriter(filename, false, Encoding.Unicode))
            {
                xmlSerializer.Serialize(writer, item);
            }
        }

        /// <summary>Deserialize object from xml</summary>
        /// <typeparam name="T">Object type</typeparam>
        /// <param name="filename">Filename</param>
        /// <returns>Deserialized object</returns>
        public static T FromXml<T>(string filename)
        {
            if (!File.Exists(filename)) { return default(T); }

            var xmlSerializer = new XmlSerializer(typeof(T));
            using (var reader = new StreamReader(filename, Encoding.Unicode))
            {
                return (T)xmlSerializer.Deserialize(reader);
            }
        }
    }
}