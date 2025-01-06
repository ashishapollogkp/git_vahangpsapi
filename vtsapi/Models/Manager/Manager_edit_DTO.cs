using System.ComponentModel.DataAnnotations;

namespace vahangpsapi.Models.Backend
{
    public class Manager_edit_DTO
    {
        public int ManagerId { get; set; }
        public string ManagerName { get; set; }
        public string MobileNo { get; set; }
        public string Designation { get; set; }        
       
        public string? UpdatedBy { get; set; }
        public int IsDeleted { get; set; }
        public int pk_ParentId { get; set; }
        
    }
}
