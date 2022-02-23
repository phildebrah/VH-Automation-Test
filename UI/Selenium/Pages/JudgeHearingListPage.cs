using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
namespace UISelenium.Pages
{
    public  class JudgeHearingListPage
    {
        public static By HealingListRow => By.XPath("//tr[@class='govuk-table__row']");
        public static By SelectButton(string conferenceId) => By.Id($"start-hearing-btn-{conferenceId}");
    }
}
