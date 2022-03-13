using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UISelenium.Pages
{
    public class MicroPhoneWorkingPage
    {
        public static By MicrophoneYesRadioBUtton => By.CssSelector("label.govuk-label.govuk-radios__label");
        public static By MicrophoneNoRadioBUtton => By.Id("microphone-no");
        public static By Continue = By.Id("continue-btn");


    }
}
