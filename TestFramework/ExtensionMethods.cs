using NLog;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace TestFramework
{
    public static class ExtensionMethods
    {
        private static Logger Logger = LogManager.GetCurrentClassLogger();

        public static void AddOrUpdate<T>(this ScenarioContext scenarioContext,string key,T value)
        {
            if(scenarioContext.ContainsKey(key))
            {
                scenarioContext[key]=value;
            }
            else
            {
                scenarioContext.Add(key, value);
            }
        }

        public static void UpdateUserName(this ScenarioContext scenarioContext, string value)
        {
            var key = "UserName";
            scenarioContext.AddOrUpdate(key, value);
        }
        public static void UpdatePageName(this ScenarioContext scenarioContext, string value)
        {
            var key = "PageName";
            scenarioContext.AddOrUpdate(key, value);
        }
        public static void UpdateElementName(this ScenarioContext scenarioContext, string value)
        {
            var key = "ElementName";
            scenarioContext.AddOrUpdate(key, value);
        }
        public static void UpdateActionName(this ScenarioContext scenarioContext, string value)
        {
            var key = "ActionName";
            scenarioContext.AddOrUpdate(key, value);
        }

        public static string GetActionName(this ScenarioContext scenarioContext)
        {
            var key = "ActionName";
            scenarioContext.TryGetValue<string>(key,out string value);
            return value;
        }

        public static string GetUserName(this ScenarioContext scenarioContext)
        {
            var key = "UserName";
            scenarioContext.TryGetValue<string>(key, out string value);
            return value;
        }

        public static string GetPageName(this ScenarioContext scenarioContext)
        {
            var key = "PageName";
            scenarioContext.TryGetValue<string>(key, out string value);
            return value;
        }

        public static string GetElementName(this ScenarioContext scenarioContext)
        {
            var key = "ElementName";
            scenarioContext.TryGetValue<string>(key, out string value);
            return value;
        }

        public static IWebElement FindElementWithWait(IWebDriver webdriver, By findBy, ScenarioContext scenarioContext,TimeSpan? waitPeriod=null)
        {
            waitPeriod = waitPeriod == null ? TimeSpan.FromSeconds(60) : waitPeriod;
            IWebElement webelement = null;
            var pageName = scenarioContext.GetPageName();
            var userName = scenarioContext.GetUserName();

            try
            {
                //If there is no page specific timeout specified, use default timeout
                var wait = new WebDriverWait(webdriver, waitPeriod.Value);
                wait.Until(ExpectedConditions.ElementIsVisible(findBy));
                //wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(findBy));
                //wait.Until(ExpectedConditions.ElementToBeClickable(findBy));

                webelement = wait.Until(d =>
                {
                    try
                    {
                        var result = d.FindElements(findBy).FirstOrDefault(elem => elem.Displayed);
                        return result;
                    }
                    catch (StaleElementReferenceException)
                    {
                        return null;
                    }
                });
            }
            catch (Exception ex)
            {
                Logger.Error(ex, $"Cannot find element By locator:'{findBy.Criteria}' on page:'{pageName}, logged in User: {userName}");
            }
            return webelement;
        }
        public static bool IsElementVisible(IWebDriver driver, By by)
        {
            try
            {
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(0);
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(0));
                wait.Until(ExpectedConditions.ElementIsVisible(by));
                return driver.FindElement(by).Displayed && driver.FindElement(by).Enabled;
            }
            catch (Exception ex)
            {
                Logger.Error(ex, $"Element is not visible By locator:'{by.Criteria}'");
                return false;
            }
        }
        public static IWebElement WaitForDropDownListItems(IWebDriver driver, By by)
        {
            bool hasItems = false;
            var el = driver.FindElement(by);
            while (!hasItems)
            {
                var ddl = new SelectElement(el);
                if(ddl != null && ddl.Options?.Count > 0)
                {
                    return el;
                }
            }

            return el;
        }

        public static SelectElement FindSelectElementWhenPopulated(IWebDriver driver, By by, int delayInSeconds, string optionText)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(delayInSeconds));
            return wait.Until<SelectElement>(drv =>
            {
                SelectElement element = new SelectElement(drv.FindElement(by));
                if (element.SelectedOption.ToString().Contains(optionText))
                {
                    return element;
                }

                return null;
            }
            );
        }

        public static SelectElement GetSelectElementWithText(IWebDriver webdriver, By selectFind, string option,ScenarioContext scenarioContext)
        {
            var element = FindElementWithWait(webdriver,selectFind, scenarioContext);
            var select = new SelectElement(element);
            var wait = new WebDriverWait(webdriver, TimeSpan.FromSeconds(20));

            var pageName = scenarioContext.GetPageName();
            var userName = scenarioContext.GetUserName();

            try
            {
                wait.Until(d => select.Options.Any(s => option.Equals(s.Text, StringComparison.InvariantCultureIgnoreCase)));
            }
            catch(Exception ex)
            {
                Logger.Error(ex,$"Option '{option}' is not available in the drop down locator '{selectFind.Criteria}' on page:'{pageName}, logged in User: {userName}");
                throw;
            }

            return select;
        }
        public static IWebElement MoveToElement(IWebDriver driver, By locator, ScenarioContext scenarioContext)
        {
            IWebElement el = null;

            var pageName = scenarioContext.GetPageName();
            var userName = scenarioContext.GetUserName();

            try
            {
                Actions action = new Actions(driver);
                el = driver.FindElement(locator);
                action.MoveToElement(driver.FindElement(locator));
                action.Perform();
                return el;
            }
            catch (Exception ex)
            {
                Logger.Error(ex, $"Cannot Move to element By locator:'{locator.Criteria}' on page:'{pageName}, logged in User: {userName}");
                return el;
            }
        }

        public static IWebElement FindElementEnabledWithWait(IWebDriver webdriver, By findBy, int? waitTimeInSec = null)
        {
            int count = 0;
            bool isVisible = false;
            while (!isVisible)
            {
                try
                {
                    webdriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(0);
                    if (!webdriver.FindElement(findBy).Enabled  && count < waitTimeInSec)
                    {
                        System.Threading.Thread.Sleep(1000);
                    }
                    else
                    {
                        webdriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(20);
                        return webdriver.FindElement(findBy);
                    }
                }
                catch
                {
                    if (count > waitTimeInSec)
                    {
                        return null;
                    }
                }
                count++;
            }
            webdriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(20);
            return null;
        }
    }
}
