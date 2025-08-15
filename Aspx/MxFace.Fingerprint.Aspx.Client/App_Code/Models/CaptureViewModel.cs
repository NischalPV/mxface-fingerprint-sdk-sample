using System;

namespace MxFace.Fingerprint.Aspx.Client.Models
{
    public class CaptureViewModel
    {
        public string BitmapData { get; set; }
        public string ErrorCode { get; set; }
        public string ErrorDescription { get; set; }
        public int? Nfiq { get; set; }
        public int? Quality { get; set; }
        public string WSQInfo { get; set; }
        public byte[] Data
        {
            get { return string.IsNullOrEmpty(BitmapData) ? null : Convert.FromBase64String(BitmapData); }
        }
    }
}
