using System.ComponentModel.DataAnnotations;

namespace MxFace.Fingerprint.Aspx.Client.Models
{
    public class EnrollRequest
    {
        [Required]
        public byte[] TemplateData { get; set; }

        [Required]
        public string PersonId { get; set; }

        [Required]
        public string Group { get; set; }
    }
}
