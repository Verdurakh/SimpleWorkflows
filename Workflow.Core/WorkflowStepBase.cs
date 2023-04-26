namespace Workflow.Core;

/// <summary>
/// Use to create handlers, handlers can also be a step to chain steps together
/// </summary>
/// <typeparam name="TRequest"></typeparam>
/// <typeparam name="TResponse"></typeparam>
public abstract class WorkflowStepBase<TRequest, TResponse> : IWorkflowStep<TRequest, TResponse>
{
    public abstract Task<IWorkflowResponse<TResponse>> HandleAsync(IWorkflowRequest<TRequest> originalWorkflowRequest,
        IWorkflowResponse<TResponse>? accumulatedWorkflowResponse = null);
}