using Gizli.Application.Features.Auth.Login;
using Gizli.Domain.Entities;

namespace Gizli.Application.Services
{
    public interface IJwtProvider
    {
        Task<LoginCommandResponse> CreateToken(AppUser user);
    }
}
