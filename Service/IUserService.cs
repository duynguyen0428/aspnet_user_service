using System;
using System.Threading.Tasks;
using web_api.Model;
namespace web_api.Service
{
    public interface IUserService
    {
        Task<User> Register(string email, string password);

        Task<User> GetUser(string email);

        Task<bool> IsUserExist(string email);

        Task<User> Login(string email, string pwd);
    }
}