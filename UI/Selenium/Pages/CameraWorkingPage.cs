using OpenQA.Selenium;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI.Pages
{
    ///<summary>
    ///   CameraWorkingPage
    ///   Page element definitions
    ///   Do not add logic here
    ///</summary>
    public class CameraWorkingPage
    {
        public static By CameraYesRadioButton => By.CssSelector("label.govuk-label.govuk-radios__label");
        public static By CameraNoRadioButton => By.Id("camera-no");
        public static By Continue = By.Id("continue-btn");
    }
}