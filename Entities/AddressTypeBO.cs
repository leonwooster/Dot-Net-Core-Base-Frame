using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

namespace Dksh.ePOD.Entities
{
    [Table("AddressType", Schema = "Person")]
    public class AddressTypeBO
    {        
        [Key]
        public int addressTypeID { get; set; }
        public string name { get; set; }
        public Guid rowguid { get; set; }
        public DateTime modifiedDate { get; set; }
    }
}
