using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebStore.Domain;

namespace WebStore.ServiceHosting.Controllers
{
    [Route(WebApi.Identity.Users)]
    [ApiController]
    public class UserApiController : ControllerBase
    {
    }
}