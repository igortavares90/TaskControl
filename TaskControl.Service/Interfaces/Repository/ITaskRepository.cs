using TaskControl.Domain.Commands.Task;

namespace TaskControl.Domain.Interfaces.Repository
{
    public interface ITaskRepository
    {
        List<GetProjectTasksCommandQueryResult> GetProjectTasks(GetProjectTaskCommand command);
        int CreateProjectTask(CreateTaskCommand command);
        bool UpdateProjectTask(UpdateProjectTaskCommand command);
        bool DeleteProjectTask(DeleteProjectTaskCommand command);
        bool TaskExists(int taskId);
        int GetCountOfProjectTasks(int projectId);
        int GetPendingTaskCount(int projectId);
        List<GetTaskPerformanceReportCommandResult> GetPerformanceReport();
    }
}
