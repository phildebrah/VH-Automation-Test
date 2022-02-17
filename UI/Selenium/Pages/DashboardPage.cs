using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UISelenium.Pages
{
    public class DashboardPage
    {
        public static By BookHearingButton = By.Id("bookHearingBtn");
        public static By QuestionnaireResultsButton = By.Id("questionnaireResultsBtn");
        public static By GetAudioFileLinkButton = By.Id("getAudioLinkBtn");
        public static By ChangeUserPasswordButton = By.Id("changePasswordBtn");
        public static By DeleteParticipantButton = By.Id("deleteUserBtn");
        public static By EditPparticipantNameButton = By.Id("editUserBtn");
    }
}
