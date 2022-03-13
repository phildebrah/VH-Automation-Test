using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UISelenium.Pages
{
    public class SelectHearingPage
    {
        public static By Quicklinks => By.CssSelector("fa-icon");
        public static By QuicklinkCopy => By.XPath("//a[text()='Copy join by quick link details to clipboard']");
        public static By Hearingidtoclipboard => By.XPath("//a[text()='Copy hearing ID to clipboard']");
        public static By Phonetoclipboard => By.XPath("//a[text()='Copy joining by phone details to clipboard']");
        public static By filters => By.CssSelector("#filters-court-rooms");
        public static By NewMessageBox => By.Id("new-message-box");
        public static By UnreadMsgBtn => By.XPath("//app-unread-messages/img");
        public static By UnreadMsgPartBtn => By.XPath("//app-unread-messages-participant/img");
    }
}
