﻿using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UISelenium.Pages
{
	///<summary>
	///   SelectYourHearingListPage
	///   Page element definitions
	///   Do not add logic here
	///</summary>
    public class SelectYourHearingListPage
    {
        public static By HearingList => By.CssSelector("input[aria-autocomplete='list']");
        public static By HearingCheckBox => By.CssSelector("input[type='checkbox']");
        public static By ViewHearings => By.CssSelector("#select-venue-allocation-btn");
        public static By SelectCaseNumber(string caseNumber) => By.XPath($"//div[contains(text(),'{caseNumber}')]");
        public static By AlertMsg(string rowNum) => By.CssSelector($"div#tasks-list div.govuk-grid-row:nth-child({rowNum}) .task-body");
        public static By FirstLastName(string rowNum) => By.CssSelector($"div#tasks-list div.govuk-grid-row:nth-child({rowNum}) .task-origin");
        public static By HearingBtn => By.Id("hearingsTabButton");
        public static By FailedAlert => By.CssSelector("div#tasks-list div.govuk-grid-row");
    }
}