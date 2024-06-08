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

namespace leaseEase.BL.Interfaces
{
    public interface ISession
    {
        BaseResponces RegisterUserActionFlow(UserRegisterData urData, ILeaseEaseRepository repo);
        BaseResponces LoginUserActionFlow(UserLoginData ulData, ILeaseEaseRepository repo);
        HttpCookie CookieGenerate(string Email, ILeaseEaseRepository repo);
        UserMinData GetUserByCookie(string cookie, ILeaseEaseRepository repo);
    }
}
