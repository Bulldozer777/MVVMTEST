using MVVMTEST.Model.DepartmentModel;
using MVVMTEST.Model.EmployeeModel;
using MVVMTEST.Model.OrderModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMTEST.ViewModel.Abstractions
{
    public interface IPropertiesAllEntities
    {
        public static List<Employee> AllEmployees { get; set; }
        public static List<Department> AllDepartments { get; set; }
        public static List<Order> AllOrders { get; set; }
    }
}
