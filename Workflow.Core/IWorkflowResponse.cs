namespace Workflow.Core;

/// <summary>
/// Use this for the response from each workflow, T can be anything
/// Messages is more like a log for the entire workflow and will be merged in the end.
/// </summary>
/// <typeparam name="TData"></typeparam>
public interface IWorkflowResponse<TData>
{
    /// <summary>
    /// Data object that will be passed to the next step
    /// </summary>
    public TData? Data { get; set; }

    /// <summary>
    /// Log messages for the entire workflow
    /// </summary>
    public List<string> Messages { get; set; }

    /// <summary>
    /// Current status of the workflow, if failed, the workflow will stop
    /// </summary>
    public WorkflowStatusEnum Status { get; set; }
}