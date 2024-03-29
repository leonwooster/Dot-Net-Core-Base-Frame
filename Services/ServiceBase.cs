﻿using System;
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
        protected readonly IUsualConfig AppConfig;

        public ServiceBase(IHttpContextAccessor httpContextAccessor)
        {
            WebContext = httpContextAccessor;
        }
        public ServiceBase(DataContext ctx)
        {            
            DbContext = ctx;
        }
        public ServiceBase(DataContext ctx, IHttpContextAccessor httpContextAccessor)
        {
            WebContext = httpContextAccessor;
            DbContext = ctx;            
        }
        public ServiceBase(DataContext ctx, IHttpContextAccessor httpContextAccessor, IUsualConfig config)
        {
            WebContext = httpContextAccessor;
            DbContext = ctx;            
            AppConfig = config;
        }
    }
}
