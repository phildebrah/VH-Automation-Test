using System;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using TechTalk.SpecFlow;

namespace UISelenium.Helper
{
    public static class Helper
    {
        private static NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        public static IWebElement Find(this IWebDriver driver, By by)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
            IWebElement element = null;
            wait.Until(d =>
            {
                try
                {
                    element = d.FindElement(by);

                    if (element.Displayed && element.Enabled)
                    {
                        Logger.Info(" The following element has been found " + element);
                        return element;
                    }
                }
                catch (NoSuchElementException e)
                {
                    Logger.Error(" The following error has occourred" + e);
                }
                return null;
            });
            return element;
        }

        public static IReadOnlyCollection<IWebElement> FindMultiple(this IWebDriver driver, By by)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
            IReadOnlyCollection<IWebElement> element = null;
            try
            {
                wait.Until(d =>
                {
                    try
                    {
                        element = d.FindElements(by);

                        if (element.Count > 0 && element.ElementAt(0).Displayed && element.ElementAt(0).Enabled)
                        {
                            Logger.Info(" The following element has been found " + element);
                            return element;
                        }
                    }
                    catch (NoSuchElementException e)
                    {
                        Logger.Error(" The following error has occourred " + e);
                    }
                    return null;
                });
            }
            catch (WebDriverTimeoutException e1)
            {
                Logger.Error(" The following error has occourred " + e1);
            }

            return element;
        }

       
        public static void Click(this IWebElement element)
        {
            try
            {
                element.Click();
                Logger.Info(" The following element has been clicked "+ element);
            }
            catch (Exception e)
            {
                Logger.Error(" Exception has occurred " + e);
            }
        }

        public static void SetValue(this IWebElement element, string text)
        {
            try
            {
                element.Clear();
                element.SendKeys(text);
                Logger.Info(text + " entered in the " + element + " field.");
            }
            catch (Exception e)
            {
                Logger.Error(" Exception has occurred " + e);
            }
        }

        public static void SelectCheckBox(this IWebElement element)
        {
            try
            {
                if (element.Selected)
                {
                    Logger.Info("Checkbox: " + element + "is already selected");
                }
                else
                {
                    // Select the checkbox
                    element.Click();
                    Logger.Info("Checkbox: " + element + "has selected");
                }
            }
            catch (Exception e)
            {
                Logger.Error("Unable to select the checkbox: " + element + "Error:"+ e );
            }
        }

        public static void DeSelectCheckbox(this IWebElement element)
        {
            try
            {
                if (element.Selected)
                {
                    //De-select the checkbox
                    element.Click();
                    Logger.Info("Checkbox: " + element + "has  deselected");
                }
                else
                {
                    Logger.Info("Checkbox: " + element + "is already deselected");
                }
            }
            catch (Exception e)
            {
                Logger.Error("Unable to select the checkbox: " + element + "Error:" + e);
            }
        }

        public static IWebDriver GetDriverInstance(ScenarioContext context)
        {
            var driver = (IWebDriver)context["driver"];
            return driver;
        }
    }
}
