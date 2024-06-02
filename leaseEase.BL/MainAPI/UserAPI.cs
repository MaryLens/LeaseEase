using leaseEase.BL.Repos;
using leaseEase.DAL;
using leaseEase.Domain.Models.Responces;
using leaseEase.Domain.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leaseEase.BL.MainAPI
{
    public class UserAPI
    {
        internal BaseResponces CheckUserCredential(UserLoginData data, ILeaseEaseRepository repo)
        {
            var user = Task.Run(() => repo.GetUserByEmailAsync(data.Credential)).Result; 
            return user != null? new BaseResponces { Status = true, CurrentUser = user} : new BaseResponces { Status = false, StatusMessage = "We didn’t find an account with those login credentials" };
        }
        internal BaseResponces GenerateUserSession(UserLoginData data, ILeaseEaseRepository repo)
        {
            var password = PwManager.Md5Crypt(data.Password);
            var user = Task.Run(() => repo.GetUserByEmailAndPwAsync(data.Credential, password)).Result;
            return user != null ? new BaseResponces { Status = true } : new BaseResponces { Status = false, StatusMessage = "Wrong username or password used" };
        }
        internal async Task<BaseResponces> RegisterUserAccountAsync(UserRegisterData data, ILeaseEaseRepository repo)
        {
            var prev = Task.Run(() => repo.GetUserByEmailAsync(data.Email)).Result;
            if (prev == null)
            {
                var user = await repo.AddUserAsync(data);
                return new BaseResponces { Status = true };
            }
           
            return new BaseResponces { Status = false, StatusMessage = "User with those signup credentials already exists" };
        }
        internal UCookieData UserCoockieGenerationAlg(User user)
        {
            return new UCookieData
            {
                MaxAge = 1709044385,
                Coockie = "MY UNIQUE ID FOR THIS SESSION"
            };
        }
    }
}
