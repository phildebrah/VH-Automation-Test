using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UISelenium.Pages
{
    public class StaffMemberHearingListPage
    {
        public static By HealingListRow => By.XPath("//tr[@class='govuk-table__row']");
    }
}
