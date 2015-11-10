using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlatOne.Auth.Business.Entity;
using PlatOne.Auth.Business.Manager;

namespace PlatOne.Auth.Service
{
    public class AuthorizationService : IAuthorizationService
    {
        public IList<Business.Entity.ResourceEntity> GetResourceByRole(int roleID)
        {
            throw new NotImplementedException();
        }

        public IList<Business.Entity.ResourceEntity> GetResourceByUser(int userID)
        {
            var rm = new ResourceManager();
            return rm.GetUserResource(userID);
        }
    }
}
