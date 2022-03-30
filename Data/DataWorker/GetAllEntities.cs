using MVVMTEST.Model.DepartmentModel;
using MVVMTEST.Model.EmployeeModel;
using MVVMTEST.Model.OrderModel;
using System.Collections.Generic;
using System.Linq;

namespace MVVMTEST.Data.DataWorker
{
    class GetAllEntities
    { 
        //Get All Departments
        public static List<Department> GetAllDepartments()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var result = db.Departments.ToList();
                return result;
            }
        }

        //Get All Order
        public static List<Order> GetAllOrders()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var result = db.Orders.ToList();
                return result;
            }
        }

        //Get All Employee
        public static List<Employee> GetAllEmployees()
        {
            using (ApplicationContext db = new ApplicationContext())
            {        
                var result = db.Employees.ToList();
                return result;
            }
        }
    }
}
