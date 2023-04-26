using Microsoft.Extensions.Logging;

namespace Workflow.Core;

public class WorkflowHandlerBuilder<TRequest, TResponse>
{
    private readonly List<IWorkflowStep<TRequest, TResponse>> _steps = new();
    private bool _isLoggingEnabled;
    private ILogger? _logger;

    /// <summary>
    /// Adds a step to the workflow
    /// </summary>
    /// <param name="step"></param>
    /// <returns></returns>
    public WorkflowHandlerBuilder<TRequest, TResponse> AddStep(IWorkflowStep<TRequest, TResponse> step)
    {
        _steps.Add(step);
        return this;
    }

    // public WorkflowHandlerBuilder<TRequest, TResponse> AddStepParallel(params IWorkflowStep<TRequest, TResponse>[] step)
    // {
    //     _steps.Add(step);
    //     return this;
    // }

    /// <summary>
    /// Enables logging for the workflow with timestamps
    /// if no logger is provided then the Console.WriteLine is used
    /// </summary>
    /// <returns></returns>
    public WorkflowHandlerBuilder<TRequest, TResponse> AddLogging(ILogger? logger)
    {
        _isLoggingEnabled = true;
        _logger = logger;
        return this;
    }

    /// <summary>
    /// Builds the workflow
    /// </summary>
    /// <returns></returns>
    public WorkflowHandler<TRequest, TResponse> Build()
    {
        var flow = new WorkflowHandler<TRequest, TResponse>(_steps);
        flow.SetLogging(_isLoggingEnabled, _logger);
        return flow;
    }
}

// public class ParallelWorkFlowStep<TRequest, TResponse> : IWorkflowStep<List<IWorkflowStep<TRequest, TResponse>>, TResponse>
// {
//     public async Task<IWorkflowResponse<TResponse>> HandleAsync(
//         IWorkflowRequest<List<IWorkflowStep<TRequest, TResponse>>> originalWorkflowRequest,
//         IWorkflowResponse<TResponse>? accumulatedWorkflowResponse = null)
//     {
//         var firstStep = originalWorkflowRequest.Data.First();
//         
//             
//         var response=await firstStep.HandleAsync(originalWorkflowRequest, accumulatedWorkflowResponse);
//     }
// }