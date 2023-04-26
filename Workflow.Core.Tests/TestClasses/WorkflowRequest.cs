namespace Workflow.Core.Tests.TestClasses;

internal class WorkflowRequest : IWorkflowRequest<string>
{
    public WorkflowRequest(string data)
    {
        Data = data;
    }

    public string Data { get; }
}