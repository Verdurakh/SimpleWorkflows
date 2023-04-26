namespace Workflow.Core;

public enum WorkflowStatusEnum
{
    /// <summary>
    /// Use if we took action and it was successful
    /// </summary>
    Success,

    /// <summary>
    /// Use if we skipped the step and should keep going
    /// </summary>
    Continue,

    /// <summary>
    /// Use if something went wrong, will abort the workflow
    /// </summary>
    Fail
}