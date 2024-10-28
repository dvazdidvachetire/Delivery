using System.Diagnostics;
using System.Xml.Serialization;

namespace Delivery.Services
{
    public class XMLSerializerService : ISerializerService
    {
        public object? DeserializeObject { get; set; }

        public void Deserialize<T>(Stream stream, T type = default!)
        {
            try
            {
                var serializer = new XmlSerializer(typeof(T));
                DeserializeObject = serializer.Deserialize(stream);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        public void Serialize(object value, Stream stream) {  }
    }
}
