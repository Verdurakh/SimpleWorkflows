namespace Workflow.Core;

/// <summary>
/// Interfaces for each step inside the workflows
/// </summary>
/// <typeparam name="TRequest">Same as mapped to T in IWorkflowRequest</typeparam>
/// <typeparam name="TResponse">Same as mapped to T in IWorkflowResponse</typeparam>
public interface IWorkflowStep<TRequest, TResponse>
{
    Task<IWorkflowResponse<TResponse>> HandleAsync(IWorkflowRequest<TRequest> originalWorkflowRequest,
        IWorkflowResponse<TResponse>? accumulatedWorkflowResponse = null);
}