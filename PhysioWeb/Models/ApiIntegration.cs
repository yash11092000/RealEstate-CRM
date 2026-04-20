namespace PhysioWeb.Models
{
    public class ApiIntegration : CommanProp
    {
        public List<DropDownSource> IntegrationType { get; set; }

        public int IntegrationTypeId { get; set; }

        public bool Status { get; set; }

        public bool IsEncrypt { get; set; }

        public string AppId { get; set; }

        public string AppSecret { get; set; }

        public string AccessToken { get; set; }

        public string PageAccessToken { get; set; }
        public string PageId { get; set; }
        public ApiIntegration()
        {
            IntegrationType = new List<DropDownSource>();
        }
    }
}
