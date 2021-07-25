using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http.Headers;
using System.Text.Json;

using Microsoft.AspNetCore.Http;

using Dksh.ePOD.Web;
using Dksh.ePOD.Data;
using Dksh.ePOD.Entities;
using Dksh.ePOD.Configuration;

namespace Dksh.ePOD.Services
{
    public interface IPerson
    {
        List<AddressTypeBO> GetAddressTypes();
        int CreateAddressType(AddressTypeBO bo);

        Task<List<AddressTypeBO>> GetAddressTypesAPI();
        Task<int> CreateAddressTypeAPI(AddressTypeBO bo);
    }

    public class PersonService : ServiceBase, IPerson
    {
        private HttpClient _client;

        public PersonService(DataContext ctx, IHttpContextAccessor httpContextAccessor, IUsualConfig config) :
            base(ctx, httpContextAccessor, config)
        {
            _client = new HttpClient();
            _client.BaseAddress = new Uri(config.WebAPIUrl);
        }

        #region DB call in same project
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
        #endregion

        #region Call from Web API
        public async Task<List<AddressTypeBO>> GetAddressTypesAPI()
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync("/Get");
            List<AddressTypeBO> result = null;

            if (response.IsSuccessStatusCode)
            {
                result = await JsonSerializer.DeserializeAsync<List<AddressTypeBO>>(await response.Content.ReadAsStreamAsync());
            }
            return result;
        }

        public async Task<int> CreateAddressTypeAPI(AddressTypeBO bo)
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.PostAsync(base.AppConfig.WebAPIUrl, new JsonContent(bo));
            int result = 0;

            if (response.IsSuccessStatusCode)
            {
                result = await JsonSerializer.DeserializeAsync<int>(await response.Content.ReadAsStreamAsync());
            }
            return result;

        }
        #endregion
    }
}
