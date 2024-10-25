using Microsoft.Extensions.Configuration;
using Moq;
using TaskControl.Application.Services;
using TaskControl.Domain.Commands.Task;
using TaskControl.Domain.Commands.TaskHistory;
using TaskControl.Domain.Interfaces.Repository;
using TaskControl.Domain.Interfaces.Service;

public class TaskServiceTests
{
    private readonly Mock<ITaskRepository> _taskRepositoryMock;
    private readonly Mock<ITaskHistoryService> _taskHistoryServiceMock;
    private readonly Mock<IConfiguration> _configurationMock;
    private readonly TaskService _taskService;

    public TaskServiceTests()
    {
        _taskRepositoryMock = new Mock<ITaskRepository>();
        _taskHistoryServiceMock = new Mock<ITaskHistoryService>();
        _configurationMock = new Mock<IConfiguration>();
        _taskService = new TaskService(_taskRepositoryMock.Object, _taskHistoryServiceMock.Object, _configurationMock.Object);
    }

    [Fact]
    public void GetTasksByProjectId_ShouldReturnTasks()
    {
        // Arrange
        var command = new GetProjectTaskCommand { ProjectId = 1 };
        var expectedTasks = new List<GetProjectTasksCommandQueryResult>();
        _taskRepositoryMock.Setup(repo => repo.GetProjectTasks(command)).Returns(expectedTasks);

        // Act
        var result = _taskService.GetTasksByProjectId(command);

        // Assert
        Assert.Equal(expectedTasks, result);
    }

    [Fact]
    public void CreateProjectTask_ValidTask_ShouldReturnEmptyString()
    {
        // Arrange
        var command = new CreateTaskCommand { ProjectId = 1, UserId = 1 };
        _configurationMock.Setup(cfg => cfg["TaskLimitPerProject"]).Returns("5");
        _taskRepositoryMock.Setup(repo => repo.GetCountOfProjectTasks(command.ProjectId)).Returns(4);
        _taskRepositoryMock.Setup(repo => repo.CreateProjectTask(command)).Returns(1);

        // Act
        var result = _taskService.CreateProjectTask(command);

        // Assert
        Assert.Equal(string.Empty, result);
        _taskHistoryServiceMock.Verify(s => s.CreateTaskHistory(It.IsAny<CreateTaskHistoryCommand>()), Times.Once);
    }

    [Fact]
    public void CreateProjectTask_ExceedingTaskLimit_ShouldReturnErrorMessage()
    {
        // Arrange
        var command = new CreateTaskCommand { ProjectId = 1, UserId = 1 };
        _configurationMock.Setup(cfg => cfg["TaskLimitPerProject"]).Returns("5");
        _taskRepositoryMock.Setup(repo => repo.GetCountOfProjectTasks(command.ProjectId)).Returns(5);

        // Act
        var result = _taskService.CreateProjectTask(command);

        // Assert
        Assert.Equal("O Projeto informado atingiu o limite máximo de 5 tarefas!", result);
        _taskHistoryServiceMock.Verify(s => s.CreateTaskHistory(It.IsAny<CreateTaskHistoryCommand>()), Times.Never);
    }

    [Fact]
    public void UpdateProjectTask_ShouldCallRepositoryAndCreateHistory()
    {
        // Arrange
        var command = new UpdateProjectTaskCommand { TaskId = 1, UserId = 1 };
        _taskRepositoryMock.Setup(repo => repo.UpdateProjectTask(command));

        // Act
        var result = _taskService.UpdateProjectTask(command);

        // Assert
        Assert.True(result);
        _taskHistoryServiceMock.Verify(s => s.CreateTaskHistory(It.IsAny<CreateTaskHistoryCommand>()), Times.Once);
    }

    [Fact]
    public void DeleteProjectTask_ShouldReturnRepositoryResult()
    {
        // Arrange
        var command = new DeleteProjectTaskCommand { TaskId = 1 };
        _taskRepositoryMock.Setup(repo => repo.DeleteProjectTask(command)).Returns(true);

        // Act
        var result = _taskService.DeleteProjectTask(command);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void GetCountOfProjectTasks_ShouldReturnCount()
    {
        // Arrange
        int projectId = 1;
        _taskRepositoryMock.Setup(repo => repo.GetCountOfProjectTasks(projectId)).Returns(10);

        // Act
        var result = _taskService.GetCountOfProjectTasks(projectId);

        // Assert
        Assert.Equal(10, result);
    }

    [Fact]
    public void GetPendingTaskCount_ShouldReturnCount()
    {
        // Arrange
        int projectId = 1;
        _taskRepositoryMock.Setup(repo => repo.GetPendingTaskCount(projectId)).Returns(5);

        // Act
        var result = _taskService.GetPendingTaskCount(projectId);

        // Assert
        Assert.Equal(5, result);
    }

    [Fact]
    public void TaskExists_ShouldReturnTrue()
    {
        // Arrange
        int taskId = 1;
        _taskRepositoryMock.Setup(repo => repo.TaskExists(taskId)).Returns(true);

        // Act
        var result = _taskService.TaskExists(taskId);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void GetPerformanceReport_ShouldReturnReport()
    {
        // Arrange
        var expectedReport = new List<GetTaskPerformanceReportCommandResult>();
        _taskRepositoryMock.Setup(repo => repo.GetPerformanceReport()).Returns(expectedReport);

        // Act
        var result = _taskService.GetPerformanceReport();

        // Assert
        Assert.Equal(expectedReport, result);
    }
}
