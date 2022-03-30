using MVVMTEST.Data.DataWorker;
using MVVMTEST.Model.EmployeeModel;
using MVVMTEST.Model.OrderModel.Abstractions;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVVMTEST.Model.OrderModel
{
    public class Order : IOrder
    {
        public int Id { get; set; }

        public int Number { get; set; }
        public string OrderName { get; set; }
        public List<Employee> Employees { get; set; } = new();      
        public int EmployeeId { get; set; }

        [NotMapped]
        public List<Employee> OrderEmployees
        {
            get
            {
                return StaticDataOperation.GetAllEmployeesByOrdersId(Id);
            }
        }
        [NotMapped]
        public Employee OrderEmployee
        {
            get
            {
                return StaticDataOperation.GetEmployeeById(EmployeeId);
            }
        }

    }
}
