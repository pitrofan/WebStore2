using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebStore.Domain;
using WebStore.Domain.Entities;
using WebStore.Interfaces.Services;

namespace WebStore.ServiceHosting.Controllers
{
    //[Route("api/[controller]")]
    [Route(WebApi.Employees)]
    [ApiController]
    public class EmployeesApiController : ControllerBase, IEmployeesData
    {
        private readonly IEmployeesData employeesData;

        public EmployeesApiController(IEmployeesData employeesData) => this.employeesData = employeesData;

        [HttpPost]
        public void Add([FromBody] Employee employee) => employeesData.Add(employee);

        [HttpDelete("{id}")]
        public bool Delete(int id) => employeesData.Delete(id);

        [HttpPut("{id}")]
        public void Edit(int id, [FromBody] Employee employee) => employeesData.Edit(id, employee);

        [HttpGet]
        public IEnumerable<Employee> GetAll() => employeesData.GetAll();

        [HttpGet("{id}")]
        public Employee GetById(int id) => employeesData.GetById(id);

        public void SaveChanges() => employeesData.SaveChanges();
    }
}