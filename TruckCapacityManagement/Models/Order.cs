namespace TruckCapacityManagement.Models
{
    public class Order
    {
        public int Customer { get; set; }
        public int OrderNumber { get; set; }
        public string ProductCode { get; set; }
        public int OrderQty { get; set; }
    }
}
