using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.DAL.Context;
using WebStore.Domain.Entities;
using WebStore.Infrastructure.Interfaces;

namespace WebStore.Infrastructure.Services.InSQL
{
    public class SqlEmployeeData : IEmployeesData
    {
        private readonly WebStoreDB db;
        public SqlEmployeeData(WebStoreDB _db) => db = _db;


        public void Add(Employee employee)
        {
            if (employee is null)
                throw new ArgumentNullException(nameof(Employee));

            if (db.Employees.Contains(employee))
                return;

            db.Employees.Add(employee);
            SaveChanges();
        }

        public bool Delete(int id)
        {
            var db_employee = GetById(id);
            if (db_employee is null)
                return false;
            
            db.Remove(db_employee);
            SaveChanges();
            return true;
        }

        public void Edit(int id, Employee employee)
        {
            if (employee is null)
                throw new ArgumentNullException(nameof(Employee));

            var db_employee = GetById(id);
            if (db_employee is null)
                return;

            db_employee.SurName = employee.SurName;
            db_employee.Name = employee.Name;
            db_employee.Patronymic = employee.Patronymic;
            db_employee.Age = employee.Age;

            SaveChanges();
        }

        public IEnumerable<Employee> GetAll() => db.Employees.AsEnumerable();

        public Employee GetById(int id) => db.Employees.Where(employee => employee.Id == id).FirstOrDefault();

        public void SaveChanges() => db.SaveChanges();
    }
}
