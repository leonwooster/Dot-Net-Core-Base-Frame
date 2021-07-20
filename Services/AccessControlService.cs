using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Security;

using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

using Dksh.ePOD.Data;
using Dksh.ePOD.Entities;

namespace Dksh.ePOD.Services
{
    public interface IAccessControlService
    {
        /// <summary>
        /// Retrieve all the related access control information based on the passed in app id.
        /// </summary>
        /// <param name="appId">Application Id</param>
        /// <returns>List of access controls for the application.</returns>
        public List<AccessControlBO> GetAccessControl(long appId);
        /// <summary>
        /// A function that checks if the passed in username and application id exist in the database.
        /// Usually use this function to check for user eligibility to access a system.
        /// </summary>
        /// <param name="username">username can be in email format or AD format</param>
        /// <param name="appId">application id in numeric value</param>
        /// <returns>true = user can access, false = user cannot access</returns>
        public bool CanAccess(string username, long appId);
    }
    public class AccessControlService : ServiceBase, IAccessControlService
    {
        private readonly AccessControlDA _da;
        public AccessControlService(AccessControlDA ctx, IHttpContextAccessor httpContextAccessor) :
         base(httpContextAccessor)
        {
            _da = ctx;
        }
        public List<AccessControlBO> GetAccessControl(long appId)
        {
            var r = _da.MST_AccessControl.Where(e => e.ApplicationID == appId).AsNoTracking().ToList();
            return r;
        }

        public bool CanAccess(string username, long appId)
        {
            var lst = GetAccessControl(appId); //get user from K2 DB.
            var usernameLower = username.ToLower();

            var found = lst.Where(e => e.Access.ToLower() == usernameLower).FirstOrDefault();

            if (found != null) //TODO: check for the related country also.
                return true;
            else return false;
        }
    }
}
