using System.Xml.Serialization;

namespace Delivery.Models
{
    [XmlRoot(ElementName = "orders")]
    public sealed class DeliveryOrders
    {
        [XmlElement(ElementName = "order")] public XMLDeliveryOrder[] OrdersArray { get; set; } = [];
    }
}
