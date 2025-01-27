using Application.Common.Interfaces;
using Domain.Helpers;
using Domain.Models;
using MediatR;
using QuickPost.Domain.Helpers;

namespace Application.Users.Commands
{
    public class SaveUserCommand : IRequest<Result>
    {
        public required string UserName { get; set; }
        public required string Password { get; set; }
        public required string Email { get; set; }
        public string? Firstname { get; set; }
        public string? Lastname { get; set; }
    }

    public class SaveUserCommandHandler(IQuickpostDbContext context) : IRequestHandler<SaveUserCommand, Result>
    {
        private readonly IQuickpostDbContext _context = context;

        public async Task<Result> Handle(SaveUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var user = new User
                {
                    Username = request.UserName,
                    Password = PasswordHelper.HashPassword(request.Password),
                    Emailaddress = request.Email,
                    Firstname = request.Firstname,
                    Lastname = request.Lastname,
                    Createddate = DateTime.Now,
                    Modifieddate = DateTime.Now,
                    Id = Guid.NewGuid()
                };
                await _context.Users.AddAsync(user, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);
                return new Result(true, "User saved successfully");
            }
            catch (Exception ex)
            {
                return new Result(false, ex.Message);
            }
        }
    }
}
