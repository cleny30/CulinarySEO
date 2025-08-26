namespace BusinessObject.Models.Dto
{
    public class PerWarehousePickDto
    {
        public Guid WarehouseId { get; set; }
        public string? WarehouseName { get; set; }
        public int PickedQty { get; set; }
        public double DistanceKm { get; set; }
    }
}
