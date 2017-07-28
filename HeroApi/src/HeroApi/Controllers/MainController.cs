using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HeroApi.Controllers
{
    [Route("")]
    public class MainController : Controller
    {
        // GET: api/values
        [HttpGet]
        [Produces("text/html")]
        public IActionResult Get()
        {
            return Ok("<html><head><link rel=\"stylesheet\" href=\"https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css\" integrity=\"sha384-BVYiiSIFeK1dGmJRAkycuHAHRg32OmUcww7on3RYdg4Va+PmSTsz/K68vbdEjh4u\" crossorigin=\"anonymous\">"
                + "<link rel='stylesheet' href='https://maxcdn.bootstrapcdn.com/bootswatch/3.3.7/superhero/bootstrap.min.css' crossorigin='anonymous'/>"
                + "</head><body><div class='container'><h1>There's no website here.</h1></div></body></html>" );
        }

        // GET api/values/5
        [HttpGet("/api")]
        public string GetApi()
        {
            return "You found the api";
        }

        
    }
}
