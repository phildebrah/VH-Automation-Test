using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UISelenium.Pages
{
    public class QuickLinkPage
    {
        public static By CheckEquipmentButton => By.Id("check-equipment-btn");              
        public static By EquipmentWorksButton => By.Id("equipment-works-btn");
        public static By EquipmentFaultyButton => By.Id("equipment-faulty-btn");    
        public static By signOut => By.CssSelector(".table-row");
        public static By hereLink => By.LinkText("here");
        public static By StartPrivateMeeting => By.Id("openStartPCButton");
        public static By JoinPrivateMeeting => By.Id("openJoinPCButton");
    }
}
