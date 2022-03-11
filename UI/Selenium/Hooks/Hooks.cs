using AventStack.ExtentReports;
using AventStack.ExtentReports.Gherkin.Model;
using AventStack.ExtentReports.Reporter;
using Microsoft.Extensions.Configuration;
using NLog;
using NLog.Web;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.Extensions;
using SeleniumSpecFlow.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Infrastructure;
using TestFramework;
using TestLibrary.Utilities;
using UI.Model;

namespace SeleniumSpecFlow
{
    [Binding]
    public class Hooks 
    {
        public IConfiguration Configuration { get; }
        public static EnvironmentConfigSettings config;
        private static string ProjectPath = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;
        public static string PathReport ;
        private static string ImagesPath;
        private static ExtentTest _feature;
        private static ExtentTest _scenario;
        private static ExtentReports _extent;
        private static ISpecFlowOutputHelper _specFlowOutputHelper;
        private static string filePath;
        private static Logger Logger = LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();


        [BeforeTestRun]
        public static void BeforeTestRun()
        {
            try
            {
                config = TestConfigHelper.GetApplicationConfiguration();

                Logger.Info("Automation Test Execution Commenced");
                
                var logFilePath = Util.GetLogFileName("logfile");
                var logFileName = Path.GetFileNameWithoutExtension(logFilePath);
                var folderName = logFileName.Replace(":",".");

                ImagesPath=Path.Combine(config.ImageLocation, folderName);
                Directory.CreateDirectory(ProjectPath + ImagesPath);

                PathReport= Path.Combine(ProjectPath+config.ReportLocation, folderName, "ExtentReport.html");
                var reporter = new ExtentHtmlReporter(PathReport);
                _extent = new ExtentReports();
                _extent.AttachReporter(reporter);
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "An error has occured before Automation Test Execution ");
            }
        }

        [BeforeFeature]
        public static void BeforeFeature(FeatureContext featureContext, ISpecFlowOutputHelper outputHelper)
        {
            var featureTitle = featureContext.FeatureInfo.Title;
            _feature = _extent.CreateTest<Feature>(featureTitle);

            Logger.Info($"Starting feature '{featureTitle}'");

            _specFlowOutputHelper = outputHelper;
        }

        [BeforeScenario]
        public void BeforeScenario(ScenarioContext scenarioContext)
        {
            var scenarioTitle = scenarioContext.ScenarioInfo.Title;
            Logger.Info($"Starting scenario '{scenarioTitle}'");
        }
        
        [BeforeScenario("web")]
        public void BeforeScenarioWeb(ScenarioContext scenarioContext)
        {
            var title = scenarioContext.ScenarioInfo.Title;
            var tags= scenarioContext.ScenarioInfo.Tags;

            _scenario = _feature.CreateNode<Scenario>(title);
            _scenario.AssignCategory(tags);

            IWebDriver Driver;
            if (RunOnSauceLabs(tags))
            {
                SauceLabsOptions sauceOptions = new SauceLabsOptions();
                sauceOptions.Name=title;
                Driver= new DriverFactory().InitializeSauceDriver(sauceOptions,config.SauceLabsConfiguration);
            }
            else
            {
                Driver= new DriverFactory().InitializeDriver(TestConfigHelper.browser);
            }
             
            scenarioContext.Add("driver", Driver);
            scenarioContext.Add("config", config);
        }


        [BeforeScenario("api")]
        public void BeforeScenarioApi(ScenarioContext scenarioContext)
        {
            var scenarioTitle = scenarioContext.ScenarioInfo.Title;
            Logger.Info($"Starting scenario '{scenarioTitle}'");

            _scenario = _feature.CreateNode<Scenario>(scenarioContext.ScenarioInfo.Title);
            _scenario.AssignCategory(scenarioContext.ScenarioInfo.Tags);
        }

        [BeforeScenario("soap")]
        public void BeforeScenarioSoapApi(ScenarioContext scenarioContext)
        {
            var scenarioTitle = scenarioContext.ScenarioInfo.Title;
            Logger.Info($"Starting scenario '{scenarioTitle}'");

            _scenario = _feature.CreateNode<Scenario>(scenarioContext.ScenarioInfo.Title);
            _scenario.AssignCategory(scenarioContext.ScenarioInfo.Tags);
        }

        [BeforeStep]
        public static void BeforeStep(ScenarioContext scenarioContext)
        {
            var stepTitle = ScenarioStepContext.Current.StepInfo.Text;
            Logger.Info($"Starting step '{stepTitle}'");
        }

        [AfterStep]
        public static void AfterStep(ScenarioContext scenarioContext)
        {
            var stepTitle = ScenarioStepContext.Current.StepInfo.Text;
            Logger.Info($"ending step '{stepTitle}'");
        }

