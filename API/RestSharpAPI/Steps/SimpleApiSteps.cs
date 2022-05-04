using NUnit.Framework;
using RestSharp;
using RestSharpApi.Hooks;
using System.Net;
using TechTalk.SpecFlow;

namespace RestSharpApi.Steps
{
    [Binding]
	///<Summary>
	/// Common Code
	///</summary>
    public sealed class SimpleApiSteps
    {
        private RestClient _client;
        public RestRequest _request;

        [Given(@"The user send an GEt call to endpoiint '(.*)'")]
        public void GivenTheUserSendAnGEtCallToEndpoiint(string endpoint)
        {
            _client = RestSharpHooks._restClient;
            _request = new RestRequest(endpoint, Method.GET);
        }

        [Then(@"The user should receive a status code of OK")]
        public void ThenTheUserShouldReceiveAStatusCodeOfOK()
        {
            IRestResponse response = _client.Execute(_request);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }
    }
}
