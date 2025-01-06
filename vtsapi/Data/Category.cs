using System.ComponentModel.DataAnnotations;

namespace vahangpsapi.Data
{
    public class Category
    {
        [Key]
        public int CatId { get; set; }
        public string CatName { get; set; }
        public int ParentCatId { get; set; }
        public int CatStatus { get; set; }
        public int DeptId { get; set; }
    }
}
