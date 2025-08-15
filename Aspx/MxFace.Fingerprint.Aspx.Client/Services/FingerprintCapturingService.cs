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
    public class FingerprintCapturingService
    {
        private readonly string _baseUrl = ConfigurationManager.AppSettings["ScanBaseUrl"];

        public async Task<CaptureViewModel> StartCaptureAsync(int timeout = 10, int minimumQuality = 60)
        {
            var serializer = new JavaScriptSerializer();
            var content = serializer.Serialize(new { Quality = minimumQuality, TimeOut = timeout });
            var response = await PostRequestAsync("capture", content);
            if (response.Key >= 200 && response.Key <= 299)
            {
                return serializer.Deserialize<CaptureViewModel>(response.Value);
            }
            return null;
        }

        private async Task<KeyValuePair<int, string>> PostRequestAsync(string endpoint, string json)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var request = new HttpRequestMessage(HttpMethod.Post, _baseUrl + "/" + endpoint);
                    if (json != null)
                        request.Content = new StringContent(json, Encoding.UTF8, "application/json");
                    var resp = await client.SendAsync(request);
                    var body = await resp.Content.ReadAsStringAsync();
                    return new KeyValuePair<int, string>((int)resp.StatusCode, body);
                }
            }
            catch (Exception ex)
            {
                return new KeyValuePair<int, string>(0, ex.Message);
            }
        }
    }
}
