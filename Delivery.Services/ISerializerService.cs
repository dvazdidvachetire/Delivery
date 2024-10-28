namespace Delivery.Services
{
    public interface ISerializerService
    {
        object? DeserializeObject { get; set; }
        void Serialize(object value, Stream stream);
        void Deserialize<T>(Stream stream, T type = default!);
    }
}
