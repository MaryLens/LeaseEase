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

namespace leaseEase.BL.BaseBL
{
    public class SessionBL : UserAPI, ISession
    {
        public BaseResponces GenerateUserSessionActionFlow(UserLoginData ulData, ILeaseEaseRepository repo)
        {
            return GenerateUserSession(ulData, repo);
        }

        public BaseResponces RegisterUserActionFlow(UserRegisterData urData, ILeaseEaseRepository repo)
        {
            return Task.Run(()=>RegisterUserAccountAsync(urData, repo)).Result;
        }

        BaseResponces ISession.ValidaeUserCredentialAction(UserLoginData ulData, ILeaseEaseRepository repo)
        {
            return CheckUserCredential(ulData, repo);
        }
        public UCookieData GenCoockieAlgo(User dataUser)
        {
            return UserCoockieGenerationAlg(dataUser);
        }
    }
}
