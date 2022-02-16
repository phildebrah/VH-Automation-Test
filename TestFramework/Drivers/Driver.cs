using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.Extensions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using WebDriverManager.DriverConfigs.Impl;
namespace TestFramework.Drivers
{
    public class Driver : IDriver
    {
        private IWebDriver _browser;
        private string browserName;

        public IWebDriver Browser()
        {
            return this._browser;
        }

        public void StartBrowser(string browser = "Chrome", string path = "")
        {
            //path = path == string.Empty ? Directory.GetCurrentDirectory() + "\\Webdriver\\" : path;
            //if (!string.IsNullOrEmpty(path))
            //{
                if (browser.ToLower() == "firefox")
                {
                    FirefoxDriverService service = FirefoxDriverService.CreateDefaultService(path, "geckodriver.exe");
                    service.Host = "::1";
                    service.HideCommandPromptWindow = true;
                    FirefoxOptions firefoxOptions = new FirefoxOptions
                    {
                        AcceptInsecureCertificates = true,

                    };

                    this._browser = new FirefoxDriver(service, firefoxOptions);
                    this._browser.Manage().Window.Maximize();
                    this.browserName = browser;
                }
                else
                {
                    new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig());
                    var options = new ChromeOptions();
                    int trys = 0;
                    while (trys < 3 && this._browser == null)
                    {
                        try
                        {
                            //var chromeDriverService = ChromeDriverService.CreateDefaultService(path);
                            //chromeDriverService.HideCommandPromptWindow = true;
                            options.AddArgument("--start-maximized");
                            options.AddArgument("--disable-notifications");
                            //this._browser = new ChromeDriver(chromeDriverService, options);
                            this._browser = new ChromeDriver(options);
                            this.browserName = browser;
                            break;
                        }
                        catch
                        {
                            WaitFor(1);
                        }

                        trys++;
                    }
                //}
            }
        }

        public string BrowserName()
        {
            return this.browserName;
        }

