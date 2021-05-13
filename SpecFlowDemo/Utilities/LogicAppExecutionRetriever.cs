using Microsoft.Azure.Management.Logic;
using Microsoft.Azure.Management.Logic.Models;
using Microsoft.Rest.Azure.OData;
using SpecFlowDemo.Models;
using System.Collections.Generic;
using System.Linq;

namespace LogicApps.Tests.Utilities
{
    public class LogicAppExecutionRetriever
    {
        private readonly ILogicManagementClient _client;

        public LogicAppExecutionRetriever(ILogicManagementClient client)
        {
            _client = client;
        }

        public IList<LogicAppEvent> GetExecutionEvents(string logicApp, string resourceGroup, string workflowRunId)
        {
            var events = new List<LogicAppEvent>();

            var workflowRun = GetWorkflowRun(logicApp, resourceGroup, workflowRunId);
            events.Add(new LogicAppEvent(workflowRun.Trigger.Name, workflowRun.Trigger.Status));

            var workflowRunActions = GetWorkflowRunActions(logicApp, workflowRun.Name, resourceGroup);
            events.AddRange(workflowRunActions.Select(action => new LogicAppEvent(action.Name, action.Status)));

            return events;
        }

        public string GetWorkflowRunStatus(string logicApp, string resourceGroup, string workflowRunId)
        {
            return GetWorkflowRun(logicApp, resourceGroup, workflowRunId).Status;
        }

        private WorkflowRun GetWorkflowRun(string logicApp, string resourceGroup, string workflowRunId)
        {
            return _client
                .WorkflowRuns
                .GetAsync(resourceGroup, logicApp, workflowRunId)
                .Result;
        }

        private IEnumerable<WorkflowRunAction> GetWorkflowRunActions(string logicApp, string workflowRunId, string resourceGroup)
        {
            var odataQuery = new ODataQuery<WorkflowRunActionFilter>
            {
                Top = 200
            };
            return _client
                .WorkflowRunActions
                .ListWithHttpMessagesAsync(resourceGroup, logicApp, workflowRunId, odataQuery)
                .Result.Body.OrderBy(x => x.StartTime);
        }
    }
}
