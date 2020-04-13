using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using WebStore.Clients.Base;
using WebStore.Domain;
using WebStore.Domain.Entities;
using WebStore.Interfaces.Services;

namespace WebStore.Clients.Employees
{
    public class EmployeesClient : BaseClient, IEmployeesData
    {

        public EmployeesClient(IConfiguration configuration) : base(configuration, WebApi.Employees) { }


        public void Add(Employee employee) => Post<Employee>(serviceAddress, employee);

        public bool Delete(int id) => Delete($"{serviceAddress}/{id}").IsSuccessStatusCode;

        public void Edit(int id, Employee employee) => Put<Employee>($"{serviceAddress}/{id}", employee);

        public IEnumerable<Employee> GetAll() => Get<List<Employee>>(serviceAddress);

        public Employee GetById(int id) => Get<Employee>($"{serviceAddress}/{id}");

        public void SaveChanges() { }
    }
}
