namespace BusinessObject.Models.Dto
{
    public class GetWarehouse
    {
        public Guid WarehouseId { get; set; }
        public string WarehouseName { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
    }
}
