using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UISelenium.Pages
{
    public class JoinYourHearingPage
    {
        public static By FullName => By.Id("full-name");
        public static By QuickLinkParticipant => By.CssSelector("#QuickLinkParticipant-item-title");
        public static By QuickLinkObserver => By.Id("QuickLinkObserver");
        public static By ContinueButton => By.Id("continue-button");
    }
}
