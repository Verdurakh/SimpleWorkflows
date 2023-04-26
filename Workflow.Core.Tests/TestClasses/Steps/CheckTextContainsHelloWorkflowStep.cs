namespace Workflow.Core.Tests.TestClasses.Steps;

internal class CheckTextContainsHelloWorkflowStep : IWorkflowStep<string, string>
{
    public async Task<IWorkflowResponse<string>> HandleAsync(IWorkflowRequest<string> originalWorkflowRequest,
        IWorkflowResponse<string>? accumulatedWorkflowResponse = null)
    {
        var text = accumulatedWorkflowResponse is null
            ? originalWorkflowRequest.Data
            : accumulatedWorkflowResponse.Data;
        if (text!.Contains("hello"))
            return await Task.FromResult(new WorkflowResponse(text, WorkflowStatusEnum.Success, "Text checked"));

        return await Task.FromResult(new WorkflowResponse(text, WorkflowStatusEnum.Fail, "Text didn't check out"));
    }
}