namespace vahangpsapi.Models
{
    public class RoleModel
    {
        public int RoleId { get; set; }
        public string RoleShortName { get; set; }
        public string RoleName { get; set; }
        public int DeptId { get; set; }
        public int ParentId { get; set; }
        public int LevelId { get; set; }
        public int StaffUser { get; set; }
        public int DriverUser { get; set; }
        public string DefaultPageApp { get; set; }
        public int NoOfLoginOnOneVehicle { get; set; }
        public int NoLoginOnApp { get; set; }
        public int NoLoginOnWeb { get; set; }
        public int NoLoginOnWindows { get; set; }
        public string DefaultMenus { get; set; }
        public int LoginOnVehicle { get; set; }
        public int LoginOnLocation { get; set; }
        public int ImageOnLogin { get; set; }
        public int ImageOnLogout { get; set; }
        public int ImageOnVisit { get; set; }
        public int TimerLocationData { get; set; }
        public string DefaultPageWeb { get; set; }
    }
}
