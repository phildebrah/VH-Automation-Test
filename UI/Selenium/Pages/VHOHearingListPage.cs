using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UISelenium.Pages
{
    public class VHOHearingListPage
    {
        public static By HealingListRow => By.XPath("//tr[@class='govuk-table__row']");
        public static By ParticipantName => By.XPath("//*[contains(@Id,'participant-contact-details-link')]");
        public static By ParticipantStatus => By.XPath("//div[@class='govuk-grid-column-one-half']/*[contains(@Id,'participant-status')]");
    }
}
