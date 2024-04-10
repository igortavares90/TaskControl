namespace TaskControl.Domain.Commands.TaskHistory
{
    public class CreateTaskHistoryCommand
    {
        public int TaskId { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public int UserId { get; set; }
    }
}
