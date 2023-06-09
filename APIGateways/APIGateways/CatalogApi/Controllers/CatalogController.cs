using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CatalogApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatalogController : ControllerBase
    {
        // GET: api/<CatalogController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "Catalog1", "Catalog2" };
        }

        // GET api/<CatalogController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return $"catalog {id}";
        }

        [HttpGet("{id}/item")]
        public Dictionary<string, string> GetCatalogItem(int id)
        {

            return new Dictionary<string, string>
            {
                { "Samsung", $"s10 form catalog id {id} " },
                { "Iphone", "8" }
            };
        }

        // POST api/<CatalogController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<CatalogController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<CatalogController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
