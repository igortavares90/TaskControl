using Dapper;
using System.Data;
using TaskControl.Domain.Commands.Task;
using TaskControl.Domain.Interfaces.Database;
using TaskControl.Domain.Interfaces.Repository;

namespace TaskControl.Infrastructure.Data.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly IUnitOfWork _unitOfWork;
        public TaskRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public List<GetProjectTasksCommandQueryResult> GetProjectTasks(GetProjectTaskCommand command)
        {
            var Query = $@"SELECT Ta.Id, Ta.ProjectId, Pr.ProjectName as ProjectName, Ta.Title, Ta.Description, Ta.PriorityId as TaskPriorityId,
                           Ta.DueDate, Tp.Description as TaskPriorityDescription, Ts.Id as TaskStatusId, Ts.Description as TaskStatusDescription 
                           FROM Task Ta INNER JOIN Project Pr ON Pr.Id = Ta.ProjectId
                           INNER JOIN TaskPriority Tp ON Tp.Id = Ta.PriorityId
                           INNER JOIN TaskStatus Ts ON Ts.Id = Ta.StatusId
                           WHERE Ta.ProjectId=@ProjectId";

            var Entity = _unitOfWork.Connection.Query<GetProjectTasksCommandQueryResult>(Query, new { ProjectId = command.ProjectId }, commandType: CommandType.Text);

            return Entity.ToList<GetProjectTasksCommandQueryResult>();
        }

        public int CreateProjectTask(CreateTaskCommand command)
        {

            var Query = $@"INSERT INTO Task(ProjectId, Title, Description, DueDate, PriorityId, StatusId, UserId)
                           VALUES (@ProjectId, @Title, @Description, @DueDate, @PriorityId, @StatusId, @UserId);
                           SELECT SCOPE_IDENTITY();";


            var Id = Convert.ToInt32(_unitOfWork.Connection.ExecuteScalar(Query,
                new
                {
                    ProjectId = command.ProjectId,
                    Title = command.Title,
                    Description = command.Description,
                    DueDate = command.DueDate,
                    PriorityId = command.PriorityId,
                    StatusId = command.StatusId,
                    UserId = command.UserId
                },
                commandType: CommandType.Text));

            return Id;
        }

        public bool UpdateProjectTask(UpdateProjectTaskCommand command)
        {
            var Query = $@"UPDATE Task SET Description=@Description,StatusId = @StatusId
                           WHERE Id=@TaskId";

            _unitOfWork.Connection.Query(Query,
                new
                {
                    TaskId = command.TaskId,
                    Description = command.Description,
                    StatusId = command.StatusId,
                },
                commandType: CommandType.Text);

            return true;
        }

        public bool DeleteProjectTask(DeleteProjectTaskCommand command)
        {
            var Query = $@"DELETE FROM TASK WHERE Id=@TaskId";

            _unitOfWork.Connection.Query(Query,
                new
                {
                    TaskId = command.TaskId
                },
                commandType: CommandType.Text);

            return true;
        }

        public bool TaskExists(int taskId)
        {
            var Query = $@"SELECT 1 FROM Task WHERE Id=@TaskId";

            var Entity = _unitOfWork.Connection.Query(Query, new { TaskId = taskId }, commandType: CommandType.Text);

            if (Entity.Count() > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public int GetCountOfProjectTasks(int projectId)
        {

            var Query = $@"SELECT COUNT(1) as count FROM Task WHERE ProjectId=@ProjectId";

            var count = _unitOfWork.Connection.ExecuteScalar<int>(Query, new { ProjectId = projectId }, commandType: CommandType.Text);

            return count;
        }

        public int GetPendingTaskCount(int projectId)
        {

            var Query = $@"SELECT COUNT(1) as PendingCount FROM Task WHERE ProjectId=@ProjectId AND (StatusId = 1 OR StatusId = 2);";

            var count = _unitOfWork.Connection.ExecuteScalar<int>(Query, new { ProjectId = projectId }, commandType: CommandType.Text);

            return count;
        }

        public List<GetTaskPerformanceReportCommandResult> GetPerformanceReport()
        {
            var Query = $@"SELECT (COUNT(ts.Id)/30.0) as AverageTasksCompletedPerMonth, ta.UserId
                           FROM Task Ta INNER JOIN TaskStatus Ts ON Ts.Id = Ta.StatusId AND Ts.Description='concluída'
                           GROUP BY ta.UserId";

            var Entity = _unitOfWork.Connection.Query<GetTaskPerformanceReportCommandResult>(Query, commandType: CommandType.Text);

            return Entity.ToList<GetTaskPerformanceReportCommandResult>();

        }

    }
}