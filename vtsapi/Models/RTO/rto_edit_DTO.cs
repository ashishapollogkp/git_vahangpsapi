using System.ComponentModel.DataAnnotations;

namespace vahangpsapi.Models.Backend
{
    public class rto_edit_DTO
    {
        public int RTOId { get; set; }
        public string RTOCode { get; set; }
        public string RTOName { get; set; }  
        public string UpdatedBy { get; set; }
        public int IsDeleted { get; set; }
        public int pk_StateId { get; set; }
      
    }
}
