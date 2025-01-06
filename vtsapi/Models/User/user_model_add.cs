namespace vahangpsapi.Models.User
{
    public class user_model_add
    {
        public int EmpId { get; set; }
        public int RoleId { get; set; }
        public string UserName { get; set; }
        public string? EmpPassword { get; set; }        
        public string FirstName { get; set; }
        public string? LastName { get; set; }        
        public string? Contact { get; set; }        
        public int EmpStatus { get; set; }
        public string? Email { get; set; }
       
        
    }
}
