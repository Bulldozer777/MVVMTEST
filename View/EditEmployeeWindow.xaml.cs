using MVVMTEST.Model.EmployeeModel;
using MVVMTEST.ViewModel.EmployeeService;
using System.Windows;

namespace MVVMTEST.View
{
    /// <summary>
    /// Логика взаимодействия для EditEmployeeWindow.xaml
    /// </summary>
    public partial class EditEmployeeWindow : Window 
    {
        public EditEmployeeWindow(Employee employee)
        {
            InitializeComponent();
            EmployeeViewModel.SelectedEmployee = employee;
            EmployeeViewModel employeeViewModel = new EmployeeViewModel();
            employeeViewModel.Name = employee.Name;
            employeeViewModel.Surname = employee.Surname;
            employeeViewModel.Patronymic = employee.Patronymic;
            employeeViewModel.Birthday = employee.Birthday;
            employeeViewModel.Gender = employee.Gender;
        }
    }
}
