using leaseEase.BL.Repos;
using System;
using System.Web.Mvc;
using System.Web.Routing;

namespace leaseEase.Web.App_Start
{
    public class CustomControllerActivator : IControllerActivator
    {
        private readonly ILeaseEaseRepository _repo;

        public CustomControllerActivator(ILeaseEaseRepository repo)
        {
            _repo = repo;
        }

        public IController Create(RequestContext requestContext, Type controllerType)
        {
            return (IController)Activator.CreateInstance(controllerType, _repo);
        }
    }

}