using RestSharpApi.Steps;

using TechTalk.SpecFlow;

using UI.Utilities;

namespace UI.Steps.CommonActions
{
    internal class BookHearings : ObjectFactory
    {
        public BookHearings(ScenarioContext context) : base(context)
        {
        }

        [Given(@"That I am booking a hearing")]
        public void GivenThatIAmBookingAHearing(Table table)
        {
            throw new PendingStepException();
        }

        [Given(@"the hearing is scheduled as")]
        public void GivenTheHearingIsScheduledAs(Table table)
        {
            throw new PendingStepException();
        }

        [Given(@"the hearing has a judge assigned")]
        public void GivenTheHearingHasAJudgeAssigned(Table table)
        {
            throw new PendingStepException();
        }

        [Given(@"the hearing has a number of participants have been added")]
        public void GivenTheHearingHasANumberOfParticipantsHaveBeenAdded(Table table)
        {
            throw new PendingStepException();
        }

        [Given(@"the hearing video access point has been set")]
        public void GivenTheHearingVideoAccessPointHasBeenSet(Table table)
        {
            throw new PendingStepException();
        }

        [Given(@"the hearing has more information")]
        public void GivenTheHearingHasMoreInformation(Table table)
        {
            throw new PendingStepException();
        }

        [Given(@"the hearing is booked and confirmed")]
        public void GivenTheHearingIsBookedAndConfirmed()
        {
            throw new PendingStepException();
        }

    }
}
