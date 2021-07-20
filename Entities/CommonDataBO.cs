using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dksh.ePOD.Entities
{
    public class CommonDataBO
    {
        public long id { get; set; }

        public string country_code { get; set; }

        public string key_words { get; set; }

        public string key_values { get; set; }

        public string descriptions { get; set; }
    }
}
