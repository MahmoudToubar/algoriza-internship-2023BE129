using Microsoft.AspNetCore.Identity;
using VezeetaManagement.Models.Domain;

namespace VezeetaManagement.Repositories.Interface
{
    public interface ITokenRepository
    {
        string CreateJwtToken(IdentityUser user , List<string> roles);
    }
}
