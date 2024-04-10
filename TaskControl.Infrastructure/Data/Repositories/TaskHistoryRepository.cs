using Dapper;
using System.Data;
using TaskControl.Domain.Commands.TaskHistory;
using TaskControl.Domain.Interfaces.Database;
using TaskControl.Domain.Interfaces.Repository;

namespace TaskControl.Infrastructure.Data.Repositories
{
    public class TaskHistoryRepository : ITaskHistoryRepository
    {
        private readonly IUnitOfWork _unitOfWork;
        public TaskHistoryRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public bool CreateTaskHistory(CreateTaskHistoryCommand command)
        {
            var Query = $@"INSERT INTO TaskHistory(TaskId, Description, Date,UserId)
                           VALUES (@TaskId, @Description, @Date, @UserId)";

            _unitOfWork.Connection.Query(Query,
                new
                {
                    TaskId = command.TaskId,
                    Description = command.Description,
                    Date = command.Date,
                    UserId = command.UserId
                },
                commandType: CommandType.Text);

            return true;
        }

    }
}
