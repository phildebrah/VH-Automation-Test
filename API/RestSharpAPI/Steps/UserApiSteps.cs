using RestSharpApi.Hooks;
using RestSharpApi.ApiHelpers;
using System;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using TechTalk.SpecFlow;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Bookings;
using Notification;
using User;
using Video;

namespace RestSharpApi.Steps
{
    [Binding]
    public class UserApiSteps : RestSharpHooks

    {
        private readonly ITokenProvider _tokenProvider;
        private static string _baseUrl = "https://vh-bookings-api-dev.azurewebsites.net";
        private static string _resourceId = "https://vh-bookings-api-dev.azurewebsites.net";
        private static string _clientid = "004a4a9f-a455-4e0c-861b-29de373892c4";
        private static string _clientSecret = "0yWbsHF-9VPb1R6sdCtC.7licMj6NADCWQ";
        private static string _tenetid = "fb6e0e22-0da3-4c35-972a-9d61eb256508";
        private static string _authority = "https://login.microsoftonline.com/";
        private static BookingsApi BookingApiService;

        ScenarioContext scenarioContext;

        UserApiSteps()
        {
            var _client = new HttpClient();
            _client.DefaultRequestHeaders.Accept
                .Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", GetServiceToServiceToken());
             BookingApiService = new BookingsApi(_client);
            BookingApiService.BaseUrl = "https://vh-bookings-api-dev.azurewebsites.net/";
        }

        [Given(@"I have a userApi")]
        public async void GivenIHaveAUserApi()
        {
            var _baseUrl = "https://vh-bookings-api-dev.azurewebsites.net";
            var _client = new HttpClient();
            _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer","");
            var BookingApiService = new BookingsApi(_client);
            BookingApiService.BaseUrl = _baseUrl;
            var health1 = await BookingApiService.CheckServiceHealthAsync();
            var drop = health1.App_version;
            _logger.Info("test");
            _logger.Info($"Drop is {drop}");

            var NotificationApiService = new NotificationApi(new HttpClient());
            NotificationApiService.BaseUrl = "https://vh-notification-api-dev.azurewebsites.net";
            try
            {
                NotificationApiService.CheckServiceHealthAuthAsync();
            }
            catch (Exception e)
            {
                var eee = e.Message;
                throw;
            }
            //

            var UserApiService = new UserApi(new HttpClient());
            UserApiService.BaseUrl = "https://vh-user-api-dev.azurewebsites.net";
            try
            {
                UserApiService.CheckServiceHealthAsync();
            }
            catch (Exception e)
            {
                var ee = e.Message;
                throw;
            }

            var VideoApiService = new VideoApi(new HttpClient());
            VideoApiService.BaseUrl = "https://vh-video-api-dev.azurewebsites.net";
            try
            {
                var health4 = VideoApiService.CheckServiceHealthAsync();
            }
            catch (Exception e)
            {
                var ee = e.Message;
                throw;
            }
            //var last = health4.App_version;
            Object caseTypes;

            // Did all the health checks, now test
            try
            {
                caseTypes = BookingApiService.GetCaseTypesAsync();
            }
            catch (Exception e)
            {
                var ee = e.Message;
                throw;
            }
            
            var cases = caseTypes.ToString();
            Console.WriteLine(cases);
            Object Judges;
            try
            {
                Judges =  UserApiService.GetJudgesAsync();

            }
            catch (Exception e)
            {
                var ee = e.Message;
                throw;
            }
            var ss = VideoApiService.GetConferencesTodayForIndividualByUsernameAsync("Judge");
        }

        [When(@"I check its health")]
        public void WhenICheckItsHealth()
        {
            throw new PendingStepException();
        }

        [Then(@"I see that it is sucessful")]
        public void ThenISeeThatItIsSucessful()
        {
            throw new PendingStepException();
        }

