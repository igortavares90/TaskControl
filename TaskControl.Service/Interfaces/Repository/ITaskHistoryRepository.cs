using TaskControl.Domain.Commands.TaskHistory;

namespace TaskControl.Domain.Interfaces.Repository
{
    public interface ITaskHistoryRepository
    {
        bool CreateTaskHistory(CreateTaskHistoryCommand command);
    }
}
