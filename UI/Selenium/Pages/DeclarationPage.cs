using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UISelenium.Pages
{
    public class DeclarationPage
    {
        public static By DeclarationCheckBox => By.CssSelector("label.govuk-label.govuk-checkboxes__label");
        public static By DeclarationContinueBtn => By.Id("nextButton");

    }
}
