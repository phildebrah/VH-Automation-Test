using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using SeleniumSpecFlow.Utilities;
using TechTalk.SpecFlow;

namespace UI.Steps.CommonActions
{
    [Binding]
    public class CommonPageActions
    {
        IWebDriver Driver;
        public CommonPageActions(IWebDriver _Driver)
        {
            Driver = _Driver;
        }
        public bool NavigateToPage(string targetUrl, string redirectUrl = null)
        {
            
            if (!Driver.Url.Contains(targetUrl))
            {
                Driver.Navigate().GoToUrl(targetUrl);
            }

            if (!string.IsNullOrEmpty(redirectUrl))
            {
                return Driver.Url.Contains(redirectUrl);
            }
            else
            {
                return Driver.Url.Contains(targetUrl);
            }
        }

        


    }
}
