using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UISelenium.Pages
{
    public class SelectYourHearingListPage
    {
        public static By HearingList => By.CssSelector("input[aria-autocomplete='list']");
        public static By HearingCheckBox => By.CssSelector("input[type='checkbox']");
        public static By ViewHearings => By.CssSelector("#select-venue-allocation-btn");
        public static By SelectCaseNumber(string caseNumber) => By.XPath($"//div[contains(text(),'{caseNumber}')]");
        public static By HearingBtn => By.Id("hearingsTabButton");
        public static By FailedAlert => By.CssSelector("div#tasks-list div.govuk-grid-row");
    }
}