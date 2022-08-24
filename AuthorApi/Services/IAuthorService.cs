using CommonDbLayer.DatabaseEntity;

namespace AuthorApi.Services
{
    public interface IAuthorService
    {
        string AddAccount(UserDetail user);
        string ValidateUser(string userName, string password);
    }
}
