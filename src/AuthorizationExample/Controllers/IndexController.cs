using AuthorizationExample.Common.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthorizationExample.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class IndexController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return Ok(User.GetUserId());
        }
    }
}