        [AfterStep("web")]
        public static void InsertReportingStepsWeb(ScenarioContext scenarioContext)
        {
            var driver = (IWebDriver)scenarioContext["driver"];
            var ScreenshotFilePath = Path.Combine(ProjectPath + ImagesPath, Path.GetFileNameWithoutExtension(Path.GetTempFileName()) + ".png");
            var mediaModel = MediaEntityBuilder.CreateScreenCaptureFromPath(ScreenshotFilePath).Build();

            if (scenarioContext.TestError != null && !(scenarioContext.TestError is AssertionException))
            {
                var stepTitle = ScenarioStepContext.Current.StepInfo.Text;
                Logger.Error(scenarioContext.TestError, $"Exception occured while executing step:'{stepTitle}'");
                var infoTextBuilder = new StringBuilder();
                
                var actionName = scenarioContext.GetActionName();
                if (!string.IsNullOrWhiteSpace(actionName))
                {
                    infoTextBuilder.Append($"Action '{actionName}'");
                }

                var elementName = scenarioContext.GetElementName();
                if (!string.IsNullOrWhiteSpace(elementName))
                {
                    infoTextBuilder.Append($",erroed on Element '{elementName}'");
                }

                var pageName = scenarioContext.GetPageName();
                if (!string.IsNullOrWhiteSpace(pageName))
                {
                    infoTextBuilder.Append($",on Page '{pageName}'");
                }

                var userName = scenarioContext.GetUserName();
                if (!string.IsNullOrWhiteSpace(userName))
                {
                    infoTextBuilder.Append($",for User '{userName}");
                }

                var infoText = infoTextBuilder.ToString();
                if(!string.IsNullOrEmpty(infoText))
                {
                    Logger.Info(infoText);
                }

                driver.TakeScreenshot().SaveAsFile(ScreenshotFilePath, ScreenshotImageFormat.Png);
                Logger.Info($"Screenshot has been saved to {ScreenshotFilePath}");
                driver.TakeScreenshot().SaveAsFile(ScreenshotFilePath, ScreenshotImageFormat.Png);

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
                driver.TakeScreenshot().SaveAsFile(ScreenshotFilePath, ScreenshotImageFormat.Png);
                Logger.Info($"Screenshot has been saved to {ScreenshotFilePath}");

                //Driver.TakeScreenshot().SaveAsFile(ScreenshotFilePath, ScreenshotImageFormat.Png);

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
                filePath = Path.Combine(ProjectPath + ImagesPath, Path.GetFileNameWithoutExtension(Path.GetRandomFileName()) + ".png");
                driver.TakeScreenshot().SaveAsFile(filePath, ScreenshotImageFormat.Png);
                Logger.Info($"Screenshot has been saved to {filePath}");

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

        [AfterScenario("Hearing")]
        public void AfterScenarioHearing(ScenarioContext scenarioContext)
        {
            var drivers = (Dictionary<string, IWebDriver>)scenarioContext["drivers"];
            foreach(var driver in drivers)
            {
                driver.Value.Quit();
                Logger.Info("Driver has been closed");

            }
            _extent.Flush();
            Logger.Info("Flush Extent Report Instance");
            GC.SuppressFinalize(this);
        }

        [AfterScenario("web")]
        public void AfterScenarioWeb(ScenarioContext scenarioContext)
        {
            if(scenarioContext.ContainsKey("drivers"))
            {
                var drivers = (Dictionary<string, IWebDriver>)scenarioContext["drivers"];
                foreach (var driver in drivers)
                {
                    driver.Value.Quit();
                    Logger.Info($"{driver.Key} Driver has been closed");
                }
            }

            if (scenarioContext.ContainsKey("driver"))
            {
                var driver = (IWebDriver)scenarioContext["driver"];
                driver.Quit();
                Logger.Info($"Driver has been closed");
            }

            _extent.Flush();
            Logger.Info("Flush Extent Report Instance");
            TestContext.AddTestAttachment(filePath);
            GC.SuppressFinalize(this);
        }

        [AfterScenario("api", "soap")]
        public void AfterScenarioApi()
        {
            _extent.Flush();
            GC.SuppressFinalize(this);
        }

        [AfterScenario]
        public void AfterScenario(ScenarioContext scenarioContext)
        {
            var scenarioTitle = scenarioContext.ScenarioInfo.Title;
            Logger.Info($"Ending scenario '{scenarioTitle}'");
        }

        [AfterTestRun]
        public static void AfterTestRun()
        {
            Logger.Info("Automation Test Execution Ended");
            LogManager.Shutdown();
        }

        [AfterFeature]
        public static void AfterFeature(FeatureContext featureContext, ISpecFlowOutputHelper outputHelper)
        {
            var featureTitle = featureContext.FeatureInfo.Title;
            Logger.Info($"Ending feature '{featureTitle}'");
        }

        private static bool RunOnSauceLabs(string[] tags)
        {
            return config.RunOnSaucelabs && tags.Any(s => s.Contains("DeviceTest"));
        }
    }
}
