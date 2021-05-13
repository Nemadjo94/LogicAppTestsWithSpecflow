using FluentAssertions;
using LogicApps.Tests.Utilities;
using Microsoft.Azure.Management.Logic;
using Microsoft.Azure.Services.AppAuthentication;
using SpecFlowDemo.Models;
using SpecFlowDemo.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using TechTalk.SpecFlow;

namespace LogicApps.Tests.Steps
{
    [Binding]
    public class LogicAppBindings
    {
        private static LogicAppExecutionRetriever _retriever;
        private readonly WorkflowRunContext _context;

        public LogicAppBindings(WorkflowRunContext context)
        {
            _context = context;
        }

        [BeforeTestRun]
        public static void Initialize()
        {
            var token = new TokenGenerator(new AzureServiceTokenProvider());
            var credentials = token.GenerateCredentials().Result;
            _retriever = new LogicAppExecutionRetriever(new LogicManagementClient(credentials)
            {
                SubscriptionId = EnvironmentVariables.SubscriptionId
            });
        }

        [When(@"I wait '(\d+)' seconds for '(.*)' logic app execution to complete")]
        public void WhenIWaitSecondsForExecutionToComplete(int seconds, string logicApp)
        {
            Thread.Sleep(TimeSpan.FromSeconds(seconds));

            int retryCounter = 0;

            while (_retriever.GetWorkflowRunStatus(logicApp, EnvironmentVariables.ResourceGroups[logicApp], _context.WorkflowRunId) == "Running")
            {
                Thread.Sleep(TimeSpan.FromSeconds(10));
                retryCounter++;

                if (retryCounter == 10)
                    break;
            }
        }

        [Then(@"I can verify the following events for '(.*)' logic app")]
        public void ThenICanVerifyTheFollowingLogicAppEventsFor(string logicApp, IList<LogicAppEvent> expectedEvents)
        {
            var actualEvents = _retriever.GetExecutionEvents(logicApp, EnvironmentVariables.ResourceGroups[logicApp], _context.WorkflowRunId);

            foreach (var @event in expectedEvents)
            {
                var actualEvent = actualEvents.FirstOrDefault(x => x.StepName == @event.StepName);

                @event.StepName.Should().BeEquivalentTo(actualEvent.StepName);
                @event.Status.Should().BeEquivalentTo(actualEvent.Status);
            }

            var expectedEventsSteps = expectedEvents.Select(x => x.StepName);
            var actualEventsSteps = actualEvents.Select(x => x.StepName);

            bool hasAllEvents = expectedEventsSteps.All(@event => actualEventsSteps.Contains(@event));

            hasAllEvents.Should().BeTrue();
        }
    }
}
