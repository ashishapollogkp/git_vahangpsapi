using System.ComponentModel.DataAnnotations;

namespace vahangpsapi.Models.Backend
{
    public class backend_edit
    {
        [Required]
        public int BackendId { get; set; }
        [Required]
        public string BackendName { get; set; }
        [Required]
        public string UpdatedBy { get; set; }
       
        //public int IsDeleted { get; set; }
    }
}
