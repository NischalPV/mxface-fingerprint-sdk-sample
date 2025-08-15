using MxFace.Fingerprint.Aspx.Client.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace MxFace.Fingerprint.Aspx.Client.Services
{
    public class FingerprintApiService
    {
        private readonly HttpClient _httpClient;
        private const string BaseUrl = "api/Fingerprint";

        public FingerprintApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<EnrollmentResponse> EnrollAsync(byte[] templateData, string personId, string group)
        {
            var serializer = new JavaScriptSerializer();
            var request = new EnrollRequest { TemplateData = templateData, PersonId = personId, Group = group };
            var content = new StringContent(serializer.Serialize(request), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(BaseUrl + "/enroll", content);
            var body = await response.Content.ReadAsStringAsync();
            return serializer.Deserialize<EnrollmentResponse>(body);
        }

        public async Task<List<SearchResponse>> SearchAsync(byte[] templateData, string group)
        {
            var serializer = new JavaScriptSerializer();
            var request = new SearchRequest { TemplateData = templateData, Group = group };
            var content = new StringContent(serializer.Serialize(request), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(BaseUrl + "/search", content);
            var body = await response.Content.ReadAsStringAsync();
            return serializer.Deserialize<List<SearchResponse>>(body);
        }

        public async Task<MatchResponse> VerifyAsync(byte[] sourceTemplate, byte[] targetTemplate)
        {
            var serializer = new JavaScriptSerializer();
            var request = new MatchRequest { SourceTemplate = sourceTemplate, TargetTemplate = targetTemplate };
            var content = new StringContent(serializer.Serialize(request), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(BaseUrl + "/verify", content);
            var body = await response.Content.ReadAsStringAsync();
            return serializer.Deserialize<MatchResponse>(body);
        }
    }
}
