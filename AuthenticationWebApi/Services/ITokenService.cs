using AuthenticationWebApi.Models;
using CommonDbLayer.DatabaseEntity;

namespace AuthenticationWebApi.Services
{
    public interface ITokenService
    {
        string BuildToken(string key, string issuer, IEnumerable<string> audience, string userName, string usertype);
        List<UserDetail> validateuser(ValidateUserCredentials usersdetails);
    }
}