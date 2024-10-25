using Moq;
using TaskControl.Application.Services;
using TaskControl.Domain.Commands.Project;
using TaskControl.Domain.Interfaces.Repository;
using TaskControl.Domain.Interfaces.Service;

public class ProjectServiceTests
{
    private readonly Mock<IProjectRepository> _projectRepositoryMock;
    private readonly Mock<ITaskService> _taskServiceMock;
    private readonly ProjectService _projectService;

    public ProjectServiceTests()
    {
        _projectRepositoryMock = new Mock<IProjectRepository>();
        _taskServiceMock = new Mock<ITaskService>();
        _projectService = new ProjectService(_projectRepositoryMock.Object, _taskServiceMock.Object);
    }

    [Fact]
    public void GetUserProjects_ShouldReturnProjects()
    {
        // Arrange
        var command = new GetProjectCommand { UserId = 1 };
        var expectedProjects = new List<ProjectCommandQueryResult>();
        _projectRepositoryMock.Setup(repo => repo.GetUserProjects(command)).Returns(expectedProjects);

        // Act
        var result = _projectService.GetUserProjects(command);

        // Assert
        Assert.Equal(expectedProjects, result);
    }

    [Fact]
    public void CreateProject_ValidProject_ShouldReturnTrue()
    {
        // Arrange
        var command = new CreateProjectCommand { ProjectName = "New Project" };
        _projectRepositoryMock.Setup(repo => repo.CreateProject(command)).Returns(true);

        // Act
        var result = _projectService.CreateProject(command);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void DeleteProject_ValidProjectWithPendingTasks_ShouldReturnErrorMessage()
    {
        // Arrange
        var command = new DeleteProjectCommand { ProjectId = 1 };
        _taskServiceMock.Setup(ts => ts.GetPendingTaskCount(command.ProjectId)).Returns(1);

        // Act
        var result = _projectService.DeleteProject(command);

        // Assert
        Assert.Equal("O Projeto informado não pode ser excluído pois possue tarefas pendentes! Marque as tarefas pendentes como concluídas ou exclua as tarefas pendentes do projeto.", result);
        _projectRepositoryMock.Verify(repo => repo.DeleteProject(It.IsAny<DeleteProjectCommand>()), Times.Never);
    }

    [Fact]
    public void DeleteProject_ValidProjectWithoutPendingTasks_ShouldDeleteProject()
    {
        // Arrange
        var command = new DeleteProjectCommand { ProjectId = 1 };
        _taskServiceMock.Setup(ts => ts.GetPendingTaskCount(command.ProjectId)).Returns(0);

        // Act
        var result = _projectService.DeleteProject(command);

        // Assert
        Assert.Equal(string.Empty, result);
        _projectRepositoryMock.Verify(repo => repo.DeleteProject(command), Times.Once);
    }
}