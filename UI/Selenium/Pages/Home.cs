using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;
using UISelenium.Helper;

namespace UISelenium.Pages
{
    public class Home
    {
        public IWebDriver WebDriver { get; }

        public Home(IWebDriver webDriver)
        {
            WebDriver = webDriver;
        }

        private IWebElement SelectDropDown => WebDriver.Find(By.XPath("//a[normalize-space()='Dropdown']"));
        private IWebElement SelectMultipleWindows => WebDriver.Find(By.LinkText("Multiple Windows"));
        private IWebElement FormAuthentication => WebDriver.Find(By.LinkText("Form Authentication"));
        private IWebElement AddRemoveElement=> WebDriver.Find(By.LinkText("Add/Remove Elements"));

        public void ClickAddRemoveElement() => AddRemoveElement.Click();
        public void ClickFormAuthentication() => FormAuthentication.Click();
        public void ClickDropDown() => SelectDropDown.Click();
        public void ClickMultipleWindows() => SelectMultipleWindows.Click();
       
    }
}
