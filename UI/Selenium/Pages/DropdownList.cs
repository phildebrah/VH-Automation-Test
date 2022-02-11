using OpenQA.Selenium;
using System.Collections.Generic;
using System.Linq;
using UISelenium.Helper;

namespace UISelenium.Pages
{
    public class DropdownList
    {
        public IWebDriver WebDriver { get; }
        public DropdownList(IWebDriver webDriver)
        {
            WebDriver = webDriver;
        }
        private IList<IWebElement> Options => (IList<IWebElement>)WebDriver.FindMultiple(By.XPath("//select[@id='dropdown']//option"));

        public IWebElement SelectedDropDown => WebDriver.Find(By.XPath("//select[@id='dropdown']//option[@selected='selected']"));

        public void SelectDropdownValue(string option)
        {
            Options.Where(a => a.Text.Equals(option)).FirstOrDefault().Click();
        }
    }
}
