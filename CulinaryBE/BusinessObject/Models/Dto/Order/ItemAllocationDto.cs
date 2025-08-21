namespace BusinessObject.Models.Dto
{
    public class ItemAllocationDto
    {
        public Guid ProductId { get; set; }
        public string? ProductName { get; set; }
        public int OrderedQty { get; set; }

        // Kho nào đã xuất (theo thứ tự gần→xa)
        public List<PerWarehousePickDto> Picks { get; set; } = new();
    }
}
