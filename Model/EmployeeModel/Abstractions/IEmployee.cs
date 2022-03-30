using MVVMTEST.Model.DepartmentModel;
using MVVMTEST.Model.OrderModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MVVMTEST.Model.EmployeeModel.Employee;

namespace MVVMTEST.Model.EmployeeModel.Abstractions
{
    public interface IEmployee
    {
        public int Id { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string Patronymic { get; set; }
        public DateTime Birthday { get; set; }
        public TypesGender Gender { get; set; }
        public int DepartmentId { get; set; }
#nullable enable
        public Department? Department { get; set; }
        public List<Order> Orders { get; set; }
        [NotMapped]
        public int OrdersId { get; set; }
        [NotMapped]
        public Department EmployeeDepartment { get; }
    }
}
