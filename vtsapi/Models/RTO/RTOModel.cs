namespace vahangpsapi.Models.Backend
{
    public class RTOModel
    {
        public int RTOId { get; set; }
        public string RTOCode { get; set; }
        public string RTOName { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public int IsDeleted { get; set; }
        public int pk_StateId { get; set; }
        public string pk_StateName { get;set; }
    }
}
