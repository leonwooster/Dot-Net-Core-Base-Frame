using System;
using System.Linq;
using System.Security.Claims;

using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

using Dksh.ePOD.Data;
using Dksh.ePOD.Configuration;

namespace Dksh.ePOD.Services
{
    public class ServiceBase
    {
        protected readonly IHttpContextAccessor WebContext;
        protected readonly DataContext DbContext;
        protected readonly IAccessControlService AccessControl;
        protected readonly IUsualConfig AppConfig;

        public ServiceBase(IHttpContextAccessor httpContextAccessor)
        {
            WebContext = httpContextAccessor;
        }
        public ServiceBase(DataContext ctx, IAccessControlService accessControl, IHttpContextAccessor httpContextAccessor)
        {
            WebContext = httpContextAccessor;
            DbContext = ctx;
            AccessControl = accessControl;
        }
        public ServiceBase(DataContext ctx, IAccessControlService accessControl, IHttpContextAccessor httpContextAccessor, IUsualConfig config)
        {
            WebContext = httpContextAccessor;
            DbContext = ctx;
            AccessControl = accessControl;
            AppConfig = config;
        }

        public virtual bool CanDo(string id)
        {
            var user = WebContext.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
            var tid = WebContext.HttpContext.User.FindFirst(e => e.Type.Contains("tenantid"));

            if (AppConfig == null) throw new Exception("AppConfig is not initialized.");
            if (AppConfig.AppId <= 0 || string.IsNullOrEmpty(AppConfig.TenantId)) throw new Exception("Invalid app id or tenant id passed in");

            if (user.Value == id) //TPI request
                return true;
            else
            {
                if (AccessControl.CanAccess(WebContext.HttpContext.User.Identity.Name, AppConfig.AppId))
                    return true;  //AD login.
                else return false;
            }
        }
    }
}
