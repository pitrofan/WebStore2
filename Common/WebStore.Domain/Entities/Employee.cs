using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Domain.Entities.Base.Interfaces;

namespace WebStore.Domain.Entities
{
	public class Employee: INamedEntity
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string SurName { get; set; }
		public string Patronymic { get; set; }
		public int Age { get; set; }
	}
}
