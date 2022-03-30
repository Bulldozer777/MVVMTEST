using MVVMTEST.Model.DepartmentModel;
using MVVMTEST.Model.EmployeeModel;
using MVVMTEST.Model.OrderModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MVVMTEST.Model.EmployeeModel.Employee;

namespace MVVMTEST.Data.DataWorker
{
    public static class StaticDataOperation
    {

        //Crete Employee
        public static string CreateEmployee(string name, string surname, string patronymic,
            DateTime birthday, TypesGender gender, int departmentId, Department department)
        {
            string result = "Уже существует";
            using (ApplicationContext db = new ApplicationContext())
            {
                bool checkIsExist = db.Employees.Any(x => x.Surname == surname &&
                x.Name == name &&
                x.Patronymic == patronymic &&
                x.Gender == gender
                );
                if (!checkIsExist)
                {
                    Employee newEmployee = new Employee
                    {
                        Surname = surname,
                        Name = name,
                        Patronymic = patronymic,
                        Birthday = birthday,
                        Gender = (TypesGender)gender,
                        Department = department,
                        DepartmentId = departmentId
                    };
                    db.Employees.Add(newEmployee);
                    db.SaveChanges();
                    result = "Выполнено!";
                }
            }
            return result;
        }

        //получение отдела по id отдела
        public static Department GetDepartmentById(int id)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                Department pos = db.Departments.FirstOrDefault(p => p.Id == id);
                return pos;
            }
        }

        public static List<Employee> GetAllEmployeesByDepartmentId(int id)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                List<Employee> employees = (from employee in GetAllEntities.GetAllEmployees() where employee.DepartmentId == id select employee).ToList();
                return employees;
            }
        }
        //получение сотрудника по id сотрудника
        public static Employee GetEmployeeById(int id)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                Employee pos = db.Employees.FirstOrDefault(p => p.Id == id);
                return pos;
            }
        }

        //получение всех пользователей по id заказа
        public static List<Employee> GetAllEmployeesByOrdersId(int id)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                List<Employee> employees = (from employee in GetAllEntities.GetAllEmployees() where employee.OrdersId == id select employee).ToList();
                return employees;
            }
        }

        //получение всех заказов по id пользователя
        public static List<Order> GetAllOrdersByEmployeesId(int id)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                List<Order> orders = (from order in GetAllEntities.GetAllOrders() where order.EmployeeId == id select order).ToList();
                return orders;
            }
        }
    }
}
