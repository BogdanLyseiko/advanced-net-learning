using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AuthTask1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatalogController : ControllerBase
    {
        //For simplifying i didn't added any logic to endpoints since target of task was show how to implement acess/authenticate flow 

        // GET: api/<CatalogController>
        [HttpGet]
        [Authorize(Policy = "read")]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<CatalogController>/5
        [HttpGet("{id}")]
        [Authorize(Policy = "read")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<CatalogController>
        [HttpPost]
        [Authorize(Policy = "crud")]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<CatalogController>/5
        [HttpPut("{id}")]
        [Authorize(Policy = "crud")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<CatalogController>/5
        [HttpDelete("{id}")]
        [Authorize(Policy = "crud")]
        public void Delete(int id)
        {
        }
    }
}
