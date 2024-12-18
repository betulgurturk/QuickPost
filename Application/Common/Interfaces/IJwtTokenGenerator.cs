using DBGenerator.Models;

namespace Application.Common.Interfaces
{
    public interface IJwtTokenGenerator
    {
        Task<string> GenerateToken(User user);
    }
}
