namespace vahangpsapi.Models
{
    public class VisitorModel 
    {
        public long VisitorId { get; set; }
        public int DeptId { get; set; }
        public int CatId { get; set; }
        public string FirstName { get; set; }
        public string? LastName { get; set; }
        public string Address1 { get; set; }
        public string? Address2 { get; set; }
        public string? Address3 { get; set; }
        public string? PhotoId { get; set; }
        public byte[]? PhotoData { get; set; }  
        public string? ContactNo { get; set; }
        public string? Email { get; set; }
        public int? EmpIdToMeet { get; set; }
        public string? EmpNameToMeet { get; set; }
        public string? IMEINo { get; set; }
        public string? RFId { get; set; }
        public int? IdTYPE1 { get; set; }
        public string? IdNumber1 { get; set; }
        public string? IdDoc1 { get; set; }
        public byte[]? IdDocData1 { get; set; }  
        public int? IdTYPE2 { get; set; }
        public string? IdNumber2 { get; set; }
        public string? IdDoc2 { get; set; }
        public byte[]? IdDocData2 { get; set; }
        public DateTime? ValidFrom { get; set; }
        public DateTime? ValidTill { get; set; }
        public string? VisitRemark { get; set; }
        public long? CreatedBy { get; set; }
        public string? DeptName { get; set; }
        public string? CatName { get; set; }
        public int? VisitorPurposeId { get; set; }
        public string? VisitorPurposeText { get; set; } 
        public int? IsVisitComplete { get; set;}
    }
}
