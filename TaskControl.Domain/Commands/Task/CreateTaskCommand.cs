using TaskControl.Domain.Enum;

namespace TaskControl.Domain.Commands.Task
{
    public class CreateTaskCommand
    {
        public int ProjectId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        /// <summary>
        /// 1 = Baixa.
        /// </summary>
        /// <summary>
        /// 2 = Média
        /// </summary>
        /// <summary>
        /// 3 = Alta
        /// </summary>
        public TaskPriorityEnum PriorityId { get; set; }
        public TaskStatusEnum StatusId { get; set; }
        public int UserId { get; set; }
    }
}
