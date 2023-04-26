namespace Workflow.Core.Tests.TestClasses.Steps;

internal class AddWorldToDataStep : IWorkflowStep<string, string>
{
    public async Task<IWorkflowResponse<string>> HandleAsync(IWorkflowRequest<string> originalWorkflowRequest,
        IWorkflowResponse<string>? accumulatedWorkflowResponse = null)
    {
        var text = accumulatedWorkflowResponse is null
            ? originalWorkflowRequest.Data
            : accumulatedWorkflowResponse.Data;

        return await Task.FromResult(new WorkflowResponse(text + " world", WorkflowStatusEnum.Success,
            "Added world to data"));
    }
}