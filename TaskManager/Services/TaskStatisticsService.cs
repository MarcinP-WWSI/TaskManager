
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TaskManager.Models;

public class TaskStatistics
{
    public int Total { get; set; }
    public int Completed { get; set; }
    public double CompletionPercent { get; set; }
}

public class TaskStatisticsService
{
    public TaskStatistics CalculateStatsParallel(IEnumerable<TodoTask> tasks)
    {
        var list = tasks.ToList();
        int total = list.Count;
        int completed = 0;

        Parallel.ForEach(list, task =>
        {
            if (task.IsCompleted)
            {
                Interlocked.Increment(ref completed);
            }
        });

        return new TaskStatistics
        {
            Total = total,
            Completed = completed,
            CompletionPercent = total == 0 ? 0 : (double)completed / total
        };
    }
}
