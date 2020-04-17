using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebStore.DAL.Context;
using WebStore.Domain;
using WebStore.Domain.Entities.Identity;

namespace WebStore.ServiceHosting.Controllers
{
    [Route(WebApi.Identity.Roles)]
    [ApiController]
    public class RolesApiController : ControllerBase
    {
        readonly RoleStore<Role> roleStore;

        public RolesApiController(WebStoreDB db) => roleStore = new RoleStore<Role>(db);


        [HttpGet("AllRoles")]
        public async Task<IEnumerable<Role>> GetAllRoles() => await roleStore.Roles.ToArrayAsync();

        [HttpPost]
        public async Task<bool> CreateAsync(Role role) => (await roleStore.CreateAsync(role)).Succeeded;

        [HttpPut]
        public async Task<bool> UpdateAsync(Role role) => (await roleStore.UpdateAsync(role)).Succeeded;

        [HttpPost("Delete")]
        public async Task<bool> DeleteAsync(Role role) => (await roleStore.DeleteAsync(role)).Succeeded;

        [HttpPost("GetRoleId")]
        public async Task<string> GetRoleIdAsync(Role role) => await roleStore.GetRoleIdAsync(role);

        [HttpPost("GetRoleName")]
        public async Task<string> GetRoleNameAsync(Role role) => await roleStore.GetRoleNameAsync(role);

        [HttpPost("SetRoleName/{name}")]
        public async Task SetRoleNameAsync(Role role, string name)
        {
            await roleStore.SetRoleNameAsync(role, name);
            await roleStore.UpdateAsync(role);
        }

        [HttpPost("GetNormalizedRoleName")]
        public async Task<string> GetNormalizedRoleNameAsync(Role role) => await roleStore.GetNormalizedRoleNameAsync(role);

        [HttpPost("SetNormalizedRoleName/{name}")]
        public async Task SetNormalizedRoleNameAsync(Role role, string name)
        {
            await roleStore.SetNormalizedRoleNameAsync(role, name);
            await roleStore.UpdateAsync(role);
        }

        [HttpGet("FindById/{id}")]
        public async Task<Role> FindByIdAsync(string id) => await roleStore.FindByIdAsync(id);

        [HttpGet("FindByName/{name}")]
        public async Task<Role> FindByNameAsync(string name) => await roleStore.FindByNameAsync(name);

    }
}