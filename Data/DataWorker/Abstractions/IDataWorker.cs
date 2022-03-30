using MVVMTEST.Model.DepartmentModel;
using MVVMTEST.Model.EmployeeModel;
using MVVMTEST.Model.OrderModel;
using System;
using System.Collections.Generic;
using System.Windows.Controls;
using static MVVMTEST.Model.EmployeeModel.Employee;

namespace MVVMTEST.Data.DataWorker.Abstractions
{
    public interface IDataWorker
    {
        public TabItem SelectedTabItem { get; set; }
        List<Department> GetAllDepartments();
        List<Employee> GetAllEmployees();
        List<Order> GetAllOrders();

        //Crete Department
        public string CreateDepartment(string name, Employee directorEmployee);

        //Delete Deparment
        public string DeleteDepartment(Department department);

        //Edit Department
        public string EditDepartment(Department oldDepartment, string newNewdepartmentName,
        Employee newDirector);

        public string CreateEmployee(string name, string surname, string patronymic,
        DateTime birthday, TypesGender gender, int departmentId, Department department);

        public string EditEmployee(Employee oldEmployee, string newSurname, string newName,
        string newPatronymic, DateTime newBirthday, TypesGender newGender, Department newDepartments);
        public string CreateOrder(int number, string ordername, Employee employees);
        public string EditOrder(Order oldOrder, int newNumber, string newOrderName,
        Employee newEmployees);
        public string DeleteOrder(Order order);

        public string DeleteEmployee(Employee order);

        public List<Employee> GetAllEmployeesByDepartmentId(int id);
        public List<Employee> GetAllEmployeesByOrdersId(int id);
        public Department GetDepartmentById(int id);
        public List<Order> GetAllOrdersByEmployeesId(int id);
        public Employee GetEmployeeById(int id);
    }    
}
