using Microsoft.AspNetCore.Mvc;

namespace Shopping.Aggregator.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class BaseController : ControllerBase
    {
    }
}
