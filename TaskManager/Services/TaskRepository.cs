using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManager.Data;
using TaskManager.Models;

public class TaskRepository
{
    public async Task<List<TodoTask>> GetAllAsync()
    {
        using var db = new AppDbContext();
        return await db.Tasks.OrderByDescending(t => t.CreatedAt).ToListAsync();
    }

    public async Task AddAsync(TodoTask task)
    {
        using var db = new AppDbContext();
        db.Tasks.Add(task);
        await db.SaveChangesAsync();
    }

    public async Task UpdateAsync(TodoTask task)
    {
        using var db = new AppDbContext();
        db.Tasks.Update(task);
        await db.SaveChangesAsync();
    }

    public async Task DeleteAsync(TodoTask task)
    {
        using var db = new AppDbContext();
        db.Tasks.Remove(task);
        await db.SaveChangesAsync();
    }
}