using Newtonsoft.Json;

namespace UI.Utilities
{
    ///<summary>
    /// Class to hold Environment related settings 
    ///</summary>
    public class EnvironmentConfigSettings : SystemConfigSettings
    {
        public string Environment { get; set; }
        public string ConnectionString { get; set; }
        public string UserURL { get; set; }
        public string UserPassword { get; set; }
        public string ApiUrl { get; set; }
        public string SoapApiUrl { get; set; }
        public int DefaultElementWait { get; set; }
        public string OneMinuteElementWait { get; set; }
        public string VideoUrl { get; set; }
        public string AdminUrl { get; set; }
        public string ServiceUrl { get; set; }
    }

    public class SystemConfigSettings
    {
        public string ReportLocation { get; set; }
        public string ImageLocation { get; set; }
        public bool RunOnSaucelabs { get; set; }
        public int PipelineElementWait { get; set; }
        public int SaucelabsElementWait { get; set; }
        public SauceLabsConfiguration SauceLabsConfiguration { get; set; }
    }

    public class SauceLabsConfiguration
    {
        public string PlatformName { get; set; }
        public string BrowserName { get; set; }
        public string DeviceName { get; set; }
        public string PlatformVersion { get; set; }
        public string AppiumVersion { get; set; }
        public string SauceUsername { get; set; }
        public string SauceAccessKey { get; set; }
        public string SauceUrl { get; set; }
        public string Orientation { get; set; }
    }
}
