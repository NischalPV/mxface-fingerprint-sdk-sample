using MxFace.Fingerprint.Aspx.Client.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Configuration;

namespace MxFace.Fingerprint.Aspx.Client.Services
{
    public class DeviceService
    {
        private readonly string _baseUrl = ConfigurationManager.AppSettings["ScanBaseUrl"].TrimEnd('/') + "/";

        public async Task<int> GetConnectedDevices(List<string> devices)
        {
            try
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
            }
            catch
            {
                // swallow exception and fall through to return 0
            }
            return 0;
        }

        public async Task<Device> GetDeviceInfoAsync(string deviceName)
        {
            try
            {
                var serializer = new JavaScriptSerializer();
                var content = serializer.Serialize(new { ConnectedDvc = deviceName });
                var response = await PostRequestAsync("info", content);
                if (response.Key >= 200 && response.Key <= 299)
                {
                    return serializer.Deserialize<Device>(response.Value);
                }
            }
            catch
            {
                // ignored - calling code will handle null result
            }
            return null;
        }

        private async Task<KeyValuePair<int, string>> PostRequestAsync(string endpoint, string json)
        {
            try
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
            catch (Exception ex)
            {
                return new KeyValuePair<int, string>(0, ex.Message);
            }
        }
    }
}
