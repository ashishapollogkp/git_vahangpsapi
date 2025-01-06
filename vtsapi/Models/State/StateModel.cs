namespace vahangpsapi.Models.Backend
{
    public class StateModel
    {
        public int StateId { get; set; }
        public string StateName { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string? UpdatedBy { get; set; }
        public int IsDeleted { get; set; }
    }
}
