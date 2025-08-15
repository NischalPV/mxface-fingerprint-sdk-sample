using MxFace.Fingerprint.Aspx.Client.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Configuration;

namespace MxFace.Fingerprint.Aspx.Client.Services
{
    public class FingerprintApiService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;

        public FingerprintApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _baseUrl = ConfigurationManager.AppSettings["FingerprintApiPath"];
        }

        public async Task<EnrollmentResponse> EnrollAsync(byte[] templateData, string personId, string group)
        {
            try
            {
                var serializer = new JavaScriptSerializer();
                var request = new EnrollRequest { TemplateData = templateData, PersonId = personId, Group = group };
                var content = new StringContent(serializer.Serialize(request), Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync(_baseUrl + "/enroll", content);
                var body = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    return serializer.Deserialize<EnrollmentResponse>(body);
                }
                return new EnrollmentResponse { Code = (int)response.StatusCode, Message = body };
            }
            catch (Exception ex)
            {
                return new EnrollmentResponse { Code = 0, Message = ex.Message };
            }
        }

        public async Task<List<SearchResponse>> SearchAsync(byte[] templateData, string group)
        {
            try
            {
                var serializer = new JavaScriptSerializer();
                var request = new SearchRequest { TemplateData = templateData, Group = group };
                var content = new StringContent(serializer.Serialize(request), Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync(_baseUrl + "/search", content);
                var body = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    return serializer.Deserialize<List<SearchResponse>>(body);
                }
                return new List<SearchResponse> { new SearchResponse { Code = (int)response.StatusCode, Message = body } };
            }
            catch (Exception ex)
            {
                return new List<SearchResponse> { new SearchResponse { Code = 0, Message = ex.Message } };
            }
        }

        public async Task<MatchResponse> VerifyAsync(byte[] sourceTemplate, byte[] targetTemplate)
        {
            try
            {
                var serializer = new JavaScriptSerializer();
                var request = new MatchRequest { SourceTemplate = sourceTemplate, TargetTemplate = targetTemplate };
                var content = new StringContent(serializer.Serialize(request), Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync(_baseUrl + "/verify", content);
                var body = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    return serializer.Deserialize<MatchResponse>(body);
                }
                return new MatchResponse { Code = (int)response.StatusCode, Message = body };
            }
            catch (Exception ex)
            {
                return new MatchResponse { Code = 0, Message = ex.Message };
            }
        }
    }
}
