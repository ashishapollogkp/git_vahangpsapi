using System.ComponentModel.DataAnnotations;

namespace vahangpsapi.Data
{
    public class AssetType
    {
        [Key]
        public int AssetId { get; set; }
        public string AssetName { get; set; }
        public string? AssetCode { get; set; }
    }
}
