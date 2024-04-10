using TaskControl.Domain.Commands.Project;
using TaskControl.Domain.Commands.Task;

namespace TaskControl.Domain.Interfaces.Service
{
    public interface ITaskService
    {
        List<GetProjectTasksCommandQueryResult> GetTasksByProjectId(GetProjectTaskCommand command);
        string CreateProjectTask(CreateTaskCommand command);
        bool UpdateProjectTask(UpdateProjectTaskCommand command);
        bool DeleteProjectTask(DeleteProjectTaskCommand command);
        bool TaskExists(int taskId);
        int GetCountOfProjectTasks(int projectId);
        int GetPendingTaskCount(int projectId);
    }
}
