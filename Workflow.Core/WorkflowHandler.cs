using System.Diagnostics;
using Microsoft.Extensions.Logging;

namespace Workflow.Core;

/// <summary>
/// The class that contains and runs all steps
/// </summary>
/// <typeparam name="TRequest"></typeparam>
/// <typeparam name="TResponse"></typeparam>
public class WorkflowHandler<TRequest, TResponse> : WorkflowStepBase<TRequest, TResponse>
{
    private readonly IReadOnlyList<IWorkflowStep<TRequest, TResponse>> _handlers;

    private bool _isLogging;
    private ILogger? _logger;
    private readonly Stopwatch _stopwatch = new();



    /// <summary>
    /// For internal use only, to Construct use WorkflowHandlerBuilder
    /// </summary>
    internal WorkflowHandler(List<IWorkflowStep<TRequest, TResponse>> steps)
    {
        if (steps == null || !steps.Any())
        {
            throw new ArgumentNullException(nameof(steps), "No steps are set.");
        }

        _handlers = steps;
    }

    internal void SetLogging(bool isLogging, ILogger? logger)
    {
        _isLogging = isLogging;
        _logger = logger;
    }

    /// <summary>
    /// Start the workflow, if we are at the start then only the request is used, then it responses will be returned as well.
    /// </summary>
    /// <param name="originalWorkflowRequest">the request sent at the start</param>
    /// <param name="accumulatedWorkflowResponse">the response returned after all steps, the Messages will be merged</param>
    /// <returns></returns>
    public override async Task<IWorkflowResponse<TResponse>> HandleAsync(
        IWorkflowRequest<TRequest> originalWorkflowRequest,
        IWorkflowResponse<TResponse>? accumulatedWorkflowResponse = null)
    {
        var previousResponse = await _handlers[0].HandleAsync(originalWorkflowRequest, accumulatedWorkflowResponse);

        if (_handlers.Count <= 1 || previousResponse.Status == WorkflowStatusEnum.Fail)
            return previousResponse;

        for (var handlerIndex = 1; handlerIndex < _handlers.Count; handlerIndex++)
        {
            Log($"Starting handler: {_handlers[handlerIndex].GetType().Name}");
            _stopwatch.Restart();

            var newResponse = await _handlers[handlerIndex].HandleAsync(originalWorkflowRequest, previousResponse);

            _stopwatch.Stop();
            Log(
                $"Handler: {_handlers[handlerIndex].GetType().Name} completed in {_stopwatch.ElapsedMilliseconds} ms. Status: {newResponse.Status}");

            newResponse.Messages.AddRange(previousResponse.Messages);
            previousResponse = newResponse;
            if (previousResponse.Status == WorkflowStatusEnum.Fail)
                break;
        }

        return previousResponse;
    }

    private void Log(string message)
    {
        if (!_isLogging) return;
        if (_logger is not null)
            _logger.LogInformation(message);
        else
            Console.WriteLine(message);
    }
}