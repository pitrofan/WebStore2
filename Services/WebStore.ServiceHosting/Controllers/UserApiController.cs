using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using WebStore.DAL.Context;
using WebStore.Domain;
using WebStore.Domain.Entities.Identity;

namespace WebStore.ServiceHosting.Controllers
{
    [Route(WebApi.Identity.Users)]
    [ApiController]
    public class UserApiController : ControllerBase
    {
        readonly UserStore<User, Role, WebStoreDB> userStore;

        public UserApiController(WebStoreDB db)
        {
            userStore = new UserStore<User, Role, WebStoreDB>(db);
        }
    }
}