        [When(@"I book a hearing")]
        public void WhenIBookAHearing()
        {

            BookAHearing();
        }
        public async Task BookAHearing()
        {
            _logger.Info("MK is Booking a hearing");
            BookNewHearingRequest _request = new BookNewHearingRequest();
            ////_request.Case_type_name = "Civil";
            //_request.Scheduled_duration = 60;
            //_request.Hearing_venue_name = "Birmingham Civil and Family Justice Centre";
            HearingDetailsResponse _response = new HearingDetailsResponse();
            try
            {
                _logger.Info("making request");
                _response = await BookingApiService.BookNewHearingAsync(_request);
                _logger.Info($"response is {_response.Id}");

            }
            catch (Exception e)
            {
                _logger.Info(e.Message);
                _logger.Info($" exception type {e.GetType().Name}");
                throw;
            }
            _logger.Info($"Booking ended");
        }

protected string GetServiceToServiceToken()
        {
            AuthenticationResult result;
            var credential = new ClientCredential(_clientid, _clientSecret);
            var authContext = new AuthenticationContext($"{_authority}{_tenetid}");

            try
            {
                result = authContext.AcquireTokenAsync(_resourceId, credential).Result;
            }
            catch (AdalException)
            {
                throw new UnauthorizedAccessException();
            }

            return result.AccessToken;
        }
    [When(@"I get case Types")]
    public async Task WhenIGetCaseTypes()
    {
        _logger.Info("Looking for Case Types");
            _logger.Info("making request");
            var cases = await BookingApiService.GetCaseTypesAsync(System.Threading.CancellationToken.None);
                foreach (var caseType in cases)
                {
                    _logger.Info($" Case found {caseType.Name}, {caseType.Id}, {caseType.GetType}");
                }
        _logger.Info($"Case Type List found");
    }

        [When(@"I get venues")]
        public async Task WhenIGetVenues()
        {
            _logger.Info("Looking for Venues");
            var venues = await BookingApiService.GetHearingVenuesAsync(System.Threading.CancellationToken.None);
            _logger.Info($"Venues found {venues}");
            foreach (var venue in venues)
                _logger.Info($"venue id {venue.Id} is {venue.Name}");
        }

        [When(@"I ask for judges with the name ""([^""]*)""")]
        public async Task WhenIAskForJudgesWithTheName(string judgename)
        {
            _logger.Info($"Looking for Judges starting with {judgename}");
            SearchTermRequest searchTermRequest = new SearchTermRequest();
            searchTermRequest.Term = judgename;
            var judges = await BookingApiService.PostJudiciaryPersonBySearchTermAsync(searchTermRequest);
            _logger.Info($"Judges found {judges}");
            foreach (var judge in judges)
                _logger.Info($"judge id {judge.Id} is {judge.Username}, {judge.Title}, {judge.First_name}, {judge.Last_name}");
        }

        [When(@"I get anonymous data")]
        public async Task WhenIGetAnonymousData()
        {
            _logger.Info("Looking for Anonymous data");
            StringBuilder sb = new StringBuilder();
            AnonymisationDataResponse bigList;
            try
            {
             bigList = await BookingApiService.GetAnonymisationDataAsync();

            }
            catch (ApiException)
            {

                throw;
            }
            foreach (var big in bigList.Usernames)
            {
                _logger.Info($"found user {big}");
            }
            foreach (var big in bigList.Hearing_ids)
            {
                _logger.Info($"found hearing {big}");
                await getHearings(big);
            }
        }

        public async Task getHearings(Guid hearingId)
        {
            _logger.Info($"Looking for Hearing data for Hearing {hearingId.ToString()}");
            StringBuilder sb2 = new StringBuilder();
            var _client2 = new HttpClient();
            _client2.DefaultRequestHeaders.Accept
                .Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _client2.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", GetServiceToServiceToken());
            BookingsApi BookingApiService2 = new BookingsApi(_client2);
            BookingApiService2.BaseUrl = "https://vh-bookings-api-dev.azurewebsites.net/";
            //HearingDetailsResponse h = new HearingDetailsResponse();
            //try
            //{
            //    h = await BookingApiService.GetHearingDetailsByIdAsync(hearingId);
            //}
            //catch (ApiException d)
            //{

            //    throw;
            //}
            //_logger.Info($"Hearing {h.Id}, Created by {h.Created_by}, {h.ToString()}");

            //var f = h.Participants;
            try
            {
            var f = await BookingApiService2.GetHearingDetailsByIdAsync(hearingId);
            foreach (var participant in f.Participants)
            {
                _logger.Info($"looking at participant {participant.Username}, {participant.GetType}, {participant.First_name} {participant.Last_name}");
                sb2.Append($"participant {participant.Username}, {participant.GetType}, {participant.First_name} {participant.Last_name}");
            }
            }
            catch (Exception)
            {

                throw;
            }
            return;
        }
    }

}

