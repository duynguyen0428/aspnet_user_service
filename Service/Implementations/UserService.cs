using System;
using web_api.Model;
using web_api.Persistence;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace web_api.Service
{
    public class UserService : IUserService {
        private readonly web_api_DbContext _context;
        public UserService(web_api_DbContext context)
        {
            this._context = context;
        }

        public Task<User> GetUser(string email)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> IsUserExist(string email)
        {
            // throw new NotImplementedException();
            return await this._context.Users.FirstOrDefaultAsync(u => u.Email.Equals(email)) != null;
        }

        public async Task<User> Login(string email, string pwd)
        {
            // throw new NotImplementedException();
            // 1. Find user 

            var user = await this._context.Users.FirstOrDefaultAsync(u => u.Email == email);
            // System.Console.WriteLine("User found :" + user);
            if(user == null)
                return null;
            
            //compare password
            var comparedPWD = VerifyPassword(pwd, user.HashedPassword, user.SaltPassword);
            // System.Console.WriteLine("Compared PWD found :" + comparedPWD);            
            if(!comparedPWD)
                return null;

            return user;
        }

        private bool VerifyPassword(string pwd, byte[] hashedPassword, byte[] saltPassword)
        {
            // throw new NotImplementedException();
            using(var hmac = new System.Security.Cryptography.HMACSHA512(saltPassword)){
                 var computedHashed = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(pwd));
                //  System.Console.WriteLine("Computed hashed: " + computedHashed + "\nHasedPassword: " + hashedPassword);
                 for(int i = 0; i < computedHashed.Length; i ++){
                     if(computedHashed[i] != hashedPassword[i]){
                        // System.Console.WriteLine("Computed hashed: " + computedHashed[i] + "\nHasedPassword: " + hashedPassword[i]);                     
                        return false;
                     }
                 }
           }
           return true;
        }

        public async Task<User> Register(User user, string password){
        //    throw new NotImplementedException();
            byte[] hashedPWD;
            byte[] saltPWD;

            // Hash password 
            HashPWD(password, out hashedPWD, out saltPWD);

            user.HashedPassword = hashedPWD;
            user.SaltPassword = saltPWD;
            await this._context.Users.AddAsync(user);

            await this._context.SaveChangesAsync();

            return user;
        }

        private void HashPWD(string password, out byte[] hashedPWD, out byte[] saltPWD)
        {
            // throw new NotImplementedException();
           using(var hmac = new System.Security.Cryptography.HMACSHA512()){
               saltPWD = hmac.Key;
               hashedPWD = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
           }
        }
    }
}