using Microsoft.Extensions.Configuration;
using TaskControl.Domain.Commands.Task;
using TaskControl.Domain.Commands.TaskHistory;
using TaskControl.Domain.Interfaces.Repository;
using TaskControl.Domain.Interfaces.Service;

namespace TaskControl.Application.Services
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _taskRepository;
        private readonly ITaskHistoryService _taskHistoryService;
        private IConfiguration _configuration;

        public TaskService(ITaskRepository taskRepository, ITaskHistoryService taskHistoryService, IConfiguration configuration)
        {
            _taskRepository = taskRepository;
            _taskHistoryService = taskHistoryService;
            _configuration = configuration;
        }

        public List<GetProjectTasksCommandQueryResult> GetTasksByProjectId(GetProjectTaskCommand command)
        {
            return _taskRepository.GetProjectTasks(command);
        }

        public string CreateProjectTask(CreateTaskCommand command)
        {
            string resultOfValidation = ValidateCreateTask(command);

            if (resultOfValidation != string.Empty)
            {
                return resultOfValidation;
            }

            var taskId = _taskRepository.CreateProjectTask(command);

            var history = new CreateTaskHistoryCommand
            {
                TaskId = taskId,
                Date = DateTime.Now,
                Description = "Usuario " + command.UserId + " criou a tarefa: " + taskId,
                UserId = command.UserId
            };

            _taskHistoryService.CreateTaskHistory(history);

            return "";
        }

        private string ValidateCreateTask(CreateTaskCommand command)
        {
            string error = string.Empty;

            var taskLimitPerProject = Convert.ToInt32(_configuration["TaskLimitPerProject"]);

            if (GetCountOfProjectTasks(command.ProjectId) >= taskLimitPerProject)
            {
                error = "O Projeto informado atingiu o limite máximo de " + taskLimitPerProject + " tarefas!";
            }

            return error;
        }

        public bool UpdateProjectTask(UpdateProjectTaskCommand command)
        {
            _taskRepository.UpdateProjectTask(command);

            var history = new CreateTaskHistoryCommand
            {
                TaskId = command.TaskId,
                Date = DateTime.Now,
                Description = "Usuario " + command.UserId + " atualizou a tarefa: " + command.TaskId,
                UserId = command.UserId
            };

            _taskHistoryService.CreateTaskHistory(history);

            return true;
        }

        public bool DeleteProjectTask(DeleteProjectTaskCommand command)
        {
            return _taskRepository.DeleteProjectTask(command);
        }

        public int GetCountOfProjectTasks(int projectId)
        {
            return _taskRepository.GetCountOfProjectTasks(projectId);
        }

        public int GetPendingTaskCount(int projectId)
        {
            return _taskRepository.GetPendingTaskCount(projectId);
        }

        public bool TaskExists(int taskId)
        {
            return _taskRepository.TaskExists(taskId);
        }

        public List<GetTaskPerformanceReportCommandResult> GetPerformanceReport()
        {
            return _taskRepository.GetPerformanceReport();
        }
    }
}
