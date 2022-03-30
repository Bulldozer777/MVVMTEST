using MVVMTEST.Data.DataWorker;
using MVVMTEST.Model.DepartmentModel;
using MVVMTEST.Model.DepartmentModel.Abstractions;
using MVVMTEST.Model.EmployeeModel;
using MVVMTEST.Model.EmployeeModel.Abstractions;
using MVVMTEST.Model.OrderModel;
using MVVMTEST.Model.OrderModel.Abstractions;
using MVVMTEST.View;
using MVVMTEST.ViewModel.Abstractions;
using MVVMTEST.ViewModel.DepartmentService;
using MVVMTEST.ViewModel.EmployeeService;
using MVVMTEST.ViewModel.OrderService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace MVVMTEST.ViewModel
{
    public class OperationWindow : IPropertiesAllEntities
    {
        public static List<Employee> AllEmployees { get ; set ; }
        public static List<Department> AllDepartments { get; set; }
        public static List<Order> AllOrders { get; set; }

        public static void SetRedBlockContorol(Window wnd, string blockName)
        {
            Control block = wnd.FindName(blockName) as Control;
            block.BorderBrush = Brushes.Red;
        }
        public static void ShowMessageToUser(string message)
        {
            MessageView messageView = new MessageView(message);
            SetCenterPositionAndOpen(messageView);
        }
        public static void SetCenterPositionAndOpen(Window window)
        {
            window.Owner = Application.Current.MainWindow;
            window.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            window.ShowDialog();
        }
        public static void UpdateAllDataView()
        {
            UpdateAllDepartmentsView();
            UpdateAllOrdersView();
            UpdateAllEmployeesView();
        }
        private static void UpdateAllDepartmentsView()
        {
            AllDepartments = GetAllEntities.GetAllDepartments();
            Window1.AllDepartmentsView.ItemsSource = null;
            Window1.AllDepartmentsView.Items.Clear();
            Window1.AllDepartmentsView.ItemsSource = AllDepartments;
            Window1.AllDepartmentsView.Items.Refresh();
        }
        private static void UpdateAllOrdersView()
        {
            AllOrders = GetAllEntities.GetAllOrders();
            Window1.AllOrdersView.ItemsSource = null;
            Window1.AllOrdersView.Items.Clear();
            Window1.AllOrdersView.ItemsSource = AllOrders;
            Window1.AllOrdersView.Items.Refresh();
        }
        private static void UpdateAllEmployeesView()
        {
            AllEmployees = GetAllEntities.GetAllEmployees();
            Window1.AllEmployeesView.ItemsSource = null;
            Window1.AllEmployeesView.Items.Clear();
            Window1.AllEmployeesView.ItemsSource = AllEmployees;
            Window1.AllEmployeesView.Items.Refresh();
        }
    }
}
