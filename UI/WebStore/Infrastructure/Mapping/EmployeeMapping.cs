using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Domain.Entities;
using WebStore.ViewModels;

namespace WebStore.Infrastructure.Mapping
{
    public static class EmployeeMapping
    {
        public static EmployeeViewModel ToView(this Employee x) => new EmployeeViewModel
        {
            Id = x.Id,
            Name = x.Name,
            SecondName = x.SurName,
            Patronymic = x.Patronymic,
            Age = x.Age
        };


        public static Employee FromView(this EmployeeViewModel employee) => new Employee
        {
            Id = employee.Id,
            Name = employee.Name,
            SurName = employee.SecondName,
            Patronymic = employee.Patronymic,
            Age = employee.Age
        };
    }
}
