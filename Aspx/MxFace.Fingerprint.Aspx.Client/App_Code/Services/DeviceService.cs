using MxFace.Fingerprint.Aspx.Client.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace MxFace.Fingerprint.Aspx.Client.Services
{
    public class DeviceService
    {
        private readonly string _baseUrl = "https://localhost:8034/mfscan/";

        public async Task<int> GetConnectedDevices(List<string> devices)
        {
            var response = await PostRequestAsync("connecteddevicelist", null);
            if (response.Key >= 200 && response.Key <= 299)
            {
                var regex = new Regex("\\\"Connected Device :(.*?)\\\",");
                var match = regex.Match(response.Value);
                if (match.Success)
                    devices.Add(match.Groups[1].Value);
                return devices.Count;
            }
            return 0;
        }

        public async Task<Device> GetDeviceInfoAsync(string deviceName)
        {
            var serializer = new JavaScriptSerializer();
            var content = serializer.Serialize(new { ConnectedDvc = deviceName });
            var response = await PostRequestAsync("info", content);
            if (response.Key >= 200 && response.Key <= 299)
            {
                return serializer.Deserialize<Device>(response.Value);
            }
            return null;
        }

        private async Task<KeyValuePair<int, string>> PostRequestAsync(string endpoint, string json)
        {
            using (var client = new HttpClient())
            {
                var request = new HttpRequestMessage(HttpMethod.Post, _baseUrl + endpoint);
                if (json != null)
                    request.Content = new StringContent(json, Encoding.UTF8, "application/json");
                var resp = await client.SendAsync(request);
                var body = await resp.Content.ReadAsStringAsync();
                return new KeyValuePair<int, string>((int)resp.StatusCode, body);
            }
        }
    }
}
