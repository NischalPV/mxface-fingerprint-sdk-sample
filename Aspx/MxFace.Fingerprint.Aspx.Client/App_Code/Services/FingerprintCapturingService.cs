using MxFace.Fingerprint.Aspx.Client.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace MxFace.Fingerprint.Aspx.Client.Services
{
    public class FingerprintCapturingService
    {
        private readonly string _baseUrl = "https://localhost:8034/mfscan";

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
    }
}
