namespace vahangpsapi.Models
{
    public class UserModel
    {
        public int EmpId { get; set; }
        public string Name { get; set; } 
        public string? Email { get; set; }
        public string Role { get; set; }
        public int DeptId { get; set; }
        public string UserName { get; set; }
        public string Result { get; set; }
        public string HomePage { get; set; }
        public string UserType { get; set; }
    }
}