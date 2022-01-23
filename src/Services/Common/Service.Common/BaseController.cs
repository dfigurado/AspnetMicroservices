using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Service.Common
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class BaseController : ControllerBase
    {
        private ILogger _logger;
        protected ILogger Logger => _logger ?? (_logger = HttpContext.RequestServices.GetService<ILogger>()); 
    }
}
