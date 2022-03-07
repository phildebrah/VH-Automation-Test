using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UISelenium.Pages
{
    public class QuickLinkPage
    {
        public static By HearingList => By.CssSelector("input[aria-autocomplete='list']");
        public static By HearingCheckBox => By.CssSelector("input[type='checkbox']");
        public static By ViewHearings => By.CssSelector("#select-venue-allocation-btn");
    }
}
