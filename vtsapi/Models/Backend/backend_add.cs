using System.ComponentModel.DataAnnotations;

namespace vahangpsapi.Models.Backend
{
    public class backend_add
    {
        [Required]
        public string BackendName { get; set; }
        [Required]
        public string CreatedBy { get; set; }

    }
}
