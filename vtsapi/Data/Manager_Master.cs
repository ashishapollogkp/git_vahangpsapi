using System.ComponentModel.DataAnnotations;

namespace vahangpsapi.Data
{
    public class Manager_Master
    {
        [Key]
        public int ManagerId { get; set; }
        public string ManagerName { get; set; }
        public string MobileNo { get; set; }
        public string Designation { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string? UpdatedBy { get; set; }
        public int IsDeleted { get; set; }
        public int pk_ParentId { get; set; }
    }
}