        public void Click(string locator)
        {
            if (string.IsNullOrEmpty(locator))
            {
                throw new Exception("Element can not be null");
            }

            try
            {
                this.WaitForPageLoad();
                this.FindElementVisible(locator).Click();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void ClickAllVisible(string locator)
        {
            try
            {
                foreach (IWebElement e in this.GetAllWebElements(locator))
                {
                    if (e.Displayed)
                    {
                        e.Click();
                    }
                }
            }
            catch
            {
            }
        }

        public void ClickAllPresent(string locator)
        {
            try
            {
                foreach (IWebElement e in this.GetAllWebElements(locator))
                {
                    e.Click();
                }
            }
            catch
            {
            }
        }

        public string GetUrl()
        {
            return Browser().Url;
        }

        public IWebElement FindElementVisible(string locator)
        {
            try
            {
                //// Added for Ie and Edge
                if (!string.IsNullOrEmpty(locator) && locator.Contains("//") && this.browserName.ToLower() != "chrome" && this.browserName.ToLower() != "firefox")
                {
                    foreach (IWebElement el in this._browser.FindElements(By.XPath(locator)))
                    {
                        if (el.TagName == "select" && el.Enabled)
                        {
                            return el;
                        }

                        if (el.Displayed)
                        {
                            return el;
                        }
                    }

                    return null;
                }

                if (this.FindElement(By.Id(locator)) != null)
                {
                    foreach (IWebElement el in this._browser.FindElements(By.Id(locator)))
                    {
                        if (el.Displayed && el.Enabled)
                        {
                            return el;
                        }
                    }
                }
                else if (this.FindElement(By.XPath(locator)) != null)
                {
                    foreach (IWebElement el in this._browser.FindElements(By.XPath(locator)))
                    {
                        if (el.TagName == "select" && el.Enabled)
                        {
                            return el;
                        }

                        if (el.Displayed && el.Enabled)
                        {
                            return el;
                        }
                    }
                }
                else if (this.FindElement(By.ClassName(locator)) != null)
                {
                    foreach (IWebElement el in this._browser.FindElements(By.ClassName(locator)))
                    {
                        if (el.Displayed && el.Enabled)
                        {
                            return el;
                        }
                    }
                }
                else if (this.FindElement(By.CssSelector(locator)) != null)
                {
                    foreach (IWebElement el in this._browser.FindElements(By.CssSelector(locator)))
                    {
                        if (el.Displayed && el.Enabled)
                        {
                            return el;
                        }
                    }
                }
                else if (this.FindElement(By.LinkText(locator)) != null)
                {
                    foreach (IWebElement el in this._browser.FindElements(By.LinkText(locator)))
                    {
                        if (el.Displayed && el.Enabled)
                        {
                            return el;
                        }
                    }
                }
                else if (this.FindElement(By.Name(locator)) != null)
                {
                    foreach (IWebElement el in this._browser.FindElements(By.Name(locator)))
                    {
                        if (el.Displayed && el.Enabled)
                        {
                            return el;
                        }
                    }
                }
            }
            catch (Exception)
            {
            }

            return null;
        }

        public IWebElement FindElementVisible(IWebElement element)
        {
            try
            {
                if (element.Displayed && element.Enabled)
                {
                    return element;
                }

            }
            catch (Exception)
            {
            }

            return null;
        }

        public IWebElement FindElementEnabled(string locator)
        {
            try
            {
                //// Added for Edge
                if (!string.IsNullOrEmpty(locator) && locator.Contains("//") && this.browserName.ToLower() != "chrome" && this.browserName.ToLower() != "firefox")
                {
                    foreach (IWebElement el in this._browser.FindElements(By.XPath(locator)))
                    {
                        if (el.TagName == "select" && el.Enabled)
                        {
                            return el;
                        }

                        if (el.Displayed && el.Enabled)
                        {
                            return el;
                        }
                    }

                    return null;
                }

                if (this.FindElement(By.Id(locator)) != null)
                {
                    foreach (IWebElement el in this._browser.FindElements(By.Id(locator)))
                    {
                        if (el.Displayed && el.Enabled)
                        {
                            return el;
                        }
                    }
                }
                else if (this.FindElement(By.XPath(locator)) != null)
                {
                    foreach (IWebElement el in this._browser.FindElements(By.XPath(locator)))
                    {
                        if (el.TagName == "select" && el.Enabled)
                        {
                            return el;
                        }

                        if (el.Displayed && el.Enabled)
                        {
                            return el;
                        }
                    }
                }
                else if (this.FindElement(By.ClassName(locator)) != null)
                {
                    foreach (IWebElement el in this._browser.FindElements(By.ClassName(locator)))
                    {
                        if (el.Displayed && el.Enabled)
                        {
                            return el;
                        }
                    }
                }
                else if (this.FindElement(By.CssSelector(locator)) != null)
                {
                    foreach (IWebElement el in this._browser.FindElements(By.CssSelector(locator)))
                    {
                        if (el.Displayed && el.Enabled)
                        {
                            return el;
                        }
                    }
                }
                else if (this.FindElement(By.LinkText(locator)) != null)
                {
                    foreach (IWebElement el in this._browser.FindElements(By.LinkText(locator)))
                    {
                        if (el.Displayed && el.Enabled)
                        {
                            return el;
                        }
                    }
                }
                else if (this.FindElement(By.Name(locator)) != null)
                {
                    foreach (IWebElement el in this._browser.FindElements(By.Name(locator)))
                    {
                        if (el.Displayed && el.Enabled)
                        {
                            return el;
                        }
                    }
                }
            }
            catch (Exception)
            {
            }

            return null;
        }

        public IWebElement FindElementPresent(string locator)
        {
            try
            {
                if (this.FindElement(By.Id(locator)) != null)
                {
                    return this.FindElement(By.Id(locator));
                }
                else if (this.FindElement(By.XPath(locator)) != null)
                {
                    return this.FindElement(By.XPath(locator));
                }
                else if (this.FindElement(By.ClassName(locator)) != null)
                {
                    this.FindElement(By.ClassName(locator));
                }
                else if (this.FindElement(By.CssSelector(locator)) != null)
                {
                    this.FindElement(By.CssSelector(locator));
                }
                else if (this.FindElement(By.LinkText(locator)) != null)
                {
                    this.FindElement(By.LinkText(locator));
                }
                else if (this.FindElement(By.Name(locator)) != null)
                {
                    this.FindElement(By.Name(locator));
                }
            }
            catch (Exception)
            {
            }

            return null;
        }

        public IList<IWebElement> GetVisibleElements(string locator)
        {
            IList<IWebElement> elements = new List<IWebElement>();
            try
            {
                foreach (var e in this.GetAllWebElements(locator))
                {
                    if (e.Displayed)
                    {
                        elements.Add(e);
                    }
                }
            }
            catch
            {
                return elements;
            }


            return elements;
        }

        public void MoveToElement(string locator)
        {
            Actions action = new Actions(this._browser);
            action.MoveToElement(this.FindElementVisible(locator));
            action.Perform();
        }

        public void WaitForPageLoad(TimeSpan? timespan = null)
        {
            timespan = timespan == null ? TimeSpan.FromSeconds(60) : timespan;
            IWait<IWebDriver> wait = new WebDriverWait(this._browser, timespan.Value);
            wait.Until(a => ((IJavaScriptExecutor)this._browser).ExecuteScript("return document.readyState").Equals("complete"));
            this.WaitForJQueryLoad(timespan.Value);
            WaitFor(0.1);
        }

        public bool IsElementClickable(string locator)
        {
            try
            {
                WebDriverWait wait = new WebDriverWait(this._browser, TimeSpan.FromSeconds(0));
                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(this.FindElementPresent(locator)));
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool IsElementClickable(IWebElement element)
        {
            try
            {
                WebDriverWait wait = new WebDriverWait(this._browser, TimeSpan.FromSeconds(0));
                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(element));
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool IsElementStale(IWebElement element)
        {
            try
            {
                WebDriverWait wait = new WebDriverWait(this._browser, TimeSpan.FromSeconds(0));
                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.StalenessOf(element));
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool IsPageSourcePresent()
        {
            try
            {
                return !string.IsNullOrEmpty(this._browser.PageSource);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return false;
        }

        public void Navigate(string url)
        {
            this._browser.Navigate().GoToUrl(url);
            this.WaitForPageLoad(TimeSpan.FromMinutes(1));
        }

        public bool IsElementVisible(string locator)
        {
            if (this.FindElementVisible(locator) != null)
            {
                return true;
            }

            return false;
        }

        public bool IsElementVisible(IWebElement element)
        {
            if (this.FindElementVisible(element) != null)
            {
                return true;
            }

            return false;
        }

        public bool IsElementPresent(string locator)
        {
            if (this.FindElementPresent(locator) != null)
            {
                return true;
            }

            return false;
        }

        public bool IsElementPresent(IWebElement element)
        {
            try
            {
                if (element.Displayed)
                {
                    return true;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return false;
        }

        public bool IsElementEnabled(string locator, bool testInteraction = false)
        {
            if (this.FindElementEnabled(locator) != null)
            {
                return testInteraction == true ? this.IsInteractable(locator) : true;
            }

            return false;
        }

        public void SelectFromDropDownList(string locator, string item, bool byValue = false, bool byIndex = false)
        {
            var ddl = this.GetAllWebElements(locator);
            if (ddl == null)
            {
                throw new Exception("Drop down list " + locator + " Not found at current page");
            }

            foreach (var el in ddl)
            {
                try
                {
                    var selectElement = new SelectElement(el);
                    if (byValue)
                    {
                        selectElement.SelectByValue(item);
                    }
                    else if (byIndex)
                    {
                        selectElement.SelectByIndex(int.Parse(item));
                    }
                    else
                    {
                        selectElement.SelectByText(item);
                    }
                }
                catch
                {
                }
            }
        }

        public List<IWebElement> GetDropDownListOptions(string locator)
        {
            List<IWebElement> el = new List<IWebElement>();
            var ddl = this.FindElementVisible(locator);
            var selectElement = new SelectElement(ddl);

            var options = selectElement.Options;
            foreach (IWebElement e in options)
            {
                el.Add(e);
            }

            return el;
        }

        public void WaitForElementVisible(string locator, double timeInSeconds = 60)
        {
            WaitForPageLoad(TimeSpan.FromSeconds(timeInSeconds));
            var ts = new TimeSpan(DateTime.Now.Ticks);
            double totalTime = this.ClockTime(ts);
            while (this.FindElementVisible(locator) == null && totalTime <= timeInSeconds)
            {
                totalTime = Convert.ToInt32(this.ClockTime(ts));
                if (!string.IsNullOrEmpty(CheckForErrorsOnPage()))
                {
                    Assert.Fail("UI_Error: " + CheckForErrorsOnPage());
                }
                if (totalTime >= timeInSeconds)
                {
                    Assert.Fail("Failed to find locator with locator expression " + locator + " after " + totalTime + " seconds");
                }
            }

            WaitForPageLoad(TimeSpan.FromSeconds(timeInSeconds));
        }

        public void WaitForElementEnabled(string locator, double timeInSeconds = 60)
        {
            WaitForPageLoad(TimeSpan.FromSeconds(timeInSeconds));
            var ts = new TimeSpan(DateTime.Now.Ticks);
            double totalTime = this.ClockTime(ts);
            while (this.FindElementEnabled(locator) == null && totalTime <= timeInSeconds)
            {
                totalTime = Convert.ToInt32(this.ClockTime(ts));
                if (totalTime >= timeInSeconds)
                {
                    Assert.Fail("Failed to find locator with locator expression " + locator + " after " + totalTime + " seconds");
                }
            }

            WaitForPageLoad(TimeSpan.FromSeconds(timeInSeconds));
        }

        public void WaitForElementNotVisible(string locator, double timeInSeconds = 60)
        {
            WaitForPageLoad(TimeSpan.FromSeconds(timeInSeconds));
            var ts = new TimeSpan(DateTime.Now.Ticks);
            while (this.IsElementVisible(locator))
            {
                double totalTime = Convert.ToInt32(this.ClockTime(ts));
                if (totalTime > timeInSeconds)
                {
                    Assert.Fail(locator + " was still visible after " + totalTime + " seconds");
                }
            }

            WaitForPageLoad(TimeSpan.FromSeconds(timeInSeconds));
        }

        public void WaitForElementNotVisible(IWebElement element, double timeInSeconds = 60)
        {
            WaitForPageLoad(TimeSpan.FromSeconds(timeInSeconds));
            var ts = new TimeSpan(DateTime.Now.Ticks);
            while (this.IsElementVisible(element))
            {
                double totalTime = Convert.ToInt32(this.ClockTime(ts));
                if (!string.IsNullOrEmpty(CheckForErrorsOnPage()))
                {
                    Assert.Fail("UI_Error: " + CheckForErrorsOnPage());
                }
                if (totalTime > timeInSeconds)
                {
                    Assert.Fail(element + " was still visible after " + totalTime + " seconds");
                }
            }

            WaitForPageLoad(TimeSpan.FromSeconds(timeInSeconds));
        }

        public void WaitForElementPresent(string locator, double timeInSeconds = 60)
        {
            WaitForPageLoad(TimeSpan.FromSeconds(timeInSeconds));
            var ts = new TimeSpan(DateTime.Now.Ticks);
            while (this.FindElementPresent(locator) == null)
            {
                double totalTime = Convert.ToInt32(this.ClockTime(ts));
                if (totalTime > timeInSeconds)
                {
                    Assert.Fail(locator + " was still present after " + totalTime + " seconds");
                }
            }

            WaitForPageLoad(TimeSpan.FromSeconds(timeInSeconds));
        }

        public void WaitForElementNotPresent(IWebElement element, double timeInSeconds = 60)
        {
            WaitForPageLoad(TimeSpan.FromSeconds(timeInSeconds));
            var ts = new TimeSpan(DateTime.Now.Ticks);
            while (this.IsElementPresent(element))
            {
                double totalTime = Convert.ToInt32(this.ClockTime(ts));
                if (totalTime > timeInSeconds)
                {
                    Assert.Fail(element + " was still visible after " + totalTime + " seconds");
                }
            }

            WaitForPageLoad(TimeSpan.FromSeconds(timeInSeconds));
        }
        public void WaitForElementNotPresent(string locator, double timeInSeconds = 60)
        {
            WaitForPageLoad(TimeSpan.FromSeconds(timeInSeconds));
            var ts = new TimeSpan(DateTime.Now.Ticks);
            while (this.FindElementPresent(locator) != null)
            {
                double totalTime = Convert.ToInt32(this.ClockTime(ts));
                if (totalTime > timeInSeconds)
                {
                    Assert.Fail(locator + " was still present after " + totalTime + " seconds");
                }
            }

            WaitForPageLoad(TimeSpan.FromSeconds(timeInSeconds));
        }

        public void Type(string locator, string text, bool attemptReEntry = true)
        {
            var element = this.FindElementVisible(locator);
            if (element != null)
            {
                element.Clear();
                if (this.browserName == "firefox")
                {
                    SendKeysFireFox(element, text);
                }
                else
                {
                    element.SendKeys(text);
                }

                if (attemptReEntry)
                {
                    VerifyInput(element, text);
                }
            }
            else
            {
                throw new Exception("Element " + locator + " is not found");
            }
        }

        public void Type(IWebElement el, string text, bool attemptReEntry = true)
        {
            var element = this.FindElementVisible(el);
            if (element != null)
            {   
                element.Clear();
                if (this.browserName == "firefox")
                {
                    SendKeysFireFox(element, text);
                }
                else
                {
                    element.SendKeys(text);
                }

                if (attemptReEntry)
                {
                    VerifyInput(element, text);
                }
            }
            else
            {
                //throw new Exception("Element " + locator + " is not found");
            }
        }

        public void SendKeyBoardKey(string key)
        {
            Actions action = new Actions(this._browser);
            action.SendKeys(key);
            action.Perform();
        }

        public void SendKeyBoardKey(string element, string key)
        {
            var el = this.FindElementPresent(element);
            Actions action = new Actions(this._browser);
            action.SendKeys(el, key);
            action.Perform();
        }

        public void SendKeyBoardKeys(string element, string[] keys)
        {
            var el = this.FindElementPresent(element);
            el.Clear();
            Actions action = new Actions(this._browser);
            string s = string.Empty;
            foreach (var key in keys)
            {
                s += key;
                action.SendKeys(el, s);
                WaitFor(0.1);
            }

            action.Perform();
        }

        public void SendKeyBoardNumbers(string element, int[] keys)
        {
            var el = this.FindElementPresent(element);
            el.Clear();
            Actions action = new Actions(this._browser);
            string s = string.Empty;
            foreach (var key in keys)
            {
                s = key == 1 ? s += Keys.NumberPad1 : key == 2 ? s += Keys.NumberPad2 : key == 3 ? s += Keys.NumberPad3 : key == 4 ? s += Keys.NumberPad4 : key == 5 ? s += Keys.NumberPad5 :
                     key == 6 ? s += Keys.NumberPad6 : key == 7 ? s += Keys.NumberPad7 : key == 8 ? s += Keys.NumberPad8 : key == 9 ? s += Keys.NumberPad9 : key == 0 ? s += Keys.NumberPad0 : string.Empty;
            }

            action.SendKeys(el, s);
            action.Build();
            action.Perform();
        }

        private void SendKeysFireFox(IWebElement element, string keys)
        {
            Actions action = new Actions(this._browser);
            action.SendKeys(element, keys);
            action.Perform();
        }

        private void VerifyInput(IWebElement element, string inputText)
        {
            int attempts = 0;
            while (element.GetAttribute("value") != inputText && attempts < 3)
            {
                element.Clear();
                if (this.browserName == "firefox")
                {
                    SendKeysFireFox(element, inputText);
                }
                else
                {
                    element.SendKeys(inputText);
                }
                WaitFor(0.5);
                attempts++;
            }
        }

        public void StopBrowser()
        {
            this.Browser()?.Dispose();
        }

        public string PageTitle()
        {
            return this.Browser().Title;
        }

        public bool IsTextPresent(string text)
        {
            var el = this.GetAllWebElements("//*[contains(text(),'" + text + "')]");
            if (el != null && el.Count > 0)
            {
                return true;
            }

            if (this.Browser().PageSource.Contains(text))
            {
                return true;
            }

            return false;
        }

        public bool IsTextVisible(string text)
        {
            try
            {
                var el = this.GetAllWebElements("//*[contains(text(),'" + text + "')]");
                if (el != null && el.Any(a => a.Displayed))
                {
                    return true;
                }
            }
            catch
            {
            }

            return false;
        }

        public void WaitForTextVisible(string text, double timeInSeconds = 60)
        {
            WaitForPageLoad(TimeSpan.FromSeconds(timeInSeconds));
            var ts = new TimeSpan(DateTime.Now.Ticks);
            double totalTime = this.ClockTime(ts);
            while (!this.IsTextVisible(text) && totalTime <= timeInSeconds)
            {
                totalTime = Convert.ToInt32(this.ClockTime(ts));
                if (!string.IsNullOrEmpty(CheckForErrorsOnPage()))
                {
                    Assert.Fail("UI_Error: " + CheckForErrorsOnPage());
                }

                if (totalTime >= timeInSeconds)
                {
                    Assert.Fail("Could not find visible text(-- " + text + " --)on page after " + totalTime + " seconds");
                }
            }

            WaitForPageLoad(TimeSpan.FromSeconds(timeInSeconds));
        }

        public void WaitForTextNotVisible(string text, double timeInSeconds = 60)
        {
            WaitForPageLoad(TimeSpan.FromSeconds(timeInSeconds));
            var ts = new TimeSpan(DateTime.Now.Ticks);
            double totalTime = this.ClockTime(ts);
            while (this.IsTextVisible(text) && totalTime <= timeInSeconds)
            {
                totalTime = Convert.ToInt32(this.ClockTime(ts));
                if (!string.IsNullOrEmpty(CheckForErrorsOnPage()))
                {
                    Assert.Fail("UI_Error: " + CheckForErrorsOnPage());
                }

                if (totalTime >= timeInSeconds)
                {
                    Assert.Fail("Text(-- " + text + " --) was still visible on page after " + totalTime + " seconds");
                }
            }
        }

        public void ExecuteJavascript(string js, string elementLocator)
        {
            IJavaScriptExecutor je = (IJavaScriptExecutor)this._browser;
            je.ExecuteScript(js, this.FindElementVisible(elementLocator));
        }

        public void ExecuteJavascript(string js, IWebElement element)
        {
            IJavaScriptExecutor je = (IJavaScriptExecutor)this._browser;
            je.ExecuteScript(js, element);
        }

        public void Refresh()
        {
            this._browser.Navigate().Refresh();
            this.WaitForPageLoad();
        }

        public string GetAttribute(string locator, string attribute)
        {
            return this.FindElementPresent(locator)?.GetAttribute(attribute);
        }

        public string GetAttribute(IWebElement element, string attribute)
        {
            return element?.GetAttribute(attribute);
        }

        public string GetText(string locator)
        {
            var text = this.FindElementVisible(locator)?.Text;
            return text;
        }

        public void ScrollToTop()
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)this._browser;
            js.ExecuteScript("window.scrollTo(0, 0)");
            System.Threading.Thread.Sleep(1000);
        }

        public void ScrollIntoView(string locator)
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)this._browser;
            js.ExecuteScript("arguments[0].scrollIntoView();", this.FindElementPresent(locator));
            System.Threading.Thread.Sleep(1000);
        }

        public string GetCurrentSliderValue(string locator)
        {
            return this.FindElementVisible(locator).GetAttribute("value");
        }

        public void Dispose()
        {
            try
            {
                this._browser.Dispose();
                this._browser = null;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void WaitFor(double timeInSec)
        {
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(timeInSec));
        }

        public void SwitchFrame(string locator)
        {
            this._browser.SwitchTo().Frame(this.FindElementVisible(locator));
        }

        public void OpenNewTabInBrowser()
        {
            ((IJavaScriptExecutor)this._browser).ExecuteScript("window.open();");
            this._browser.SwitchTo().Window(this._browser.WindowHandles.Last());
        }

        public void CloseCurrentWindowTab()
        {
            ((IJavaScriptExecutor)this._browser).ExecuteScript("window.close();");
        }

        public void WaitForElementClickable(string element, double timeInSeconds = 60)
        {
            WaitForPageLoad(TimeSpan.FromSeconds(timeInSeconds));
            var ts = new TimeSpan(DateTime.Now.Ticks);
            while (!this.IsElementClickable(element))
            {
                double totalTime = Convert.ToInt32(this.ClockTime(ts));
                if (!string.IsNullOrEmpty(CheckForErrorsOnPage()))
                {
                    Assert.Fail("UI_Error: " + CheckForErrorsOnPage());
                }

                if (totalTime > timeInSeconds)
                {
                    Assert.Fail(element + " was not clickable after " + totalTime + " seconds");
                }
            }
        }

        private string CheckForErrorsOnPage()
        {
            var error = GetVisibleElements("//div[contains(@class,'notice__Notice-sc-')]");
            if (error.Count > 0 && !error.Any(a => a.Text.Contains("6-digit")))
            {
                return error[0].Text;
            }

            return string.Empty;
        }

        public void WaitForElementNotClickable(string element, double timeInSeconds = 60)
        {
            WaitForPageLoad(TimeSpan.FromSeconds(timeInSeconds));
            WebDriverWait wait = new WebDriverWait(this._browser, TimeSpan.FromSeconds(0));
            var ts = new TimeSpan(DateTime.Now.Ticks);
            while (this.IsElementClickable(element))
            {
                double totalTime = Convert.ToInt32(this.ClockTime(ts));
                if (totalTime > timeInSeconds)
                {
                    Assert.Fail(element + " was clickable after " + totalTime + " seconds");
                }
            }
        }

        public void WaitForElementNotClickable(IWebElement element, double timeInSeconds = 60)
        {
            WaitForPageLoad(TimeSpan.FromSeconds(timeInSeconds));
            WebDriverWait wait = new WebDriverWait(this._browser, TimeSpan.FromSeconds(0));
            var ts = new TimeSpan(DateTime.Now.Ticks);
            while (this.IsElementClickable(element))
            {
                double totalTime = Convert.ToInt32(this.ClockTime(ts));
                if (totalTime > timeInSeconds)
                {
                    Assert.Fail(element + " was clickable after " + totalTime + " seconds");
                }
            }
        }

        public bool IsElementInteractable(IWebElement element)
        {
            try
            {
                element.Click();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return false;
        }

        public void TakeScreenshot(string fileName)
        {
            this._browser.TakeScreenshot().SaveAsFile(fileName, ScreenshotImageFormat.Png);
        }

        private ICollection<IWebElement> GetAllWebElements(string locator)
        {
            ICollection<IWebElement> el = null;
            try
            {
                if (!string.IsNullOrEmpty(locator) && locator.Contains("//"))
                {
                    el = this.Browser().FindElements(By.XPath(locator));
                }
                else
                {
                    if (this.FindElement(By.Id(locator)) != null)
                    {
                        el = this.Browser().FindElements(By.Id(locator));
                    }
                    else if (this.FindElement(By.XPath(locator)) != null)
                    {
                        el = this.Browser().FindElements(By.XPath(locator));
                    }
                    else if (this.FindElement(By.Name(locator)) != null)
                    {
                        el = this.Browser().FindElements(By.Name(locator));
                    }
                    else if (this.FindElement(By.LinkText(locator)) != null)
                    {
                        el = this.Browser().FindElements(By.LinkText(locator));
                    }
                    else if (this.FindElement(By.TagName(locator)) != null)
                    {
                        el = this.Browser().FindElements(By.TagName(locator));
                    }
                    else if (this.FindElement(By.ClassName(locator)) != null)
                    {
                        el = this.Browser().FindElements(By.ClassName(locator));
                    }
                    else if (this.FindElement(By.CssSelector(locator)) != null)
                    {
                        el = this.Browser().FindElements(By.CssSelector(locator));
                    }
                }
            }
            catch
            {
            }

            return el;
        }

        public void OpenNewTabAndSwitch(string tabTitle)
        {
            _browser.SwitchTo().NewWindow(WindowType.Tab);
            SetCurrentWindowTitle(tabTitle);
        }
        
        public void SetCurrentWindowTitle(string title)
        {
            if (!string.IsNullOrEmpty(title))
            {
                IJavaScriptExecutor executor = (IJavaScriptExecutor)this._browser;
                executor.ExecuteScript($"document.title = '{title}'");
            }
        }

        private IWebElement FindElement(By by)
        {
            this._browser.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(0);
            try
            {
                return this._browser.FindElement(by);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        private bool IsInteractable(string element)
        {
            try
            {
                this.FindElementEnabled(element).Click();
                return true;
            }
            catch
            {
            }

            return false;
        }

        private double ClockTime(TimeSpan timeSpan)
        {
            var ts2 = new TimeSpan(DateTime.Now.Ticks);
            var ts3 = timeSpan - ts2;
            return Math.Abs(ts3.TotalSeconds);
        }

        private void WaitForJQueryLoad(TimeSpan timespan)
        {
            IJavaScriptExecutor executor = (IJavaScriptExecutor)this._browser;
            var time = TimeSpan.FromSeconds(this.ClockTime(timespan));
            if ((bool)executor.ExecuteScript("return window.jQuery != undefined"))
            {
                while (!(bool)executor.ExecuteScript("return jQuery.active == 0"))
                {
                    if (time > timespan)
                    {
                        break;
                    }
                }
            }

            //// make sure page source is present
            while (!this.IsPageSourcePresent())
            {
                if (time > timespan)
                {
                    break;
                }
            }
        }
    }
}
