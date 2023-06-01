using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthTask2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Buyer, Manager")]
    public class CartingController : ControllerBase
    {
        // GET: api/<CartingController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<CartingController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<CartingController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<CartingController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<CartingController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
