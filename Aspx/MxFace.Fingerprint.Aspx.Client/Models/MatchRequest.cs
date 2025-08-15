using System.ComponentModel.DataAnnotations;

namespace MxFace.Fingerprint.Aspx.Client.Models
{
    public class MatchRequest
    {
        [Required]
        public byte[] SourceTemplate { get; set; }

        [Required]
        public byte[] TargetTemplate { get; set; }
    }
}
