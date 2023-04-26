namespace Workflow.Core.Tests.TestClasses.Steps;

internal class SkipStep : IWorkflowStep<string, string>
{
    public async Task<IWorkflowResponse<string>> HandleAsync(IWorkflowRequest<string> originalWorkflowRequest,
        IWorkflowResponse<string>? accumulatedWorkflowResponse = null)
    {
        var text = accumulatedWorkflowResponse is null
            ? originalWorkflowRequest.Data
            : accumulatedWorkflowResponse.Data;

        return await Task.FromResult(
            new WorkflowResponse(text!, WorkflowStatusEnum.Continue, "Skip step"));
    }
}