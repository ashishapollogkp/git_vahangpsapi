using System.ComponentModel.DataAnnotations;

namespace vahangpsapi.Data
{
    public class product_master
    {
        [Key]
        public int ProductId { get; set; }
        public string Product_Name { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string? UpdatedBy { get; set; }
        public int Deleted { get; set; }
    }
}
