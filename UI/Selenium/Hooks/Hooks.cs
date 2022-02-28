﻿using AventStack.ExtentReports;
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
using System.Collections.Generic;
using System.IO;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Infrastructure;
using TestLibrary.Utilities;
using UISelenium.Helper;
using NLog;
using System.Text;
using TestFramework;
using NLog.Config;
using NLog.Web;
using UISelenium.Pages;

namespace SeleniumSpecFlow
{
    [Binding]
    public class Hooks //: ObjectFactory
    {
       //public static IWebDriver Driver { get; private set; }
        public static RestClient restClient { get; private set; }
        public IConfiguration Configuration { get; }
        //private IObjectContainer _objectContainer;
        public static EnvironmentConfigSettings config;
        public static string ProjectPath = AppDomain.CurrentDomain.BaseDirectory.ToString().Remove(AppDomain.CurrentDomain.BaseDirectory.ToString().LastIndexOf("\\") - 17);
        public static string PathReport = ProjectPath + "\\TestResults\\Report\\ExtentReport.html";
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
                Directory.CreateDirectory(ProjectPath + Path.Combine("\\TestResults\\Report"));
                Directory.CreateDirectory(ProjectPath + Path.Combine("\\TestResults\\Img"));
                var reporter = new ExtentHtmlReporter(PathReport);
                _extent = new ExtentReports();
                _extent.AttachReporter(reporter);
                Logger.InfoWithDate("Automation Test Execution Commenced");
            }
            catch (Exception ex)
            {
                Logger.Error(ex,"An error has occured");
            }
        }

        [BeforeFeature]
        public static void BeforeFeature(FeatureContext featureContext, ISpecFlowOutputHelper outputHelper)
        {
            var featureTile = featureContext.FeatureInfo.Title;
            _feature = _extent.CreateTest<Feature>(featureTile);

            Logger.InfoWithDate($"Starting feature '{featureTile}'");

            config = TestConfigHelper.GetApplicationConfiguration();
            _specFlowOutputHelper = outputHelper;
        }

        [BeforeScenario]
        public void BeforeScenario(ScenarioContext scenarioContext)
        {
            var scenarioTile = scenarioContext.ScenarioInfo.Title;
            Logger.InfoWithDate($"Starting scenario '{scenarioTile}'");
        }
        
        [BeforeScenario("web")]
        public void BeforeScenarioWeb(ScenarioContext scenarioContext)
        {
            IWebDriver Driver = new DriverFactory().InitializeDriver(TestConfigHelper.browser);
            //IWebDriver Driver =  DriverFactory.InitializeDriver(TestConfigHelper.browser);
            //Driver.(TestConfigHelper.browser.ToString());
            scenarioContext.Add("driver", Driver);
            scenarioContext.Add("config", config);
            _scenario = _feature.CreateNode<Scenario>(scenarioContext.ScenarioInfo.Title);
            _scenario.AssignCategory(scenarioContext.ScenarioInfo.Tags);
        }


        [BeforeScenario("api")]
        public void BeforeScenarioApi(ScenarioContext scenarioContext)
        {
            var scenarioTile = scenarioContext.ScenarioInfo.Title;
            Logger.InfoWithDate($"Starting scenario '{scenarioTile}'");

            restClient = new RestClient(config.ApiUrl);
            _scenario = _feature.CreateNode<Scenario>(scenarioContext.ScenarioInfo.Title);
            _scenario.AssignCategory(scenarioContext.ScenarioInfo.Tags);
        }

        [BeforeScenario("soap")]
        public void BeforeScenarioSoapApi(ScenarioContext scenarioContext)
        {
            var scenarioTile = scenarioContext.ScenarioInfo.Title;
            Logger.InfoWithDate($"Starting scenario '{scenarioTile}'");

            restClient = new RestClient(config.SoapApiUrl);
            _scenario = _feature.CreateNode<Scenario>(scenarioContext.ScenarioInfo.Title);
            _scenario.AssignCategory(scenarioContext.ScenarioInfo.Tags);
        }

        [BeforeStep]
        public static void BeforeStep(ScenarioContext scenarioContext)
        {
            var stepTitle = ScenarioStepContext.Current.StepInfo.Text;
            Logger.InfoWithDate($"Starting step '{stepTitle}'");
        }

        [AfterStep]
        public static void AfterStep(ScenarioContext scenarioContext)
        {
            var stepTitle = ScenarioStepContext.Current.StepInfo.Text;
            Logger.InfoWithDate($"ending step '{stepTitle}'");
        }

        [AfterStep("web")]
        public static void InsertReportingStepsWeb(ScenarioContext scenarioContext)
        {   
            var ScreenshotFilePath = Path.Combine(ProjectPath + "\\TestResults\\Img", Path.GetFileNameWithoutExtension(Path.GetTempFileName()) + ".png");
            var mediaModel = MediaEntityBuilder.CreateScreenCaptureFromPath(ScreenshotFilePath).Build();

            if (scenarioContext.TestError != null)
            {
                var stepTitle = ScenarioStepContext.Current.StepInfo.Text;
                Logger.Error(scenarioContext.TestError, $"Exception occured while executing step:{stepTitle}");
                var infoTextBuilder = new StringBuilder();

                var actionName = scenarioContext.GetActionName();
                if (!string.IsNullOrWhiteSpace(actionName))
                {
                    infoTextBuilder.Append($"Action '{actionName}");
                }

                var elementName = scenarioContext.GetElementName();
                if (!string.IsNullOrWhiteSpace(elementName))
                {
                    infoTextBuilder.Append($"erroed on Element '{elementName}");
                }

                var pageName = scenarioContext.GetPageName();
                if (!string.IsNullOrWhiteSpace(pageName))
                {
                    infoTextBuilder.Append($"on Page '{pageName}");
                }

                var userName = scenarioContext.GetUserName();
                if (!string.IsNullOrWhiteSpace(userName))
                {
                    infoTextBuilder.Append($"for User '{userName}");
                }

                var infoText = infoTextBuilder.ToString();
                if(!string.IsNullOrEmpty(infoText))
                {
                    Logger.InfoWithDate(infoText);
                }

                Helper.GetDriverInstance(scenarioContext).TakeScreenshot().SaveAsFile(ScreenshotFilePath, ScreenshotImageFormat.Png);
                Logger.InfoWithDate($"Screenshot has been saved to {ScreenshotFilePath}");
                Helper.GetDriverInstance(scenarioContext).TakeScreenshot().SaveAsFile(ScreenshotFilePath, ScreenshotImageFormat.Png);

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
                Helper.GetDriverInstance(scenarioContext).TakeScreenshot().SaveAsFile(ScreenshotFilePath, ScreenshotImageFormat.Png);
                Logger.InfoWithDate($"Screenshot has been saved to {ScreenshotFilePath}");

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
                filePath = Path.Combine(ProjectPath + "\\TestResults\\Img", Path.GetFileNameWithoutExtension(Path.GetRandomFileName()) + ".png");
                Helper.GetDriverInstance(scenarioContext).TakeScreenshot().SaveAsFile(filePath, ScreenshotImageFormat.Png);
                Logger.InfoWithDate($"Screenshot has been saved to {filePath}");

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
                Logger.InfoWithDate("Driver has been closed");

            }
            _extent.Flush();
            Logger.InfoWithDate("Flush Extent Report Instance");
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
                    Logger.InfoWithDate($"{driver.Key} Driver has been closed");
                }
            }

            if (scenarioContext.ContainsKey("driver"))
            {
                var driver = (IWebDriver)scenarioContext["driver"];
                driver.Quit();
                Logger.InfoWithDate($"Driver has been closed");
            }

            _extent.Flush();
            Logger.InfoWithDate("Flush Extent Report Instance");
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
            var scenarioTile = scenarioContext.ScenarioInfo.Title;
            Logger.InfoWithDate($"Ending scenario '{scenarioTile}'");
        }

        [AfterTestRun]
        public static void AfterTestRun()
        {
            Logger.InfoWithDate("Automation Test Execution Ended");
            LogManager.Shutdown();
        }

        [AfterFeature]
        public static void AfterFeature(FeatureContext featureContext, ISpecFlowOutputHelper outputHelper)
        {
            var featureTile = featureContext.FeatureInfo.Title;
            Logger.InfoWithDate($"Ending feature '{featureTile}'");
        }

        private void ConfigureNLog()
        {
            // Intialize Config Object
            LoggingConfiguration config = new LoggingConfiguration();
        }
    }
}
