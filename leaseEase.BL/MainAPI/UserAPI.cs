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
        internal BaseResponces CheckUserCredential(UserLoginData data, ILeaseEaseRepository repo)
        {
            var user = Task.Run(() => repo.GetUserByEmailAsync(data.Credential)).Result;
            return user != null ? new BaseResponces { Status = true, CurrentUser = user } : new BaseResponces { Status = false, StatusMessage = "We didn’t find an account with those login credentials" };
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
        internal UserMinData GetUserByCookieApi(string cookie)
        {
            using(var db = new SessionContext())
            {
                var session = db.Sessions.FirstOrDefault(o=>o.CookieString == cookie);
                if(session != null)
                {
                    if (session.Lifetime < DateTime.Now)
                    {
                        db.Sessions.Remove(session);
                        db.SaveChanges();
                    }
                    using(var udb = new leaseEaseContext())
                    {
                        var user = udb.Users.FirstOrDefault(o=>o.Email == session.Email);
                        if (user == null) {
                            return null;
                        }
                        var uMinData = new UserMinData { Username = user.Name };
                        return uMinData;
                    }
                }
                return null;
                
            }

        }
        internal HttpCookie CookieGenByUName(string Email)
        {
            var cookies = new HttpCookie("LEASEEASE")
            {
                Value = CookieGeneration.Create(Email)
            };

            using (var db = new SessionContext())
            {
                UDbSession current;

                current = (from el in db.Sessions where el.Email == Email select el).FirstOrDefault();

                if (current != null)
                {
                    current.CookieString = cookies.Value;
                    current.Lifetime = DateTime.Now.AddHours(2);
                    using (var db_context = new SessionContext())
                    {
                        db_context.Entry(current).State = EntityState.Modified;
                        db_context.SaveChanges();
                    }
                }
                else
                {
                    db.Sessions.Add(new UDbSession
                    {
                        Email = Email,
                        CookieString = cookies.Value,
                        Lifetime = DateTime.Now.AddHours(2)
                    });
                    db.SaveChanges();

                }
                return cookies;
            }
        }
    }
}
