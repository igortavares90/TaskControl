using TaskControl.Domain.Commands.TaskHistory;
using TaskControl.Domain.Interfaces.Repository;
using TaskControl.Domain.Interfaces.Service;

namespace TaskControl.Application.Services
{
    public class TaskHistoryService : ITaskHistoryService
    {
        private readonly ITaskHistoryRepository _taskHistoryRepository;

        public TaskHistoryService(ITaskHistoryRepository taskHistoryRepository)
        {
            _taskHistoryRepository = taskHistoryRepository;
        }

        public bool CreateTaskHistory(CreateTaskHistoryCommand command)
        {
            _taskHistoryRepository.CreateTaskHistory(command);

            return true;
        }

    }
}
