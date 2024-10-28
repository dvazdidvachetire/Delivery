using System.Xml.Serialization;

namespace Delivery.Models
{
    [XmlRoot(ElementName = "order")]
    public sealed class XMLDeliveryOrder
    {
        [XmlElement(ElementName = "number_order")] public int Number { get; set; }
        [XmlElement(ElementName = "weight_order")] public double Weight { get; set; }
        [XmlElement(ElementName = "region_order")] public string Region { get; set; } = string.Empty;
        [XmlElement(ElementName = "date_order")] public string Date { get; set; }
    }
}
