﻿using System.Collections.Generic;
using System.Linq;

using Dksh.ePOD.Data;
using Dksh.ePOD.Entities;

using Microsoft.AspNetCore.Http;
using Microsoft.VisualBasic;

using Org.BouncyCastle.Bcpg;

namespace Dksh.ePOD.Services
{    
    public interface ICommonDataService
    {

    }

    public class CommonDataService : ServiceBase, ICommonDataService
    {
        DataContext _ctx;
        public CommonDataService(DataContext ctx, IHttpContextAccessor httpContextAccessor) :
            base(ctx, httpContextAccessor)
        {
            _ctx = ctx;
        }

        public void CreateAuditTrail(AuditTrailBO bo)
        {
            bo.ActionedByName = bo.ActionedBy = base.WebContext.HttpContext.User.Identity.Name;
            bo.ActionedDate = DateAndTime.Now;

            _ctx.tbl_DKSH_TPI_AUDIT_TRAIL.Add(bo);

            _ctx.SaveChanges();
        }

        public IEnumerable<AuditTrailBO> GetAuditTrails()
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<CommonDataBO> GetData(string country)
        {
            if (string.IsNullOrEmpty(country))
                throw new AppException("Passed in parameter cannot be null.");

            var r = _ctx.tbl_DKSH_TPI_KEYWORDS.Where(e => e.country_code == country).ToList();

            return r;
        }
    }
}
