namespace TaskControl.Domain.Commands.Task
{
    public class GetProjectTasksCommandQueryResult
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public int TaskPriorityId { get; set; }
        public string TaskPriorityDescription { get; set; }
        public int TaskStatusId { get; set; }
        public string TaskStatusDescription { get; set; }

    }
}
