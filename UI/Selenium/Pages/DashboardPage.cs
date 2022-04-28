using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UISelenium.Pages
{
	///<summary>
	///   DashboardPage
	///   Page element definitions
	///   Do not add logic here
	///</summary>
    public class DashboardPage
    {
        public static By BookHearingButton = By.Id("bookHearingBtn");
        public static By QuestionnaireResultsButton = By.Id("questionnaireResultsBtn");
        public static By GetAudioFileLinkButton = By.Id("getAudioLinkBtn");
        public static By ChangeUserPasswordButton = By.Id("changePasswordBtn");
        public static By DeleteParticipantButton = By.Id("deleteUserBtn");
        public static By EditPparticipantNameButton = By.Id("editUserBtn");
        public static By QuestionairVHTable = By.Id("vh-table");
        public static By CaseNumber = By.Id("caseNumber");
        public static By UserName = By.Id("userName");
        public static By ContactEmail = By.Id("contactEmail");
        
    }
}
