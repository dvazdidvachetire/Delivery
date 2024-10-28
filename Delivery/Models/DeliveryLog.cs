using System;

namespace Delivery.Models
{
    public sealed class DeliveryLog
    {
        public string IPAddress { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public string Message { get; set; } = string.Empty;
    }
}
