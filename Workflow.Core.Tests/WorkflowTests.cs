using FluentAssertions;
using Workflow.Core.Tests.TestClasses;
using Workflow.Core.Tests.TestClasses.Steps;
using Xunit;

namespace Workflow.Core.Tests;

public class WorkflowTests
{
    [Fact]
    public async void WorkflowCheckText_Valid_ReturnsTrue()
    {
        //Arrange
        var initialRequest = new WorkflowRequest("hello");

        //Act
        var handler = new WorkflowHandlerBuilder<string, string>()
            .AddStep(new CheckTextContainsHelloWorkflowStep())
            .Build();


        var response = await handler.HandleAsync(initialRequest);
        //Assert
        response.Status.Should().Be(WorkflowStatusEnum.Success);
    }

    [Fact]
    public async void WorkflowCheckText_InValid_DidNotContainRequirement()
    {
        //Arrange
        var initialRequest = new WorkflowRequest("yolo");

        //Act
        var handler = new WorkflowHandlerBuilder<string, string>()
            .AddStep(new CheckTextContainsHelloWorkflowStep())
            .Build();


        var response = await handler.HandleAsync(initialRequest);
        //Assert
        response.Status.Should().Be(WorkflowStatusEnum.Fail);
    }

    [Fact]
    public void Workflow_InValid_NoStepsException()
    {
        //Arrange

        //Act

        Func<WorkflowHandler<string, string>> act = () => new WorkflowHandlerBuilder<string, string>().Build();

        //Assert
        act.Should().Throw<ArgumentNullException>()
            .WithMessage("No steps are set. (Parameter 'steps')");
    }

    [Fact]
    public async void TwoStepWorkflow_Valid_ReturnsTrue()
    {
        //Arrange
        var initialRequest = new WorkflowRequest("hello");

        //Act
        var handler = new WorkflowHandlerBuilder<string, string>()
            .AddStep(new AddWorldToDataStep())
            .AddStep(new CheckTextContainsHelloWorkflowStep())
            .Build();

        var response = await handler.HandleAsync(initialRequest);
        //Assert
        response.Status.Should().Be(WorkflowStatusEnum.Success);
        response.Data.Should().Contain("world");
    }

    [Fact]
    public async void TwoStepWorkflow_InValid_FailAndDoNotContinue()
    {
        //Arrange
        var initialRequest = new WorkflowRequest("wololo");

        //Act
        var handler = new WorkflowHandlerBuilder<string, string>()
            .AddStep(new CheckTextContainsHelloWorkflowStep())
            .AddStep(new AddWorldToDataStep())
            .Build();


        var response = await handler.HandleAsync(initialRequest);
        //Assert
        response.Status.Should().Be(WorkflowStatusEnum.Fail);
        response.Data.Should().NotContain("world");
    }

    [Fact]
    public async void SkipWorkflow_Valid_AddedWorldAnyway()
    {
        //Arrange
        var initialRequest = new WorkflowRequest("wololo");

        //Act
        var handler = new WorkflowHandlerBuilder<string, string>()
            .AddStep(new SkipStep())
            .AddStep(new AddWorldToDataStep())
            .Build();


        var response = await handler.HandleAsync(initialRequest);
        //Assert
        response.Status.Should().Be(WorkflowStatusEnum.Success);
        response.Data.Should().Contain("world");
    }

    [Fact]
    public async void AddWorldAndThenSkipStep_Valid_AddedWorld()
    {
        //Arrange
        var initialRequest = new WorkflowRequest("wololo");

        //Act
        var handler = new WorkflowHandlerBuilder<string, string>()
            .AddStep(new AddWorldToDataStep())
            .AddStep(new SkipStep())
            .Build();

        var response = await handler.HandleAsync(initialRequest);
        //Assert
        response.Status.Should().Be(WorkflowStatusEnum.Continue);
        response.Data.Should().Contain("world");
    }

    [Fact]
    public async void AddWorldManyTimesAndFailWorkflow_Valid_AddedFourTimes()
    {
        //Arrange
        var initialRequest = new WorkflowRequest("wololo");

        //Act
        var handler = new WorkflowHandlerBuilder<string, string>()
            .AddStep(new AddWorldToDataStep())
            .AddStep(new AddWorldToDataStep())
            .AddStep(new AddWorldToDataStep())
            .AddStep(new AddWorldToDataStep())
            .Build();


        var response = await handler.HandleAsync(initialRequest);
        //Assert
        response.Status.Should().Be(WorkflowStatusEnum.Success);
        response.Data.Should().Contain("world world world world");
    }
}