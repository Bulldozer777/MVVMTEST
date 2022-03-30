using MVVMTEST.Model;
using MVVMTEST.Model.DepartmentModel;
using MVVMTEST.Model.EmployeeModel;
using MVVMTEST.Model.EmployeeModel.Abstractions;
using MVVMTEST.Model.OrderModel;
using System.Collections.Generic;
using static MVVMTEST.Model.EmployeeModel.Employee;

namespace MVVMTEST.ViewModel.EmployeeService.Abstractions
{
    public interface IEmployeeViewModel : IEmployee
    {
        public static Employee SelectedEmployee { get; set; }
        public RelayCommand DeleteEmployee { get; }
        public RelayCommand EditEmployee { get; }
        public RelayCommand OpenEditEmployeeItemWnd { get; }
        public RelayCommand OpenEmployeeWnd { get; }
        public RelayCommand AddNewEmployee { get; }
        public RelayCommand OpenAddNewEmployeeWnd { get; }
        public void OpenAddEmployeeWindow();
        public void SetNullValuesToProperties();
        public List<Employee> AllEmployees { get; set; }
        public List<Department> AllDepartments { get; set; }
        public List<Order> AllOrders { get; set; }
        public List<TypesGender> GenderTypes { get; set; }
        //public void UpdateAllEmployeesView();
    }
}
