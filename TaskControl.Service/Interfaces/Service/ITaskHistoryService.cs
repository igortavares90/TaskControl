using TaskControl.Domain.Commands.TaskHistory;

namespace TaskControl.Domain.Interfaces.Service
{
    public interface ITaskHistoryService
    {
        bool CreateTaskHistory(CreateTaskHistoryCommand command);
    }
}
