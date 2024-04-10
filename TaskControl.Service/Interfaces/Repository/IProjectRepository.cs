using TaskControl.Domain.Commands.Project;

namespace TaskControl.Domain.Interfaces.Repository
{
    public interface IProjectRepository
    {
        List<ProjectCommandQueryResult> GetUserProjects(GetProjectCommand command);
        bool CreateProject(CreateProjectCommand command);
        bool DeleteProject(DeleteProjectCommand command);
    }
}
