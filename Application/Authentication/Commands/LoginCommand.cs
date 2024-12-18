using Application.Common.Interfaces;
using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Authentication.Commands
{
    public class LoginCommand : IRequest<string>
    {
        public required LoginModel model { get; set; }
    }
    public class LoginCommandHandler : IRequestHandler<LoginCommand, string>
    {
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IQuickpostDbContext _context;

        public LoginCommandHandler(IJwtTokenGenerator jwtTokenGenerator, IQuickpostDbContext context)
        {
            _jwtTokenGenerator = jwtTokenGenerator;
            _context = context;
        }

        public async Task<string> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = _context.Users.FirstOrDefault(x => x.Username == request.model.UserName && x.Password == request.model.Password);
            if (user == null)
            {
                throw new Exception("Invalid username or password");
            }
            return await _jwtTokenGenerator.GenerateToken(user);
            //TODO: Result yapısı oluşturulacak.
        }
    }
}
