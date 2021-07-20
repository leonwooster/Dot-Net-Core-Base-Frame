using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.AspNetCore.Http;

using Dksh.ePOD.Data;
using Dksh.ePOD.Entities;

namespace Dksh.ePOD.Services
{
    public interface IPerson
    {
        List<AddressTypeBO> GetAddressTypes();
        int CreateAddressType(AddressTypeBO bo);
    }

    public class PersonService : ServiceBase, IPerson
    {
        public PersonService(DataContext ctx, IHttpContextAccessor httpContextAccessor) :
            base(ctx, httpContextAccessor)
        {

        }

        public int CreateAddressType(AddressTypeBO bo)
        {
            this.DbContext.AddressType.Add(bo);
            this.DbContext.AuditTrail.Add(new AuditTrailBO()
            {
                Action = "Add new address type",
                ActionedBy = "James",
                ActionedByName = "James",
                ActionedDate = DateTime.Now,
                Attachment = "",
                Comments = "A new address type creation by James",
                RequestID = 0

            });

            this.DbContext.SaveChanges();

            return bo.AddressTypeID;
        }

        public List<AddressTypeBO> GetAddressTypes()
        {
            return this.DbContext.AddressType.ToList();
        }
    }
}
