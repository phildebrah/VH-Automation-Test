﻿using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UISelenium.Pages
{
	///<summary>
	///   OtherInformationPage
	///   Page element definitions
	///   Do not add logic here
	///</summary>
    public class OtherInformationPage
    {
        public static By RecordAudioYes = By.Id("audio-choice-yes");
        public static By RecordAudioNo = By.Id("audio-choice-no");
        public static By OtherInfo = By.Id("details-other-information");
        public static By NextButton = By.Id(("nextButton"));
    }
}
