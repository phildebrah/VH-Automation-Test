using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UISelenium.Pages
{
    public class GetAudioFilePage
    {
        public static By HearingAudioFileRadio => By.Id("search-choice-vhfile");
        public static By CvpAudioFileRadio => By.Id("search-choice-cvpfile");
        public static By CaseNumberInput => By.Id("caseNumber");
        public static By SearchButton => By.Id("submit");
        public static By GetLinkButton(string caseId) => By.XPath($"//div[text[contains('{caseId}')]]//button[@id='getLinkButton']");
        public static By CopyLinkButton(string caseId) => By.XPath($"//div[text()[contains('{caseId}')]]//button[contains(@id,'copyLinkButton')]");
        public static By LinkCopiedMessage => By.XPath("//div[@class='linkCopiedMessage']");
    }
}
