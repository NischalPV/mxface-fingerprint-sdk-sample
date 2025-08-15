namespace MxFace.Fingerprint.Aspx.Client.Models
{
    public class SearchResponse : BaseResult
    {
        public string ExternalId { get; set; }
        public int MatchingScore { get; set; }
    }
}
