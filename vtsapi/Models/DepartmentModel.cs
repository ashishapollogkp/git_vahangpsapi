namespace vahangpsapi.Models
{
    public class DepartmentModel
    { 
        public long DeptId { get; set; }
        public string DeptCode { get; set; }
        public string DeptName { get; set; }
        public long ParentDeptId { get; set; }
        public int DeptStatus { get; set; }
        public int DeptType { get; set; }
    }
}
