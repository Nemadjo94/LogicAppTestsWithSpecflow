using SpecFlowDemo.Models;
using System.Collections.Generic;
using System.Linq;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace SpecFlowDemo.Utilities
{
    [Binding]
    public class LogicAppEventsTransformer
    {
        [StepArgumentTransformation]
        public IList<LogicAppEvent> TransformTableToLogicAppEvents(Table table) =>
            table.CreateSet<LogicAppEvent>().ToList();
    }
}
