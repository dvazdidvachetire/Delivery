using Delivery.Services;
using System;

namespace Delivery.Models
{
    public sealed class DeliveryLogWithIPAddress
    {
        public string IPAddress => IPAddressService.GetLocalIPAddress();
        public string Date => $"{DateTime.Now:yyyy-MM-dd HH:mm:ss}";
        public string Message { get; set; } = string.Empty;
    }
}
