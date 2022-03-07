using RestSharpApi.Hooks;
using RestSharpApi.ApiHelpers;
using System;
using System.Net.Http;
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
        string _baseUrl = "https://vh-bookings-api-dev.azurewebsites.net";
        string _resourceId = "https://vh-bookings-api-dev.azurewebsites.net";
        string _clientid = "004a4a9f-a455-4e0c-861b-29de373892c4";
        string _clientSecret = "0yWbsHF-9VPb1R6sdCtC.7licMj6NADCWQ";
        string _tenetid = "fb6e0e22-0da3-4c35-972a-9d61eb256508";
        string _authority = "https://login.microsoftonline.com/";

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
        public async void WhenIBookAHearing()
        {
            _logger.Info("MK is Booking a hearing");
            var _client = new HttpClient(); 
            _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", GetServiceToServiceToken());
            var BookingApiService = new BookingsApi(_client);
            BookingApiService.BaseUrl = "https://vh-bookings-api-dev.azurewebsites.net/";
            BookNewHearingRequest _request = new BookNewHearingRequest();
            _request.Case_type_name = "Civil";
            _request.Scheduled_date_time = DateTime.Now.AddMinutes(3);
            _request.Scheduled_duration = 60;
            _request.Hearing_venue_name = "Birmingham Civil and Family Justice Centre";
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
    public async void WhenIGetCaseTypes()
    {
        _logger.Info("Looking for Case Types");
        var _client = new HttpClient();
        _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", GetServiceToServiceToken());
        var BookingApiService = new BookingsApi(_client);
        BookingApiService.BaseUrl = "https://vh-bookings-api-dev.azurewebsites.net/";
        //BookNewHearingRequest _request = new BookNewHearingRequest();
        //_request.Case_type_name = "Civil";
        //_request.Scheduled_date_time = DateTime.Now.AddMinutes(3);
        //_request.Scheduled_duration = 60;
        //_request.Hearing_venue_name = "Birmingham Civil and Family Justice Centre";
        //HearingDetailsResponse _response = new HearingDetailsResponse();
        //try
        //{
            _logger.Info("making request");
            var cases = await BookingApiService.GetCaseTypesAsync(System.Threading.CancellationToken.None);
                foreach (var caseType in cases)
                {
                    _logger.Info($" Case found {caseType.Name}, {caseType.Id}, {caseType.GetType}");
                }
            
        //}
        //catch (Exception e)
        //{
        //    _logger.Info(e.Message);
        //    _logger.Info($" exception type {e.GetType().Name}");
        //    throw; 
        //}
        _logger.Info($"Case Type List found");
    }
    }

}
