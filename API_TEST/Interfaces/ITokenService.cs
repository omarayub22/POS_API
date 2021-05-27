using API_TEST.Models;

namespace API_TEST.Interfaces
{
    public interface ITokenService
    {
         string CreateToken(AppUser user);
    }
}