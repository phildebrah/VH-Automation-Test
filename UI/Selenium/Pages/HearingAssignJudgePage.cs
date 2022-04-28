using OpenQA.Selenium;

namespace UISelenium.Pages
{
	///<summary>
	///   HearingAssignJudgePage
	///   Page element definitions
	///   Do not add logic here
	///</summary>
    public class HearingAssignJudgePage
    {
        public static By JudgeEmail = By.Id("judge-email");
        public static By JudgeDisplayNameFld = By.Id("judgeDisplayNameFld");
        public static By JudgePhoneFld = By.Id("judgePhoneFld");
        public static By NextButton = By.Id("nextButton");
        public static By SearchResults = By.Id("search-results-list");
    }
}
