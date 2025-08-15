using System;
using System.Net.Http;
using System.Linq;
using System.Configuration;
using MxFace.Fingerprint.Aspx.Client.Services;

namespace MxFace.Fingerprint.Aspx.Client.Pages
{
    public partial class Default : System.Web.UI.Page
    {
        private static readonly HttpClient httpClient = new HttpClient { BaseAddress = new Uri(ConfigurationManager.AppSettings["ApiBaseUrl"]) };
        private static readonly FingerprintApiService apiService = new FingerprintApiService(httpClient);

        protected void Enroll_Click(object sender, EventArgs e)
        {
            try
            {
                var template = Convert.FromBase64String(hfTemplate1.Value);
                var result = apiService.EnrollAsync(template, txtPersonId.Text, txtGroup.Text).Result;
                lblEnrollCode.Text = result?.Code?.ToString();
                lblEnrollMsg.Text = result?.Message;
            }
            catch (Exception ex)
            {
                lblEnrollCode.Text = "0";
                lblEnrollMsg.Text = ex.Message;
            }
        }

        protected void Search_Click(object sender, EventArgs e)
        {
            try
            {
                var template = Convert.FromBase64String(hfTemplate1.Value);
                var result = apiService.SearchAsync(template, txtGroup.Text).Result;
                var first = result?.FirstOrDefault();
                if (first != null && first.Code.GetValueOrDefault() >= 200)
                {
                    txtSearchScore.Text = first.MatchingScore.ToString();
                    txtSearchImage.Text = hfTemplate1.Value;
                }
                else
                {
                    txtSearchScore.Text = first?.Message;
                    txtSearchImage.Text = string.Empty;
                }
            }
            catch (Exception ex)
            {
                txtSearchScore.Text = ex.Message;
                txtSearchImage.Text = string.Empty;
            }
        }

        protected void Verify_Click(object sender, EventArgs e)
        {
            try
            {
                var source = Convert.FromBase64String(hfTemplate1.Value);
                var target = Convert.FromBase64String(hfTemplate2.Value);
                var result = apiService.VerifyAsync(source, target).Result;
                txtMatchScore.Text = result?.Score.ToString();
            }
            catch (Exception ex)
            {
                txtMatchScore.Text = ex.Message;
            }
        }
    }
}
