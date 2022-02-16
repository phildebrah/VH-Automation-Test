using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestFramework.Drivers;

namespace UI.Steps.CommonActions
{
    public class CommonPageActions
    {
        IDriver driver;
        public CommonPageActions(IDriver _driver)
        {
            driver = _driver;
        }
        public bool NavigateToPage(string targetUrl, string redirectUrl = null)
        {
            if (!driver.GetUrl().Contains(targetUrl))
            {
                driver.Navigate(targetUrl);
                /// driver.OpenNewTabAndSwitch("this is a test");
            }

            if (!string.IsNullOrEmpty(redirectUrl))
            {
                return driver.GetUrl().Contains(redirectUrl);
            }
            else
            {
                return driver.GetUrl().Contains(targetUrl);
            }
        }
    }
}
