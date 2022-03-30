using MVVMTEST.Data.DataWorker;
using MVVMTEST.Model.DepartmentModel;
using MVVMTEST.Model.EmployeeModel.Abstractions;
using MVVMTEST.Model.OrderModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVVMTEST.Model.EmployeeModel
{
    public class Employee : IEmployee
    {
        public enum TypesGender
        {
            Мужской,
            Женский
        }
        public TypesGender gender;

        public int Id { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string Patronymic { get; set; }
        public DateTime Birthday { get; set; }
        public TypesGender Gender { get; set; }

        public int DepartmentId { get; set; }
#nullable enable
        public Department? Department { get; set; }
        public List<Order> Orders { get; set; } = new();

        [NotMapped]
        public Department EmployeeDepartment
        {
            get
            {
                return StaticDataOperation.GetDepartmentById(DepartmentId);
            }
        }
        [NotMapped]
        public int OrdersId { get; set; }
        [NotMapped]
        public List<Order> EmployeeOrders
        {
            get
            {
                return StaticDataOperation.GetAllOrdersByEmployeesId(Id);
            }
        }
    }
}
