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
    /// <summary>
    /// Контроллер управления сотрудниками
    /// </summary>
    //[Route("api/[controller]")]
    [Route(WebApi.Employees)]
    [ApiController]
    public class EmployeesApiController : ControllerBase, IEmployeesData
    {
        private readonly IEmployeesData employeesData;

        public EmployeesApiController(IEmployeesData employeesData) => this.employeesData = employeesData;


        /// <summary>
        /// Добавить сотрудника
        /// </summary>
        /// <param name="employee">Новый сотрудник</param>
        [HttpPost]
        public void Add([FromBody] Employee employee) => employeesData.Add(employee);

        /// <summary>
        /// Удалить сотрудника
        /// </summary>
        /// <param name="id">Идентификатор удаляемого сотрудника</param>
        /// <returns>Истина, если сотрудник успешно удален</returns>
        [HttpDelete("{id}")]
        public bool Delete(int id) => employeesData.Delete(id);

        /// <summary>
        /// Редактирование сотрудника
        /// </summary>
        /// <param name="id">Идентификатор редактируемого сотрудника</param>
        /// <param name="employee">Измененная информаци вносимая в БД</param>
        [HttpPut("{id}")]
        public void Edit(int id, [FromBody] Employee employee) => employeesData.Edit(id, employee);

        /// <summary>
        /// Получить всех сотрудников
        /// </summary>
        /// <returns>Список сотрудников</returns>
        [HttpGet]
        public IEnumerable<Employee> GetAll() => employeesData.GetAll();

        /// <summary>
        /// Получить сотрудника по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор запрашиваемого сотрудника</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public Employee GetById(int id) => employeesData.GetById(id);

        [NonAction]
        public void SaveChanges() => employeesData.SaveChanges();
    }
}