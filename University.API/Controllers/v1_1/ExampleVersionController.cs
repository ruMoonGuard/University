using Microsoft.AspNetCore.Mvc;
using University.API.Code.Models.v1_1;

namespace University.API.Controllers.v1_1
{
    [ApiVersion("1.1")]
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class ExampleVersionController : ControllerBase
    {
        [HttpGet]
        public ActionResult<ExampleVersionModel> Get()
        {
            return new ExampleVersionModel
            {
                Id = 1,
                Name = "Test version"
            };
        }
    }
}
