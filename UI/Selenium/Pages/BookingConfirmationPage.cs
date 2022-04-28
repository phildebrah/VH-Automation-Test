using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
namespace UISelenium.Pages
{
	///<summary>
	///   BookingConfirmationPage
	///   Page element definitions
	///   Do not add logic here
	///</summary>
    public class BookingConfirmationPage
    {
        public static By ViewBookingLink = By.XPath("//a[text()='View this booking']");
    }
}
