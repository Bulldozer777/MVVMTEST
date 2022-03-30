using MVVMTEST.Model.EmployeeModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMTEST.Model.OrderModel.Abstractions
{
    public interface IOrder
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public string OrderName { get; set; }
        public List<Employee> Employees { get; set; }
        public int EmployeeId { get; set; }

        public List<Employee> OrderEmployees
        {
            get ;
        }
        public Employee OrderEmployee
        {
            get;
        }
    }
}
