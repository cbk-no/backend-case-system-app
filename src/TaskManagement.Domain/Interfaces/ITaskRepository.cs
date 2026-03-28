using TaskManagement.Domain.Entities;

namespace TaskManagement.Domain.Interfaces;


public interface ITaskRepository : IRepository<TaskItem>
{
}