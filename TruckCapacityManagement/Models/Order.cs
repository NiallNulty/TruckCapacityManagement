namespace TruckCapacityManagement.Models
{
    public class Order
    {
        public int Customer { get; set; }
        public int OrderNumber { get; set; }
        public string ProductCode { get; set; }
        public int OrderQty { get; set; }
        public int ScaledOrderQty { get; set; }
        public int MinimumScaledOrderQtyAllowed { get; set; }
        public int DeliveryQty { get; set; }
        public int RemainingQty { get; set; }
    }
}
