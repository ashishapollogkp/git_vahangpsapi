namespace vahangpsapi.Models.Backend
{
    public class ManagerModel
    {
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
        public string ParentName { get; set; }
    }
}
