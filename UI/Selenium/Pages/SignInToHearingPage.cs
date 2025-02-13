﻿using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UISelenium.Pages
{
	///<summary>
	///   ConfirmEquipmentPage
	///   Page element definitions
	///   Do not add logic here
	///</summary>
    public class SignInToHearingPage
    {
        public static By CheckEquipmentButton => By.Id("check-equipment-btn");
        public static By SignInToHearingButton => By.XPath("//button[contains(@id,'sign-into-hearing-btn-')]");
    }
}