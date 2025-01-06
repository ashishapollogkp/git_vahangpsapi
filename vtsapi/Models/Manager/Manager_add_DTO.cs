using System.ComponentModel.DataAnnotations;

namespace vahangpsapi.Models.Backend
{
    public class Manager_add_DTO
    {
        [Required]
        public string ManagerName { get; set; }
        [Required]
        public string MobileNo { get; set; }
        [Required]
        public string Designation { get; set; }    
        public string? CreatedBy { get; set; }       
        public int IsDeleted { get; set; }
        public int pk_ParentId { get; set; }

    }
}
