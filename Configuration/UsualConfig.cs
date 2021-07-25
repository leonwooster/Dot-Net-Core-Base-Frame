using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dksh.ePOD.Configuration
{
    public interface IUsualConfig
    {
        long AppId { get; set; }
        string TenantId { get; set; }
        string WebAPIUrl { get; set; }
    }
    public class UsualConfig : IUsualConfig
    {
        public long AppId { get; set; }
        public string TenantId { get; set; }
        public string WebAPIUrl { get; set; }
    }
}
