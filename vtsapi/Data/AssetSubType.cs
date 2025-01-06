using System.ComponentModel.DataAnnotations;

namespace vahangpsapi.Data
{
    public class AssetSubType
    {
        [Key]
        public long AssetSubId { get; set; }
        public string AssetSubName { get; set; }
        public string? AssetSubCode { get; set; }
        public int AssetId { get; set; }
    }
}
