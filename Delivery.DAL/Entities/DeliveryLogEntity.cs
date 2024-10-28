namespace Delivery.DAL.Entities
{
    public sealed class DeliveryLogEntity : BaseEntity
    {
        public string IPAddress { get; set; } = string.Empty;
        public string Date { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
    }
}
