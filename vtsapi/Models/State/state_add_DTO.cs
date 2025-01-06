using System.ComponentModel.DataAnnotations;

namespace vahangpsapi.Models.Backend
{
    public class state_add_DTO
    {
        [Required]
        public string StateName { get; set; }
        [Required]
        public string CreatedBy { get; set; }

    }
}
