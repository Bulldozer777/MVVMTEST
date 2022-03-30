using MVVMTEST.Data.DataWorker.Abstractions;
using MVVMTEST.Model.DepartmentModel;
using MVVMTEST.Model.EmployeeModel;
using MVVMTEST.Model.OrderModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using static MVVMTEST.Model.EmployeeModel.Employee;

namespace MVVMTEST.Data.DataWorker
{
    public class DataWorker : IDataWorker
    {
        public TabItem SelectedTabItem { get ; set; }

        //Get All Departments
        public List<Department> GetAllDepartments()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var result = db.Departments.ToList();
                return result;
            }
        }

        //Get All Order
        public List<Order> GetAllOrders()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                //var result = db.Orders.ToList();
                var result = new List<Order>();
                return result;
            }
        }

        //Get All Employee
        public List<Employee> GetAllEmployees()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var result = db.Employees.ToList();
                return result;
            }
        }

        //Crete Department
        public string CreateDepartment(string name, Employee directorEmployee)
        {
            string result = "Уже существует";
            using (ApplicationContext db = new ApplicationContext())
            {
                bool checkIsExist = db.Departments.Any(x => x.DepartmentName == name);
                if (!checkIsExist)
                {
                    Department newdepartment = new Department
                    {
                        DepartmentName = name,
                        Director = directorEmployee.Id
                    };
                    db.Departments.Add(newdepartment);
                    db.SaveChanges();
                    result = "Выполнено!";
                }
            }
            return result;
        }


        //Crete Order
        public string CreateOrder(int number, string ordername, Employee employees/*, int employeeId*/)
        {
            string result = "Уже существует";
            using (ApplicationContext db = new ApplicationContext())
            {
                bool checkIsExist = db.Orders.Any(x => x.OrderName == ordername &&
                x.Number == number);
                if (!checkIsExist)
                {
                    Order newOrder = new Order
                    {
                        OrderName = ordername,
                        Number = number,
                        EmployeeId = employees.Id
                    };
                    db.Orders.Add(newOrder);
                    db.SaveChanges();
                    result = "Выполнено!";
                }
            }
            return result;
        }


        //Crete Employee
        public string CreateEmployee(string name, string surname, string patronymic,
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
                        DepartmentId = department.Id
                };                    
                    db.Employees.Add(newEmployee);
                    db.SaveChanges();
                    result = "Выполнено!";
                }
            }
            return result;
        }

        //Delete Deparment
        public string DeleteDepartment(Department department)
        {
            string result = "Такого подразделения не существует";
            using (ApplicationContext db = new ApplicationContext())
            {
                db.Departments.Remove(department);
                db.SaveChanges();
                result = "Выполнено! Отдел " + department.DepartmentName + " удален";
            }
            return result;
        }


        //Delete Order
        public string DeleteOrder(Order order)
        {
            string result = "Такого заказа не существует";
            using (ApplicationContext db = new ApplicationContext())
            {
                db.Orders.Remove(order);
                db.SaveChanges();
                result = "Выполнено! Заказ " + order.OrderName + " удален";
            }
            return result;
        }

        //Delete Employee
        public string DeleteEmployee(Employee employee)
        {
            string result = "Такого сотрудника не существует";
            using (ApplicationContext db = new ApplicationContext())
            {
                db.Employees.Remove(employee);
                db.SaveChanges();
                result = "Выполнено! Сотрудник " + employee.Name + " удален";
            }
            return result;
        }

        //Edit Department
        public string EditDepartment(Department oldDepartment, string newNewdepartmentName,
            Employee newDirector)
        {
            string result = "Такого подразделения не существует";
            using (ApplicationContext db = new ApplicationContext())
            {
                Department newdepartment = db.Departments.FirstOrDefault(x => x.Id == oldDepartment.Id);
                newdepartment.DepartmentName = newNewdepartmentName;
                newdepartment.Director = newDirector.Id;
                db.SaveChanges();
                result = "Выполнено! Отдел " + newdepartment.DepartmentName + " изменен";
            }
            return result;
        }


        //Edit Order
        public string EditOrder(Order oldOrder, int newNumber, string newOrderName,
            Employee newEmployees)
        {
            string result = "Такого заказа не существует";
            using (ApplicationContext db = new ApplicationContext())
            {
                Order newOrder = db.Orders.FirstOrDefault(x => x.Id == oldOrder.Id);
                if (newOrder != null)
                {
                    newOrder.Number = newNumber;
                    newOrder.OrderName = newOrderName;
                    newOrder.EmployeeId = newEmployees.Id;
                    db.SaveChanges();
                    result = "Выполнено! Заказ " + newOrder.OrderName + " изменен";
                }
            }
            return result;
        }

        //Edit Emloyee
        public string EditEmployee(Employee oldEmployee, string newSurname, string newName,
            string newPatronymic,
            DateTime newBirthday, TypesGender newGender, Department newDepartments)
        {
            string result = "Такого заказа не существует";
            using (ApplicationContext db = new ApplicationContext())
            {
                Employee newEmployee = db.Employees.FirstOrDefault(x => x.Id == oldEmployee.Id);
                if (newEmployee != null)
                {
                    newEmployee.Surname = newSurname;
                    newEmployee.Name = newName;
                    newEmployee.Patronymic = newPatronymic;
                    newEmployee.Birthday = newBirthday;
                    newEmployee.Gender = newGender;
                    newEmployee.DepartmentId = newDepartments.Id;
                    db.SaveChanges();
                    result = "Выполнено! Сотрудник " + newEmployee.Name + " изменен";
                }
            }
            return result;
        }

        //получение всех заказов по id пользователя
        public List<Order> GetAllOrdersByEmployeesId(int id)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                List<Order> orders = (from order in GetAllEntities.GetAllOrders() where order.EmployeeId == id select order).ToList();
                return orders;
            }
        }
        //получение сотрудника по id сотрудника
        public Employee GetEmployeeById(int id)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                Employee pos = db.Employees.FirstOrDefault(p => p.Id == id);
                return pos;
            }
        }

        //получение отдела по id отдела
        public Department GetDepartmentById(int id)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                Department pos = db.Departments.FirstOrDefault(p => p.Id == id);
                return pos;
            }
        }
        //получение всех пользователей по id заказа
        public List<Employee> GetAllEmployeesByOrdersId(int id)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                List<Employee> employees = (from employee in GetAllEntities.GetAllEmployees() where employee.OrdersId == id select employee).ToList();
                return employees;
            }
        }
        //получение всех пользователей по id подразделения
        public List<Employee> GetAllEmployeesByDepartmentId(int id)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                List<Employee> employees = (from employee in GetAllEntities.GetAllEmployees() where employee.DepartmentId == id select employee).ToList();
                return employees;
            }
        }
    }
}
