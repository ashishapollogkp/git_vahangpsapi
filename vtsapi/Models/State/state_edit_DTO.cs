using System.ComponentModel.DataAnnotations;

namespace vahangpsapi.Models.Backend
{
    public class state_edit_DTO
    {
        [Required]
        public int StateId { get; set; }
        [Required]
        public string StateName { get; set; }
        [Required]
        public string UpdatedBy { get; set; }
       
        //public int IsDeleted { get; set; }
    }
}
