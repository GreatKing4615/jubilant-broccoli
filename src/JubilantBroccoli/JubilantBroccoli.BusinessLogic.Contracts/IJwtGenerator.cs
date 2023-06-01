using JubilantBroccoli.Domain.Dtos.User;
using Microsoft.AspNetCore.Identity;

namespace JubilantBroccoli.BusinessLogic.Contracts
{
    public interface IJwtGenerator
    {
        public AuthenticationResponse CreateToken(IdentityUser user);
    }
}