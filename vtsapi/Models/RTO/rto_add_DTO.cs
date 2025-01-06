using System.ComponentModel.DataAnnotations;

namespace vahangpsapi.Models.Backend
{
    public class rto_add_DTO
    {
      
        public string RTOCode { get; set; }
        public string RTOName { get; set; }        
        public string CreatedBy { get; set; }      
        public int IsDeleted { get; set; }
        public int pk_StateId { get; set; }
       

    }
}
