namespace Workflow.Core;

/// <summary>
/// Use this for the request that goes into a workflow, T can be anything
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IWorkflowRequest<T>
{
    /// <summary>
    /// Input data for the workflow
    /// </summary>
    public T? Data { get; }
}