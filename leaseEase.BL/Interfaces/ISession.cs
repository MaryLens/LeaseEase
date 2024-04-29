using leaseEase.BL.Repos;
using leaseEase.DAL;
using leaseEase.Domain.Models.Responces;
using leaseEase.Domain.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leaseEase.BL.Interfaces
{
    public interface ISession
    {
        BaseResponces GenerateUserSessionActionFlow(UserLoginData ulData, ILeaseEaseRepository repo);
        BaseResponces RegisterUserActionFlow(UserRegisterData urData, ILeaseEaseRepository repo);
        BaseResponces ValidaeUserCredentialAction(UserLoginData ulData, ILeaseEaseRepository repo);
    }
}
