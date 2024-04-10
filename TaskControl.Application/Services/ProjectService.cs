using TaskControl.Domain.Commands.Project;
using TaskControl.Domain.Interfaces.Repository;
using TaskControl.Domain.Interfaces.Service;

namespace TaskControl.Application.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository _projectRepository;
        private readonly ITaskService _taskService;

        public ProjectService(IProjectRepository projectRepository, ITaskService taskService)
        {
            _projectRepository = projectRepository;
            _taskService = taskService;
        }

        public List<ProjectCommandQueryResult> GetUserProjects(GetProjectCommand command)
        {
            return _projectRepository.GetUserProjects(command);
        }

        public bool CreateProject(CreateProjectCommand command)
        {
            return _projectRepository.CreateProject(command);
        }

        public string DeleteProject(DeleteProjectCommand command)
        {
            string resultOfValidation = ValidateDeleteProject(command);

            if (resultOfValidation != string.Empty)
            {
                return resultOfValidation;
            }

            _projectRepository.DeleteProject(command);

            return "";
        }

        private string ValidateDeleteProject(DeleteProjectCommand command)
        {
            string error = string.Empty;

            if (_taskService.GetPendingTaskCount(command.ProjectId) > 0)
            {
                error = "O Projeto informado não pode ser excluído pois possue tarefas pendentes! Marque as tarefas pendentes como concluídas ou exclua as tarefas pendentes do projeto.";
            }

            return error;
        }
    }
}