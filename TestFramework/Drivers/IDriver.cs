using OpenQA.Selenium;
using System;
using System.Collections.Generic;

namespace TestFramework.Drivers
{
    public interface IDriver
    {
        /// <summary>
        /// IwebDriver
        /// </summary>
        /// <returns>Driver instance </returns>
        IWebDriver Browser();

        /// <summary>
        /// Terminates brower and Driver session
        /// </summary>
        void StopBrowser();

        /// <summary>
        /// Starts a new browser instance / session
        /// </summary>
        /// <param name="browser">browser, chrome is default if no browser is specified</param>
        void StartBrowser(string browser = "chrome", string path = "");

        /// <summary>
        /// Clicks on a specified page element
        /// </summary>
        /// <param name="locator">element identifier</param>
        void Click(string locator);

        /// <summary>
        /// Clicks All Visible elements on the page
        /// </summary>
        /// <param name="locator">element identifier</param>
        void ClickAllVisible(string locator);
        /// <summary>
        /// Clicks on all elements present
        /// </summary>
        /// <param name="locator"></param>
        void ClickAllPresent(string locator);

        /// <summary>
        /// Waits For Page load
        /// </summary>
        /// <param name="span">TimeSpan, default is 60 sec</param>
        void WaitForPageLoad(TimeSpan? span = null);

        /// <summary>
        /// Navigates to spacified Url
        /// </summary>
        /// <param name="url">Url</param>
        void Navigate(string url);

        /// <summary>
        /// finds a visible element
        /// </summary>
        /// <param name="locator"> page element</param>
        /// <returns>Element if visible else null</returns>
        IWebElement FindElementVisible(string locator);
        /// <summary>
        /// finds element present
        /// </summary>
        /// <param name="locator"></param>
        /// <returns>IwebElement if present else null</returns>
        IWebElement FindElementPresent(string locator);
        /// <summary>
        /// Returns true if element is visible on the page
        /// </summary>
        /// <param name="locator">Page Element</param>
        /// <returns>True if element is visible on UI else false</returns>
        bool IsElementVisible(string locator);

        /// <summary>
        /// Returns true if element is enabled on the page
        /// </summary>
        /// <param name="locator"></param>
        /// <returns>True if element is enabled on UI else false</returns>
        bool IsElementEnabled(string locator, bool testInteraction = false);

        /// <summary>
        /// Selects item from drop down list, selects by text by default
        /// </summary>
        /// <param name="locator">name of the page element</param>
        /// <param name="item">item to select</param>
        /// <param name="byValue">select item by css value</param>
        /// <param name="byIndex">select item by item index</param>
        void SelectFromDropDownList(string locator, string item, bool byValue = false, bool byIndex = false);

        /// <summary>
        /// waits until specified element is visible
        /// </summary>
        /// <param name="locator">locator</param>
        /// <param name="timeInSeconds"> default timeout in seconds is 60 sec</param>
        void WaitForElementVisible(string locator, double timeInSeconds = 60);

        /// <summary>
        /// Type text
        /// </summary>
        /// <param name="locator">page element</param>
        /// <param name="textToSend">string to enter into a page element</param>
        void Type(string locator, string textToSend, bool attemptReEntry = true);

        void Type(IWebElement el, string textToSend, bool attemptReEntry = true);

        /// <summary>
        /// Sends special keyboard keys like Tab/Entr
        /// </summary>
        /// <param name="key"></param>
        void SendKeyBoardKey(string key);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="element"></param>
        /// <param name="keys"></param>
        void SendKeyBoardKeys(string element, string[] keys);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="element"></param>
        /// <param name="key"></param>
        void SendKeyBoardKey(string element, string key);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="element"></param>
        /// <param name="keys"></param>
        void SendKeyBoardNumbers(string element, int[] keys);

        /// <summary>
        /// Gets the current page tittle
        /// </summary>
        /// <returns>returns page title name</returns>
        string PageTitle();

        /// <summary>
        /// Gets a list of all visible elements by specified element locator
        /// </summary>
        /// <param name="locator">page element</param>
        /// <returns>returns all visible page elements</returns>
        IList<IWebElement> GetVisibleElements(string locator);

        /// <summary>
        /// Moves to specified page element
        /// </summary>
        /// <param name="locator">page element</param>
        void MoveToElement(string locator);

        /// <summary>
        /// Checks if specified text is present
        /// </summary>
        /// <param name="text">Text as string to check</param>
        /// <returns>returns true if text is present</returns>
        bool IsTextPresent(string text);

        /// <summary>
        /// Executes js
        /// </summary>
        /// <param name="jsString">jsString</param>
        /// <param name="locator">locator</param>
        void ExecuteJavascript(string jsString, string locator);

        /// <summary>
        /// Refresh the page
        /// </summary>
        void Refresh();

        /// <summary>
        /// Gets a specified attribute of the element
        /// </summary>
        /// <param name="locator">locator</param>
        /// <param name="attribute">attribute</param>
        /// <returns>element attribute</returns>
        string GetAttribute(string locator, string attribute);

        /// <summary>
        /// Gets a specified attribute of the element
        /// </summary>
        /// <param name="element">element</param>
        /// <param name="attribute">attribute</param>
        /// <returns>element attribute</returns>
        string GetAttribute(IWebElement element, string attribute);

