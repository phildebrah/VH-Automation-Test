using NUnit.Framework;
using RestSharp;
using RestSharpApi.Hooks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using TechTalk.SpecFlow;

namespace RestSharpApi.Steps
{
    [Binding]
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
            var content = response.Content;
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }
    }
}
