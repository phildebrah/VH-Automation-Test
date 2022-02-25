using TechTalk.SpecFlow;
namespace UI.Steps
{
    class AdminWebLogin
    {
        [Given(@"I want to creat a new hearing with Judge, (.*) Interpreter, (.*) complainant, (.*) respondant, (.*) VHO, (.*) representative")]
        public void GivenIWantToCreatANewHearingWithJudgeInterpreterComplainantRespondantVHORepresentative(int p0, int p1, int p2, int p3, int p4)
        {
            ScenarioContext.Current.Pending();
        }

        [When(@"I start a hearing")]
        public void WhenIStartAHearing()
        {
            ScenarioContext.Current.Pending();
        }

        [Then(@"all the attendees will be seen")]
        public void ThenAllTheAttendeesWillBeSeen()
        {
            ScenarioContext.Current.Pending();
        }

    }
}
