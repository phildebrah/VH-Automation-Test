using AventStack.ExtentReports;
using AventStack.ExtentReports.Gherkin.Model;
using AventStack.ExtentReports.Reporter;
using BoDi;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.Extensions;
using RestSharp;
using SeleniumSpecFlow.Utilities;
using System;
using System.IO;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Infrastructure;
using TestLibrary.Utilities;

namespace SeleniumSpecFlow
{
    [Binding]
    public class Hooks : ObjectFactory
    {
       public static IWebDriver Driver { get; private set; }
        public static RestClient restClient { get; private set; }
        public IConfiguration Configuration { get; }
        private readonly IObjectContainer _objectContainer;
        public static EnvironmentConfigSettings config;
        public static string ProjectPath = AppDomain.CurrentDomain.BaseDirectory.ToString().Remove(AppDomain.CurrentDomain.BaseDirectory.ToString().LastIndexOf("\\") - 24);
        public static string PathReport = ProjectPath + "\\TestResults\\Report\\ExtentReport.html";
        private static ExtentTest _feature;
        private static ExtentTest _scenario;
        private static ExtentReports _extent;
        private static ISpecFlowOutputHelper _specFlowOutputHelper;
        private static string filePath;
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        public Hooks(IObjectContainer objectContainer, ISpecFlowOutputHelper outputHelper)
        {
            _objectContainer = objectContainer;
            config = TestConfigHelper.GetApplicationConfiguration();
            _specFlowOutputHelper = outputHelper;
        }

        [BeforeTestRun]
        public static void BeforeTestRun()
        {
            try
            {
                Directory.CreateDirectory(ProjectPath + Path.Combine("\\TestResults\\Report"));
                Directory.CreateDirectory(ProjectPath + Path.Combine("\\TestResults\\Img"));
                var reporter = new ExtentHtmlReporter(PathReport);
                _extent = new ExtentReports();
                _extent.AttachReporter(reporter);
                //log
                logger.Info(" Automation Testing Execution Commenced");
            }
            catch (Exception e)
            {
                logger.Error(" Exception has occurred " + e);
            }
        }

        [BeforeFeature]
        public static void BeforeFeature(FeatureContext featureContext)
        {
            _feature = _extent.CreateTest<Feature>(featureContext.FeatureInfo.Title);
            logger.Info(" Following Test has been started: " + featureContext.FeatureInfo.Title);
        }

        [BeforeScenario("web")]
        public void BeforeScenarioWeb(ScenarioContext scenarioContext)
        {
            Driver = DriverFactory.Value.InitializeDriver(TestConfigHelper.browser);
            _objectContainer.RegisterInstanceAs(Driver);
            _scenario = _feature.CreateNode<Scenario>(scenarioContext.ScenarioInfo.Title);
            _scenario.AssignCategory(scenarioContext.ScenarioInfo.Tags);
        }


        [BeforeScenario("api")]
        public void BeforeScenarioApi(ScenarioContext scenarioContext)
        {
            restClient = new RestClient(config.ApiUrl);
            _scenario = _feature.CreateNode<Scenario>(scenarioContext.ScenarioInfo.Title);
            _scenario.AssignCategory(scenarioContext.ScenarioInfo.Tags);
        }

        [BeforeScenario("soap")]
        public void BeforeScenarioSoapApi(ScenarioContext scenarioContext)
        {
            restClient = new RestClient(config.SoapApiUrl);
            _scenario = _feature.CreateNode<Scenario>(scenarioContext.ScenarioInfo.Title);
            _scenario.AssignCategory(scenarioContext.ScenarioInfo.Tags);
        }

