namespace vahangpsapi.Models.Backend
{
    public class backendModel
    {
        public int BackendId { get; set; }
        public string BackendName { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string? UpdatedBy { get; set; }
        public int IsDeleted { get; set; }
    }
}
