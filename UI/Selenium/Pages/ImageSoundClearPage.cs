using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UISelenium.Pages
{
	///<summary>
	///   ImageSoundClearPage
	///   Page element definitions
	///   Do not add logic here
	///</summary>
    public class ImageSoundClearPage
    {
        public static By VideoYesRadioButton => By.CssSelector("label.govuk-label.govuk-radios__label");
        public static By VideoNoRadioButton => By.Id("video-no");
        public static By Continue = By.Id("continue-btn");
    }
}