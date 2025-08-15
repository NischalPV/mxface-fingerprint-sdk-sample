using System.ComponentModel.DataAnnotations;

namespace MxFace.Fingerprint.Aspx.Client.Models
{
    public class SearchRequest
    {
        [Required]
        public byte[] TemplateData { get; set; }

        [Required]
        public string Group { get; set; }
    }
}
