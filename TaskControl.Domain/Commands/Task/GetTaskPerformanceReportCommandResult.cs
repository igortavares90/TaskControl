namespace TaskControl.Domain.Commands.Task
{
    public class GetTaskPerformanceReportCommandResult
    {
        public decimal AverageTasksCompletedPerMonth { get; set; }
        public int UserId { get; set; }
    }
}
