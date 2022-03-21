using System;
using TechTalk.SpecFlow;

namespace RestSharpApi
{
    [Binding]
    public class UserApiStepDefinitions
    {
        [When(@"I ask for judges with the name <JudgeName>")]
        public void WhenIAskForJudgesWithTheNameJudgeName(Table table)
        {
            throw new PendingStepException();
        }
    }
}
