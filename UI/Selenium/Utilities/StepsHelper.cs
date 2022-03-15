using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
namespace UI.Utilities
{
    internal class StepsHelper
    {
        public abstract class Set
        {
            public static Table HearingDetailsData()
            {
                var table = new Table(new string[] { "Case Number", "Case Name", "Case Type", "Hearing Type" });
                Dictionary<string, string> data = new Dictionary<string, string>()
                {
                    ["Case Number"] = "AA",
                    ["Case Name"] = "AutomationTestCaseName",
                    ["Case Type"] = "Civil",
                    ["Hearing Type"] = "Enforcement Hearing"
                };
                table.AddRow(data);

                return table;
            }

            public static Table HearingScheduleData()
            {
                var table = new Table(new string[] { "Duration Hour", "Duration Minute" });
                var data = new Dictionary<string, string>()
                {
                    ["Duration Hour"] = "0",
                    ["Duration Minute"] = "30"
                };
                table.AddRow(data);

                return table;
            }

            public static Table JudgeData()
            {
                var table = new Table(new string[] { "Judge or Courtroom Account" });
                var data = new Dictionary<string, string>()
                {
                    ["Judge or Courtroom Account"] = "auto_aw.judge_02@hearings.reform.hmcts.net"
                };
                table.AddRow(data);

                return table;
            }

            public static Table ParticipantsData()
            {
                var table = new Table(new string[] { "Party", "Role", "Id" });
                var data = new Dictionary<string, string>()
                {
                    ["Party"] = "Claimant",
                    ["Role"] = "Litigant in person",
                    ["Id"] = "auto_vw.individual_05@hearings.reform.hmcts.net",
                };
                table.AddRow(data);

                return table;
            }

            public static Table VideoAccessPointData()
            {
                var table = new Table(new string[] { "Display Name", "Advocate" });
                var data = new Dictionary<string, string>()
                {
                    ["Display Name"] = "",
                    ["Advocate"] = ""
                };
                table.AddRow(data);

                return table;
            }

            public static Table SetOtherInfoData()
            {
                var table = new Table(new string[] { "Record Hearing", "Other information" });
                var data = new Dictionary<string, string>()
                {
                    ["Record Hearing"] = "",
                    ["Other information"] = "accessiblility test"
                };
                table.AddRow(data);

                return table;
            }
        }
       
    }
}
