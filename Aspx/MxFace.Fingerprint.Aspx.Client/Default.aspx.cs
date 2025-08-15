using System;
using System.Net.Http;
using MxFace.Fingerprint.Aspx.Client.Services;

public partial class Default : System.Web.UI.Page
{
    private static readonly HttpClient httpClient = new HttpClient { BaseAddress = new Uri("https://localhost:7103/") };
    private static readonly FingerprintApiService apiService = new FingerprintApiService(httpClient);

    protected void Enroll_Click(object sender, EventArgs e)
    {
        var template = Convert.FromBase64String(hfTemplate.Value);
        var result = apiService.EnrollAsync(template, txtPersonId.Text, txtGroup.Text).Result;
        ltResult.Text = "Enroll: " + result?.Code + " - " + result?.Message;
    }

    protected void Search_Click(object sender, EventArgs e)
    {
        var template = Convert.FromBase64String(hfTemplate.Value);
        var result = apiService.SearchAsync(template, txtGroup.Text).Result;
        ltResult.Text = "Search matches: " + (result != null ? result.Count.ToString() : "0");
    }

    protected void Verify_Click(object sender, EventArgs e)
    {
        var template = Convert.FromBase64String(hfTemplate.Value);
        var result = apiService.VerifyAsync(template, template).Result;
        ltResult.Text = "Verify score: " + result?.Score;
    }
}
