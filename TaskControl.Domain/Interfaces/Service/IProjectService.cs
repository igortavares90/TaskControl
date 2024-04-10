using TaskControl.Domain.Commands.Project;

namespace TaskControl.Domain.Interfaces.Service
{
    public interface IProjectService
    {
        List<ProjectCommandQueryResult> GetUserProjects(GetProjectCommand command);
        bool CreateProject(CreateProjectCommand command);
        string DeleteProject(DeleteProjectCommand command);
    }
}
