using leaseEase.BL.Helpers;
using leaseEase.BL.Repos;
using leaseEase.DAL;
using leaseEase.Domain.Models.Responces;
using leaseEase.Domain.Models.User;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace leaseEase.BL.MainAPI
{
    public class UserAPI
    {
        internal async Task<BaseResponces> UserLogin(UserLoginData data, ILeaseEaseRepository repo)
        {
            var password = PwManager.Md5Crypt(data.Password);
            var user = Task.Run(() => repo.GetUserByEmailAndPwAsync(data.Credential, password)).Result;
            if (user == null) { 
                return new BaseResponces { Status = false, StatusMessage = "Wrong username or password used" }; 
            }
            user.LastLogin = data.LastLogin;
            user.UserIp = data.UserIp;
            await repo.UpdateUserAsync(user);
            return new BaseResponces { Status = true };
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
        internal async Task<UserMinData> GetUserByCookieApi(string cookie, ILeaseEaseRepository repo)
        {
            var session = Task.Run(() => repo.GetSessionByCookieAsync(cookie)).Result;
            if (session != null)
            {
                if (session.Lifetime < DateTime.Now)
                {
                    await repo.RemoveSessionAsync(session);
                }
                var user = Task.Run(() => repo.GetUserByEmailAsync(session.Email)).Result;
                if (user == null)
                {
                    return null;
                }
                var uMinData = new UserMinData { Username = user.Name, Role = user.Role, Email = user.Email };
                if (user.creatorData != null)
                {
                    uMinData.Image = user.creatorData.Image;
                }
                return uMinData;
            }
            return null;

        }
        internal async Task<HttpCookie> CookieGenByEmail(string Email, ILeaseEaseRepository repo)
        {
            var cookies = new HttpCookie("LEASEEASE")
            {
                Value = CookieGeneration.Create(Email)
            };
            var current = Task.Run(() => repo.GetSessionByEmailAsync(Email)).Result;

            if (current != null)
            {
                current.CookieString = cookies.Value;
                current.Lifetime = DateTime.Now.AddHours(2);
                await repo.UpdateSessionAsync(current);
            }
            else
            {

               await repo.AddNewSessionAsync(new UDbSession
                {
                    Email = Email,
                    CookieString = cookies.Value,
                    Lifetime = DateTime.Now.AddHours(2)
                });
            }
                return cookies;
            }
        
    }
}
