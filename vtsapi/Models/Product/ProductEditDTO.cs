namespace vahangpsapi.Models.Product
{
    public class ProductEditDTO
    {
        public int ProductId { get; set; }
        public string Product_Name { get; set; }
        public string? Description { get; set; }
        public string? UpdatedBy { get; set; }
        public int Deleted { get; set; }
    }
}
