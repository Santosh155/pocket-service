using Microsoft.AspNetCore.Mvc;

namespace pocket_service.Controller
{
    [ApiController]
    [Route("api/health")]
    public class HealthController: ControllerBase
    {
        [HttpGet]
        public IActionResult Get() => Ok(new{status = "Healthy", time = DateTime.UtcNow});
    }

}