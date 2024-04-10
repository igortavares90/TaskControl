using TaskControl.Domain.Enum;

namespace TaskControl.Domain.Commands.Task
{
    public class UpdateProjectTaskCommand
    {
        public int TaskId { get; set; }
        public string? Description { get; set; }
        public TaskStatusEnum StatusId { get; set; }
        public int UserId { get; set; }
    }
}
