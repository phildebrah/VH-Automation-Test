﻿using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UISelenium.Pages
{
	///<summary>
	///   SummaryPage
	///   Page element definitions
	///   Do not add logic here
	///</summary>
    public class SummaryPage
    {
        public static By BookButton = By.Id("bookButton");
        public static By DotLoader = By.Id("dot-loader");
        public static By TryAgainButton = By.Id("btnTryAgain");
        //public static By SuccessTitle = By.ClassName("govuk-panel__title");
        public static By SuccessTitle = By.XPath("//h1[text()[contains(.,'Your hearing booking was successful')]]");

        public static By ViewThisBooking = By.LinkText("View this booking");
    }
}