        /// <summary>
        /// Gets element text
        /// </summary>
        /// <param name="locator">locator</param>
        /// <returns>text from element</returns>
        string GetText(string locator);

        /// <summary>
        /// current browser
        /// </summary>
        /// <returns>Returns current browser</returns>
        string BrowserName();

        /// <summary>
        /// scrolls to top of the page
        /// </summary>
        void ScrollToTop();

        /// <summary>
        /// scrolls specified element into view
        /// </summary>
        /// <param name="locator">locator</param>
        void ScrollIntoView(string locator);

        /// <summary>
        /// stops driver and disposes all it's association
        /// </summary>
        void Dispose();

        /// <summary>
        /// Waita For Element to be Enabled
        /// </summary>
        /// <param name="locator">locator </param>
        /// <param name="timeInSeconds">time in sec</param>
        void WaitForElementEnabled(string locator, double timeInSeconds = 60);

        /// <summary>
        /// Finds Element Enabled
        /// </summary>
        /// <param name="locator">locator</param>
        /// <returns>returns specified enabled element </returns>
        IWebElement FindElementEnabled(string locator);
        /// <summary>
        /// Explicitly waits for a specifield number of sec
        /// </summary>
        /// <param name="timeInSec"></param>
        void WaitFor(double timeInSec);
        /// <summary>
        /// Checks if text is visible or no
        /// </summary>
        /// <param name="text"></param>
        /// <returns>boolean</returns>
        bool IsTextVisible(string text);
        /// <summary>
        /// Waits until element is not visible
        /// </summary>
        /// <param name="locator"></param>
        /// <param name="timeInSeconds"></param>
        void WaitForElementNotVisible(string locator, double timeInSeconds = 60);

        /// <summary>
        /// Waits until element is not present
        /// </summary>
        /// <param name="locator"></param>
        /// <param name="timeInSeconds"></param>
        void WaitForElementNotPresent(string locator, double timeInSeconds = 60);

        /// <summary>
        /// Waits until element is not present
        /// </summary>
        /// <param name="locator"></param>
        /// <param name="timeInSeconds"></param>
        void WaitForElementNotPresent(IWebElement element, double timeInSeconds = 60);

        /// <summary>
        /// Selects item from dropdown list
        /// </summary>
        /// <param name="locator"></param>
        /// <returns></returns>
        List<IWebElement> GetDropDownListOptions(string locator);

        /// <summary>
        /// Waits until a specific text is visble on a page
        /// </summary>
        /// <param name="text"></param>
        /// <param name="timeInSeconds"></param>
        void WaitForTextVisible(string text, double timeInSeconds = 60);

        /// <summary>
        /// Gets current url string
        /// </summary>
        /// <returns>Active window url</returns>
        string GetUrl();
        /// <summary>
        /// Waits for element to be clickable
        /// </summary>
        /// <param name="element"></param>
        /// <param name="timeSpan"></param>
        void WaitForElementClickable(string element, double timeSpan = 60);

        /// <summary>
        /// Checks if element is clickable
        /// </summary>
        /// <param name="element"></param>
        /// <returns>boolean</returns>
        bool IsElementClickable(IWebElement element);

        /// <summary>
        /// Checks if element is clickable
        /// </summary>
        /// <param name="locator"></param>
        /// <returns>boolean</returns>
        bool IsElementClickable(string locator);

        /// <summary>
        /// Waits untill element is not clickable
        /// </summary>
        /// <param name="element"></param>
        /// <param name="timeInSeconds"></param>
        void WaitForElementNotClickable(string element, double timeInSeconds = 60);

        /// <summary>
        /// Waits untill element is not clickable
        /// </summary>
        /// <param name="element"></param>
        /// <param name="timeInSeconds"></param>
        void WaitForElementNotClickable(IWebElement element, double timeInSeconds = 60);

        /// <summary>
        /// Waits untill element is not visible
        /// </summary>
        /// <param name="element"></param>
        /// <param name="timeInSeconds"></param>
        void WaitForElementNotVisible(IWebElement element, double timeInSeconds = 60);

        /// <summary>
        /// Checks if element is visible
        /// </summary>
        /// <param name="element"></param>
        /// <returns>boolean</returns>
        bool IsElementVisible(IWebElement element);

        /// <summary>
        /// Checks if element is present on the page
        /// </summary>
        /// <param name="locator"></param>
        /// <returns>boolean</returns>
        bool IsElementPresent(string locator);

        /// <summary>
        /// Checks if element is present on the page
        /// </summary>
        /// <param name="locator"></param>
        /// <returns>boolean</returns>
        bool IsElementPresent(IWebElement element);

        /// <summary>
        /// Waits for a specific text not to be visible
        /// </summary>
        /// <param name="text"></param>
        /// <param name="timeInSeconds"></param>
        void WaitForTextNotVisible(string text, double timeInSeconds = 60);

        /// <summary>
        /// Waits for an element to be present on the page
        /// </summary>
        /// <param name="locator"></param>
        /// <param name="timeInSeconds"></param>
        void WaitForElementPresent(string locator, double timeInSeconds = 60);

        void TakeScreenshot(string fileName);

        void OpenNewTabAndSwitch(string tabName = "");
        void SetCurrentWindowTitle(string title);
    }
}
