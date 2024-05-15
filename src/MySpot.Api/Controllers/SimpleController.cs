using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySpot.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SimpleController : ControllerBase
    {
        [HttpGet(Name ="GetSimple")]
        public IEnumerable<string> GetStrings() 
        {
            var names = new List<string>
            {
                "Super",
                "Super2"
            };
            return names;
        }
    }
}
