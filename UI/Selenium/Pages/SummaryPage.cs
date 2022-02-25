using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UISelenium.Pages
{
    public class SummaryPage
    {
        public static By BookButton = By.Id("bookButton");
        public static By DotLoader = By.Id("dot-loader");
        public static By TryAgainButton = By.Id("btnTryAgain");
        public static By SuccessTitle = By.ClassName("govuk-panel__title");
    }
}
