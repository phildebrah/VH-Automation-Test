using RestSharp;
using TechTalk.SpecFlow;
using TestFramework.Hooks;

namespace RestSharpApi.Hooks
{
    [Binding]
    public sealed class RestSharpHooks 
    {
        public static RestClient _restClient;

        [BeforeScenario]
        public void BeforeScenario()
        {
            _restClient = new RestClient("http://api.zippopotam.us");
            GlobalHooks.BeforeRestSharpScenario();
        }

        [AfterScenario]
        public void AfterScenario()
        {
            GlobalHooks.AfterRestSharpScenario();
        }
    }
}