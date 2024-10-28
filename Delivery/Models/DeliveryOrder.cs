using System;

namespace Delivery.Models
{
    public sealed class DeliveryOrder
    {
        public int Number { get; set; }
        public double Weight { get; set; }
        public string Region { get; set; } = string.Empty;
        public DateTime Date { get; set; }
    }
}
