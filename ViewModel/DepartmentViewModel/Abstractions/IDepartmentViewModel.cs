using MVVMTEST.Model;
using MVVMTEST.Model.DepartmentModel;
using MVVMTEST.Model.DepartmentModel.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace MVVMTEST.ViewModel.DepartmentService.Abstractions
{
    public interface IDepartmentViewModel : IDepartment
    {
        public RelayCommand EditDepartment { get; }
        public RelayCommand OpenEditDepartmentItemWnd { get; }
        public TabItem SelectedTabItem { get; set; }
        public static Department SelectedDepartment { get; set; }
        public RelayCommand DeleteDepartment { get; }
        public RelayCommand OpenDepartmentWnd { get; }
        public RelayCommand OpenAddNewDepartmentWnd { get; }
        public RelayCommand AddNewDepartment { get; }
        void OpenAddDepartentWindow();
        public void SetNullValuesToProperties();
        public List<Department> AllDepartments { get; set; }
    }
}
