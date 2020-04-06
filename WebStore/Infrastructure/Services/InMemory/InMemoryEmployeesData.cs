using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Data;
using WebStore.Domain.Entities;
using WebStore.Infrastructure.Interfaces;

namespace WebStore.Infrastructure.Services.InMemory
{
	public class InMemoryEmployeesData : IEmployeesData
	{
		readonly List<Employee> _employees = TestData.Employees;

		public void Add(Employee employee)
		{
			if (employee is null)
				throw new ArgumentNullException(nameof(Employee));

			if (_employees.Contains(employee))
				return;
			employee.Id = _employees.Count == 0 ? 1 : _employees.Max(x => x.Id) + 1;

			_employees.Add(employee);
		}

		public bool Delete(int id)
		{
			var db_employee = GetById(id);
			if (db_employee is null)
				return false;

			return _employees.Remove(db_employee);
		}

		public void Edit(int id, Employee employee)
		{
			if(employee is null)
				throw new ArgumentNullException(nameof(Employee));

			if (_employees.Contains(employee)) 
				return;

			var db_employee = GetById(id);
			if (db_employee is null)
				return;

			db_employee.SurName = employee.SurName;
			db_employee.Name = employee.Name;
			db_employee.Patronymic = employee.Patronymic;
			db_employee.Age = employee.Age;
		}

		public IEnumerable<Employee> GetAll() => _employees;

		public Employee GetById(int id) => _employees.FirstOrDefault(x => x.Id == id);


		public void SaveChanges() { }
	}
}
