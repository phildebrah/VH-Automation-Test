using Newtonsoft.Json;

namespace Utilities
{
    public class EnvironmentConfigSettings:SystemConfigSettings
    {
        public string Environment { get; set; }
        public string _authority { get; set; }
        public string bookingsapi { get; set; }
        public string usersapi { get; set; }
        public string videoapi { get; set; }
        public string UserPassword { get; set; }
        public string clientid { get; set; }
        public string _clientSecret { get; set; }
        public string _tenetid { get; set; }
        public string bookingsapiResourceId { get; set; }
        public string userapiResourceId { get; set; }
        public string videoapiResourceId { get; set; }
    }

    public class SystemConfigSettings
    {
        public string ReportLocation { get; set; }
        public string ImageLocation { get; set; }
        public int PipelineElementWait { get; set; }
    }

 
}
