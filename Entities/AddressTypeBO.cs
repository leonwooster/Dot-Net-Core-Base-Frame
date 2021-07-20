using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dksh.ePOD.Entities
{
    public class AddressTypeBO
    {
        public int AddressTypeID { get; set; }
        public string Name { get; set; }
        public Guid rowguid { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
