using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI.Pages
{
    public class VideoAccessPointsPage
    {
        public static By DisplayName(int number) => By.Id($"displayName{number}");
        public static By DefenceAdvocate(int number) => By.Id($"defenceAdvocate{number}");
        public static By RemoveDisplayName(int number) => By.Id($"removeDisplayName{number}");
        public static By AddAnotherBtn = By.Id("addEndpoint");
        
    }
}
