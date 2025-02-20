using Application.Common.Interfaces;
using Domain.Models;
using MediatR;
using OpenTelemetry.Trace;
using QuickPost.Domain.Helpers;
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
        private readonly Tracer _tracer;

        public LoginCommandHandler(IJwtTokenGenerator jwtTokenGenerator, IQuickpostDbContext context, TracerProvider tracerProvider)
        {
            _jwtTokenGenerator = jwtTokenGenerator;
            _context = context;
            _tracer = tracerProvider.GetTracer("MainTracer");
        }

        public async Task<string> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            using var span = _tracer.StartActiveSpan("ApplicationLayer.LoginCommand");

            span.SetAttribute("auth.user", request.model.UserName);
            span.SetAttribute("auth.service", "AuthService");

            var user = _context.Users.FirstOrDefault(x => x.Username == request.model.UserName);
            if (user is null || !PasswordHelper.VerifyPassword(request.model.Password, user.Password))
            {
                var ex = new Exception("Invalid username or password");
                span.RecordException(ex);
                span.SetAttribute("auth.error", true);
                span.AddEvent("Kullanıcı doğrulama sırasında hata oluştu");
                throw ex;
            }
            return await _jwtTokenGenerator.GenerateToken(user);
        }
    }
}
