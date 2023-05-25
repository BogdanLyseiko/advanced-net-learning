using Microsoft.AspNetCore.Mvc;
using QueueTask2.DAL.Entities;
using QueueTask2.Services.Interfaces;

namespace QueueTask2.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CatalogItemController : ControllerBase
    {
        private readonly ICatalogService _catalogService;

        public CatalogItemController(ICatalogService catalogService)
        {
            _catalogService = catalogService;
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] CatalogItem catalogItem)
        {
            _catalogService.Update(id, catalogItem);
        }
    }
}