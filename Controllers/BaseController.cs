using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dksh.ePOD.Controllers
{
    public class BaseController : Controller
    {
       public string Country
        {
            get
            {
                return "MY";
            }
        }
    }
}
