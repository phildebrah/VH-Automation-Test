using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Linq;

namespace Utilities
{
    public class TestConfigHelper
    {
        private static NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();
        private static IConfigurationRoot _configRoot;

        public TestConfigHelper()
        {
            _configRoot = BuildConfig("CA353381-2F0D-47D7-A97B-79A30AFF8B86", "18c466fd-9265-425f-964e-5989181743a7");
        }
            public static IConfigurationRoot GetIConfigurationBase()
        {
            return new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: true)
            .AddJsonFile("passwords.json",optional:true)
            .AddEnvironmentVariables()
            .Build();
        }

        public static IConfigurationRoot BuildConfig(string userSecretsId, string testSecretsId)
        {
            var testConfigBuilder = new ConfigurationBuilder()
                .AddUserSecrets(testSecretsId)
                .Build();

            return new ConfigurationBuilder()
                .AddJsonFile($"appsettings.json")
                .AddJsonFile($"appsettings.Development.json", optional: true)
                .AddJsonFile($"appsettings.Production.json", optional: true)
                .AddUserSecrets(userSecretsId)
                .AddConfiguration(testConfigBuilder)
                .Build();
        }

        public static EnvironmentConfigSettings GetApplicationConfiguration()
        {
         //   _configRoot = BuildConfig("CA353381-2F0D-47D7-A97B-79A30AFF8B86", "18c466fd-9265-425f-964e-5989181743a7");
            EnvironmentConfigSettings configSettings=null;
            LaunchSettingsFixture();
            var environment = Environment.GetEnvironmentVariable("ENVIRONMENT");
            var systemConfiguration = new SystemConfiguration();
            var iTestConfigurationRoot = BuildConfig("CA353381-2F0D-47D7-A97B-79A30AFF8B86", "18c466fd-9265-425f-964e-5989181743a7");
            Logger.Info($"test config {iTestConfigurationRoot.GetDebugView}");
            //var iTestConfigurationRoot = GetIConfigurationBase();
            Logger.Info("Reading Appsetitngs Json File");
            iTestConfigurationRoot.GetSection("SystemConfiguration").Bind(systemConfiguration);
            if (environment != null)
            {
                if (environment.ToLower() == "Development".ToLower())
                {
                    configSettings=systemConfiguration.DevelopmentEnvironmentConfigSettings;
                }
                else if (environment.ToLower() == "Acceptance".ToLower())
                {
                    configSettings=systemConfiguration.AcceptanceEnvironmentConfigSettings;
                }
                else if (environment.ToLower() == "Production".ToLower())
                {
                    configSettings=systemConfiguration.ProductionEnvironmentConfigSettings;
                }
            }
            Logger.Info($"iTestConfigurationRoot = {iTestConfigurationRoot.ToString()}");
            Logger.Info($"iTestConfigurationRootGetSection('AzureAd') = {iTestConfigurationRoot.GetSection("AzureAd").ToString()}");
            Logger.Info($"iTestConfigurationRootGetSection('AzureAd') = {iTestConfigurationRoot.GetSection("AzureAd").Get<AzureAdConfiguration>()}");
            AzureAdConfiguration azureAdConfiguration = iTestConfigurationRoot.GetSection("AzureAd").Get<AzureAdConfiguration>();
            configSettings.clientid = azureAdConfiguration.ClientId;
            configSettings._clientSecret = azureAdConfiguration.ClientSecret;
            configSettings._tenetid = azureAdConfiguration.TenantId;
            //var a = Options.Create(_configRoot.GetSection("VhServices").Get<VHServices>());
            VHServices vHServices = iTestConfigurationRoot.GetSection("VhServices").Get<VHServices>();
            Logger.Info($"Vhservices: BookingsApiResourceId = {vHServices.BookingsApiResourceId}, BookingsApiUrl = {vHServices.BookingsApiUrl}, UserApiResourceId = {vHServices.UserApiResourceId}, UserApiUrl = {vHServices.UserApiUrl}, VideoApiResourceId = {vHServices.VideoApiResourceId}, {vHServices.VideoApiUrl}, VideoWebUrl = {vHServices.VideoWebUrl}, VideoWebResourceId = {vHServices.VideoWebResourceId}");
            configSettings.bookingsapi = vHServices.BookingsApiUrl;
            configSettings.bookingsapiResourceId = vHServices.BookingsApiResourceId;
            configSettings.userapiResourceId = vHServices.UserApiResourceId;
            configSettings.usersapi = vHServices.UserApiUrl;
            configSettings.videoapi = vHServices.VideoApiUrl;
            configSettings.videoapiResourceId = vHServices.VideoApiResourceId;
            //       var featureToggle = new FeatureToggles(_configRoot.GetSection("FeatureToggle"));
            //context.WebConfig.BookingConfirmToggle = featureToggle.BookAndConfirmToggle();
            return configSettings;
        }

        public static void LaunchSettingsFixture()
        {
            var cs = AppDomain.CurrentDomain.BaseDirectory.ToString();
            using (var file = File.OpenText(@"Properties/launchSettings.json"))
            {
                var reader = new JsonTextReader(file);
                var jObject = JObject.Load(reader);
                var variables = jObject
                    .GetValue("profiles")
                    //select a proper profile here
                    .SelectMany(profiles => profiles.Children())
                    .SelectMany(profile => profile.Children<JProperty>())
                    .Where(prop => prop.Name == "environmentVariables")
                    .SelectMany(prop => prop.Value.Children<JProperty>())
                    .ToList();
                foreach (var variable in variables)
                {
                    Environment.SetEnvironmentVariable(variable.Name, variable.Value.ToString());
                }
            }
        }
    }
}
