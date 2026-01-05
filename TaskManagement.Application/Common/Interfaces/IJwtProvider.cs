namespace TaskManagement.Application.Common.Interfaces;

public interface IJwtProvider
{
    string Generate(TaskManagement.Domain.Entities.User user);
}