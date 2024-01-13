using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace veterinaria_yara_ux.api.Controllers
{
    //[Authorize]
    [ApiController]
    public abstract class BaseApiController : ControllerBase
    {
    }
}
