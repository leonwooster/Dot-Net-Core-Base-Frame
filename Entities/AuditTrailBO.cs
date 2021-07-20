using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dksh.ePOD.Entities
{
    public class AuditTrailBO
    {
        public long ID { get; set; }
        public long? RequestID { get; set; }
        public string Action { get; set; }
        public string Comments { get; set; }
        public DateTime? ActionedDate { get; set; }
        public string ActionedBy { get; set; }
        public string ActionedByName { get; set; }
        public string Attachment { get; set; }
    }
}
