using CommonDbLayer.DatabaseEntity;
using System.Linq;

namespace AuthorApi.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly MyDigitalBookDbContext _dbcontext;
        public AuthorService(MyDigitalBookDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public string AddAccount(UserDetail user)
        {
            try
            {

                _dbcontext.UserDetails.Add(user);
                _dbcontext.SaveChanges();
                return $"User Created Successfully";


            }
            catch (Exception ex)
            {

                return ex.Message;
            }
        }
        public string ValidateUser(string userName, string password)
        {
            try
            {
                var user = _dbcontext.UserDetails.Select(x => x.UserName).ToList();
                if (user.Contains(userName))
                {
                    var pass = _dbcontext.UserDetails.Where(x => x.UserName == userName).Select(x => x.Password).FirstOrDefault();
                    if (pass == password)
                    {
                        return $"User logged in successfully";
                    }
                    else
                    {
                        return $"Username or password is incorrect. Please try again";
                    }
                }
                else
                {
                    return $"User Account doesn't exist.";
                }

            }
            catch (Exception ex)
            {

                return ex.Message;
            }
        }
    }
}