        [AfterStep("web")]
        public static void InsertReportingStepsWeb(ScenarioContext scenarioContext)
        {
            var ScreenshotFilePath = Path.Combine(ProjectPath + "\\TestResults\\Img", Path.GetFileNameWithoutExtension(Path.GetTempFileName()) + ".png");
            var mediaModel = MediaEntityBuilder.CreateScreenCaptureFromPath(ScreenshotFilePath).Build();

            if (scenarioContext.TestError != null)
            {
                Driver.TakeScreenshot().SaveAsFile(ScreenshotFilePath, ScreenshotImageFormat.Png);
                //((ITakesScreenshot)Driver).GetScreenshot().SaveAsFile(filePath);
                switch (ScenarioStepContext.Current.StepInfo.StepDefinitionType)
                {
                    case TechTalk.SpecFlow.Bindings.StepDefinitionType.Given:
                        _scenario.CreateNode<Given>(ScenarioStepContext.Current.StepInfo.Text).Fail(scenarioContext.TestError.Message, mediaModel);
                        break;

                    case TechTalk.SpecFlow.Bindings.StepDefinitionType.When:
                        _scenario.CreateNode<When>(ScenarioStepContext.Current.StepInfo.Text).Fail(scenarioContext.TestError.Message, mediaModel);
                        break;

                    case TechTalk.SpecFlow.Bindings.StepDefinitionType.Then:
                        _scenario.CreateNode<Then>(ScenarioStepContext.Current.StepInfo.Text).Fail(scenarioContext.TestError.Message, mediaModel);
                        break;
                }
            }

            if (scenarioContext.ScenarioExecutionStatus == ScenarioExecutionStatus.StepDefinitionPending)
            {
                switch (ScenarioStepContext.Current.StepInfo.StepDefinitionType)
                {
                    case TechTalk.SpecFlow.Bindings.StepDefinitionType.Given:
                        _scenario.CreateNode<Given>(ScenarioStepContext.Current.StepInfo.Text).Skip("Step Definition Pending", mediaModel);
                        break;

                    case TechTalk.SpecFlow.Bindings.StepDefinitionType.When:
                        _scenario.CreateNode<When>(ScenarioStepContext.Current.StepInfo.Text).Skip("Step Definition Pending", mediaModel);
                        break;

                    case TechTalk.SpecFlow.Bindings.StepDefinitionType.Then:
                        _scenario.CreateNode<Then>(ScenarioStepContext.Current.StepInfo.Text).Skip("Step Definition Pending", mediaModel);
                        break;
                }
            }

            if (scenarioContext.TestError == null)
            {
                Driver.TakeScreenshot().SaveAsFile(ScreenshotFilePath, ScreenshotImageFormat.Png);

                // ((ITakesScreenshot)Driver).GetScreenshot().SaveAsFile(filePath);
                //For Extent report
                switch (ScenarioStepContext.Current.StepInfo.StepDefinitionType)
                {
                    case TechTalk.SpecFlow.Bindings.StepDefinitionType.Given:
                        _scenario.CreateNode<Given>(ScenarioStepContext.Current.StepInfo.Text).Pass(string.Empty, mediaModel);
                        _scenario.CreateNode<Given>(ScenarioStepContext.Current.StepInfo.Text).Pass(string.Empty, mediaModel);
                        break;

                    case TechTalk.SpecFlow.Bindings.StepDefinitionType.When:
                        _scenario.CreateNode<When>(ScenarioStepContext.Current.StepInfo.Text).Pass(string.Empty, mediaModel);
                        break;

                    case TechTalk.SpecFlow.Bindings.StepDefinitionType.Then:
                        _scenario.CreateNode<Then>(ScenarioStepContext.Current.StepInfo.Text).Pass(string.Empty, mediaModel);
                        break;
                }

                // For Living Doc
                filePath = Path.Combine(ProjectPath + "\\TestResults\\Img", Path.GetFileNameWithoutExtension(Path.GetRandomFileName()) + ".png");
                ((ITakesScreenshot)Driver).GetScreenshot().SaveAsFile(filePath);
                _specFlowOutputHelper.WriteLine("Logging Using Specflow");
                _specFlowOutputHelper.AddAttachment(filePath);
            }
        }

        [AfterStep("api", "soap")]
        public static void InsertReportingStepsApi(ScenarioContext scenarioContext)
        {
            if (scenarioContext.TestError != null)
            {
                switch (ScenarioStepContext.Current.StepInfo.StepDefinitionType)
                {
                    case TechTalk.SpecFlow.Bindings.StepDefinitionType.Given:
                        _scenario.CreateNode<Given>(ScenarioStepContext.Current.StepInfo.Text).Fail(scenarioContext.TestError.Message);
                        break;

                    case TechTalk.SpecFlow.Bindings.StepDefinitionType.When:
                        _scenario.CreateNode<When>(ScenarioStepContext.Current.StepInfo.Text).Fail(scenarioContext.TestError.Message);
                        break;

                    case TechTalk.SpecFlow.Bindings.StepDefinitionType.Then:
                        _scenario.CreateNode<Then>(ScenarioStepContext.Current.StepInfo.Text).Fail(scenarioContext.TestError.Message);
                        break;
                }
            }

            if (scenarioContext.ScenarioExecutionStatus == ScenarioExecutionStatus.StepDefinitionPending)
            {
                switch (ScenarioStepContext.Current.StepInfo.StepDefinitionType)
                {
                    case TechTalk.SpecFlow.Bindings.StepDefinitionType.Given:
                        _scenario.CreateNode<Given>(ScenarioStepContext.Current.StepInfo.Text).Skip("Step Definition Pending");
                        break;

                    case TechTalk.SpecFlow.Bindings.StepDefinitionType.When:
                        _scenario.CreateNode<When>(ScenarioStepContext.Current.StepInfo.Text).Skip("Step Definition Pending");
                        break;

                    case TechTalk.SpecFlow.Bindings.StepDefinitionType.Then:
                        _scenario.CreateNode<Then>(ScenarioStepContext.Current.StepInfo.Text).Skip("Step Definition Pending");
                        break;
                }
            }

            if (scenarioContext.TestError == null)
            {
                switch (ScenarioStepContext.Current.StepInfo.StepDefinitionType)
                {
                    case TechTalk.SpecFlow.Bindings.StepDefinitionType.Given:
                        _scenario.CreateNode<Given>(ScenarioStepContext.Current.StepInfo.Text).Pass(string.Empty);
                        break;

                    case TechTalk.SpecFlow.Bindings.StepDefinitionType.When:
                        _scenario.CreateNode<When>(ScenarioStepContext.Current.StepInfo.Text).Pass(string.Empty);
                        break;

                    case TechTalk.SpecFlow.Bindings.StepDefinitionType.Then:
                        _scenario.CreateNode<Then>(ScenarioStepContext.Current.StepInfo.Text).Pass(string.Empty);
                        break;
                }
            }
        }

        [AfterScenario("web")]
        public void AfterScenarioWeb()
        {
            _extent.Flush();
            logger.Info(" Flush Extent Report Instance");
            Driver?.Quit();
            Driver?.Dispose();
            logger.Info(" Driver has been closed");
            TestContext.AddTestAttachment(filePath);
            GC.SuppressFinalize(this);
        }

        [AfterScenario("api", "soap")]
        public void AfterScenarioApi()
        {
            _extent.Flush();
            GC.SuppressFinalize(this);
        }

        [AfterTestRun]
        public static void AfterTestRun()
        {
            NLog.LogManager.Shutdown();
            logger.Info(" Flush Nlog Instance");
        }
    }
}
