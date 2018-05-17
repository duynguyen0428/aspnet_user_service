using System;
using web_api.Model;
using web_api.Persistence;
using System.Threading.Tasks;
namespace web_api.Service
{
    public class UserService : IUserService {
        private readonly web_api_DbContext _context;
        public UserService(web_api_DbContext context)
        {
            this._context = context;
        }
        public async Task<User> Register(string email, string password){
           throw new NotImplementedException();
        }

        public async Task<User> GetUser(string email){
            throw new NotImplementedException();
        }

        public async Task<bool> IsUserExist(string email){
           throw new NotImplementedException();
        }

        public async Task<User> Login(string email, string pwd){
           throw new NotImplementedException();
        }
    }
}