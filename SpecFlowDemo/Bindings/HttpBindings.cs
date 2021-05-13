using LogicApps.Tests.Utilities;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using SpecFlowDemo.Models;
using SpecFlowDemo.Utilities;
using System.Linq;
using System.Net.Http;
using System.Text;
using TechTalk.SpecFlow;

namespace LogicApps.Tests.Steps
{
    [Binding]
    public class HttpBindings
    {
        private static Encoding _encoding = Encoding.UTF8;
        private static string _mediaType = "application/json";

        private static HttpClient _client;
        private readonly WorkflowRunContext _context;

        public HttpBindings(WorkflowRunContext context)
        {
            _context = context;
        }

        [BeforeTestRun]
        public static void Initialize()
        {
            _client = new HttpClient();
        }

        [Given(@"I send a POST request with '(.*)' scenario to trigger the '(.*)' logic app")]
        [When(@"I send a POST request with '(.*)' scenario to trigger the '(.*)' logic app")]
        public void ISendAPOSTRequestToWithTheBody(string scenario, string logicAppName)
        {
            var request = SerializeObject(RequestsLibrary.Requests[logicAppName][scenario]);
            var content = new StringContent(request, _encoding, _mediaType);

            var response = _client.PostAsync(EnvironmentVariables.HttpTriggerUris[logicAppName], content).Result;
            response.Headers.TryGetValues("x-ms-workflow-run-id", out var workflowRunId);

            _client.DefaultRequestHeaders.Clear();
            _context.WorkflowRunId = workflowRunId.Single();
        }

        private string SerializeObject(object @object)
        {
            DefaultContractResolver contractResolver = new DefaultContractResolver
            {
                NamingStrategy = new CamelCaseNamingStrategy()
            };

            string json = JsonConvert.SerializeObject(@object, new JsonSerializerSettings
            {
                ContractResolver = contractResolver,
                Formatting = Formatting.Indented
            });

            return json;
        }
    }
}
