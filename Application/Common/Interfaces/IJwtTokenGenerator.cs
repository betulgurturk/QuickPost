using Domain.Models;

namespace Application.Common.Interfaces
{
    public interface IJwtTokenGenerator
    {
        Task<string> GenerateToken(User user);
    }
}
