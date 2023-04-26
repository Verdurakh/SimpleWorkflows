namespace Workflow.Core.Tests.TestClasses;

internal class WorkflowResponse : IWorkflowResponse<string>
{
    public WorkflowResponse(string resp, WorkflowStatusEnum status, string message)
    {
        Data = resp;
        Status = status;
        Messages.Add(message);
    }

    public string? Data { get; set; }
    public List<string> Messages { get; set; } = new();
    public WorkflowStatusEnum Status { get; set; }
} 