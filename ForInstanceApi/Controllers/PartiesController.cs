using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InsuranceApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PartiesController : ControllerBase
    {
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            return new OkObjectResult($"Your party fetched successfully for {id}.");
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return new OkObjectResult($"Your parties fetched successfully for products list.");
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] string model)
        {
            return new OkObjectResult($"Your party created successfully for {model}");
        }

        [HttpPut]
        public async Task<IActionResult> Edit([FromBody] string model)
        {
            return new OkObjectResult($"Your party updated successfully for {model}");
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] string id)
        {
            return new OkObjectResult($"Your party deleted successfully for {id}");
        }
    }
}
