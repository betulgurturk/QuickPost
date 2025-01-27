using Application.Common.Interfaces;
using System.Security.Claims;

namespace QuickPostApi.Services
{
    /// <summary>
    /// Kullanıcı bilgilerini döndüren servis
    /// </summary>
    /// <param name="httpContextAccessor"></param>
    public class CurrentUserService(IHttpContextAccessor httpContextAccessor) : IUserService
    {

        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

        /// <summary>
        /// gets user ıd
        /// </summary>
        public Guid Id => Guid.TryParse(_httpContextAccessor.HttpContext?.User?.FindFirstValue("UserId"), out var userId) ? userId : Guid.Empty;
        /// <summary>
        /// gets user name
        /// </summary>
        public string UserName => _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier) ?? string.Empty;
        /// <summary>
        /// gets user email
        /// </summary>
        public string Email => _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.Email) ?? string.Empty;
    }
}
