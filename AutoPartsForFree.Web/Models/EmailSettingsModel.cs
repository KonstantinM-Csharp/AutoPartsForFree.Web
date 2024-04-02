namespace AutoPartsForFree.Web.Models
{
    public class EmailSettingsModel
    {
        public string Host { get; set; }
        public int Port { get; set; }
        public bool UseSsl { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string DownloadFolder { get; set; }
        public string SenderAddress { get; set; }
    }
}
