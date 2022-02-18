using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace UI.Steps.CommonActions
{
    public class CommonPageActions
    {
        IWebDriver driver;
        public CommonPageActions(IWebDriver _driver)
        {
            driver = _driver;
        }
        public bool NavigateToPage(string targetUrl, string redirectUrl = null)
        {
            
            if (!driver.Url.Contains(targetUrl))
            {
                driver.Navigate().GoToUrl(targetUrl);
            }

            if (!string.IsNullOrEmpty(redirectUrl))
            {
                return driver.Url.Contains(redirectUrl);
            }
            else
            {
                return driver.Url.Contains(targetUrl);
            }
        }

    }
}
