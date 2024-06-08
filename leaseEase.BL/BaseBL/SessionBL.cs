using leaseEase.BL.Interfaces;
using leaseEase.BL.MainAPI;
using leaseEase.BL.Repos;
using leaseEase.DAL;
using leaseEase.Domain.Models.Responces;
using leaseEase.Domain.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace leaseEase.BL.BaseBL
{
    public class SessionBL : UserAPI, ISession
    {

        public BaseResponces RegisterUserActionFlow(UserRegisterData urData, ILeaseEaseRepository repo)
        {
            return Task.Run(()=>RegisterUserAccountAsync(urData, repo)).Result;
        }

        public BaseResponces LoginUserActionFlow(UserLoginData ulData, ILeaseEaseRepository repo)
        {
            return Task.Run(() => UserLogin(ulData, repo)).Result;
        }
        public HttpCookie CookieGenerate(string Email, ILeaseEaseRepository repo)
        {
            return Task.Run(() => CookieGenByEmail(Email, repo)).Result;
        }
        public UserMinData GetUserByCookie(string cookie, ILeaseEaseRepository repo)
        {
            return Task.Run(() => GetUserByCookieApi(cookie, repo)).Result;
        }
    }
}
