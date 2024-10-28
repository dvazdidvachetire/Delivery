namespace Delivery.DAL.Entities
{
    public sealed class DeliveryOrderEntity : BaseEntity
    {
        public int Number { get; set; }
        public double Weight { get; set; }
        public string Region { get; set; } = string.Empty;
        public string Date { get; set; } = string.Empty;
    }
}
