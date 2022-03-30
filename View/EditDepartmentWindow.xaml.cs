using MVVMTEST.Model.DepartmentModel;
using MVVMTEST.ViewModel.DepartmentService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MVVMTEST.View
{
    /// <summary>
    /// Логика взаимодействия для EditDepartmentWindow.xaml
    /// </summary>
    public partial class EditDepartmentWindow : Window
    {
        public EditDepartmentWindow(Department department)
        {
            InitializeComponent();
            DepartmentViewModel.SelectedDepartment = department;
            DepartmentViewModel departmentViewModel = new DepartmentViewModel();
            departmentViewModel.DepartmentName = department.DepartmentName;
            departmentViewModel.DepartmentDirector = department.DepartmentDirector;
        }
    }
}
