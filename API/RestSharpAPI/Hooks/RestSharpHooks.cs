using AventStack.ExtentReports;
using AventStack.ExtentReports.Gherkin.Model;
using AventStack.ExtentReports.Reporter;
using Microsoft.Extensions.Configuration;
using NLog;
using NLog.Web;
using RestSharp;
using System;
using System.IO;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Infrastructure;
using TestFramework;
using TestLibrary.Utilities;
using Utilities;

namespace RestSharpApi.Hooks
{
	///<summary>
	/// A binding class to support running Specflow features within an Nunit test framework
	/// This framework incorporates logging and reporting
	/// As the API calls are to an Azure web service, this framework supports Azure authentication
	///<Summary>
    [Binding]
    public  class RestSharpHooks 
    {
        public IConfiguration Configuration { get; }
        public static EnvironmentConfigSettings config;
        public static RestClient _restClient;
        public static string ProjectPath = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;
        public static string PathReport;
        private static ExtentTest _feature;
        private static ExtentTest _scenario;
        private static ExtentReports _extent;
        public static Logger _logger = NLog.LogManager.GetCurrentClassLogger();


        [BeforeTestRun]
        public static void BeforeTestRun()
        {
            try
            {
                config = TestConfigHelper.GetApplicationConfiguration();

                _logger.Info("Automation Test Execution Commenced");
                _logger.Info($"Extent reports settings basepath: {Directory.GetCurrentDirectory}, projectpath: {ProjectPath}");
                var logFilePath = Util.GetLogFileName("logfile");
                var logFileName = Path.GetFileNameWithoutExtension(logFilePath);
                var folderName = logFileName.Replace(":", ".");
                PathReport = Path.Combine(ProjectPath + config.ReportLocation, folderName, "ExtentReport.html");
                _logger.Info($"Extent Reports path: {PathReport}");
                var reporter = new ExtentHtmlReporter(PathReport);
                _extent = new ExtentReports();
                _extent.AttachReporter(reporter);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "An error has occured before Automation Test Execution ");
            }
        }

        [BeforeFeature]
        public static void BeforeFeature(FeatureContext featureContext, ISpecFlowOutputHelper outputHelper)
        {
            var featureTitle = featureContext.FeatureInfo.Title;
            _feature = _extent.CreateTest<Feature>(featureTitle);
            _logger.Info($"Starting feature '{featureTitle}'");
        }

        [BeforeScenario]
        public void BeforeScenario(ScenarioContext scenarioContext)
        {
            var scenarioTitle = scenarioContext.ScenarioInfo.Title;
            _logger.Info($"Starting scenario '{scenarioTitle}'");
            _scenario = _feature.CreateNode<Scenario>(scenarioContext.ScenarioInfo.Title);
            _scenario.AssignCategory(scenarioContext.ScenarioInfo.Tags);
        }
        [BeforeStep]
        public static void BeforeStep(ScenarioContext scenarioContext)
        {
            var stepTitle = ScenarioStepContext.Current.StepInfo.Text;
            _logger.Info($"Starting step '{stepTitle}'");
        }

        [AfterStep]
        public static void AfterStep(ScenarioContext scenarioContext)
        {
            var stepTitle = ScenarioStepContext.Current.StepInfo.Text;
            _logger.Info($"ending step '{stepTitle}'");
        }

        [AfterScenario]
        public void AfterScenario(ScenarioContext scenarioContext)
        {
            var scenarioTitle = scenarioContext.ScenarioInfo.Title;
            _logger.Info($"Ending scenario '{scenarioTitle}'");
        }

        [AfterFeature]
        public static void AfterFeature(FeatureContext featureContext, ISpecFlowOutputHelper outputHelper)
        {
            var featureTitle = featureContext.FeatureInfo.Title;
            _logger.Info($"Ending feature '{featureTitle}'");
        }

        [AfterTestRun]
        public static void AfterTestRun()
        {
            _logger.Info("Automation Test Execution Ended");
            LogManager.Shutdown();
        }

    }
}