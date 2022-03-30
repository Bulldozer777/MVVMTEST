using MVVMTEST.Data.DataWorker;
using MVVMTEST.Data.DataWorker.Abstractions;
using MVVMTEST.Model;
using MVVMTEST.Model.DepartmentModel;
using MVVMTEST.Model.EmployeeModel;
using MVVMTEST.Model.OrderModel;
using MVVMTEST.View;
using MVVMTEST.ViewModel.Abstractions;
using MVVMTEST.ViewModel.DepartmentService.Abstractions;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;

namespace MVVMTEST.ViewModel.DepartmentService
{
    public class DepartmentViewModel : IDepartmentViewModel, INotifyPropertyChanged, ISelectedItem
    {
        private IDataWorker dataWorker;

        public DepartmentViewModel(IDataWorker Dataworker)
        {
            this.dataWorker = Dataworker;
        }

        public DepartmentViewModel()
        {

        }

        #region PROPERTIES DEPARTMENT
        public int Id { get; set; }

        private string departmentname;
        public string DepartmentName
        {
            get { return departmentname; }
            set
            {
                departmentname = value;
                OnPropertyChanged("DepartmentName");
            }
        }
        public int Director { get; set; }   

        private Employee departmentDirector;
        public Employee DepartmentDirector
        {
            get { return departmentDirector; }
            set
            {
                departmentDirector = value;
                OnPropertyChanged("DepartmentDirector");
            }
        }

        //All Departments
        private List<Department> allDepartments = GetAllEntities.GetAllDepartments();
        public List<Department> AllDepartments
        {
            get { return allDepartments; }
            set
            {
                allDepartments = value;
            }
        }

        //All Orders
        private List<Order> allOrders = GetAllEntities.GetAllOrders();
        public List<Order> AllOrders
        {
            get { return allOrders; }
            set
            {
                allOrders = value;
            }
        }

        //All Employee
        private List<Employee> allEmployees = GetAllEntities.GetAllEmployees();
        public List<Employee> AllEmployees
        {
            get { return allEmployees; }
            set
            {
                allEmployees = value;
            }
        }
        public TabItem SelectedTabItem { get; set; }
        public static Department SelectedDepartment { get; set; }

        #endregion

        #region COMMAND TO OPEN WINDOW DEPARTMENT

        private RelayCommand openDepartmentWnd;

        public RelayCommand OpenDepartmentWnd
        {
            get
            {
                return openDepartmentWnd ?? new RelayCommand(obj =>
                {
                    OpenDepartentWindow();
                }
                );
            }
        }

        #endregion

        #region COMMAND TO OPEN ADD NEW DEPARTMENT WINDOW

        private RelayCommand openAddNewDepartmentWnd;

        public RelayCommand OpenAddNewDepartmentWnd
        {
            get
            {
                return openAddNewDepartmentWnd ?? new RelayCommand(obj =>
                {
                    OpenAddDepartentWindow();
                }
                );
            }
        }

        #endregion   

        #region COMMAND TO ADD DEPARTMENT

        private RelayCommand addNewDepartment;

        public RelayCommand AddNewDepartment
        {
            get
            {
                return addNewDepartment ?? new RelayCommand(obj =>
                {
                    Window wnd = obj as Window;
                    string resultStr = "";
                    Department department = new Department();
                    if (DepartmentName == null || DepartmentName.Replace(" ", "").Length == 0)
                    {
                        OperationWindow.SetRedBlockContorol(wnd, "DepartmentName");
                    }
                    if (DepartmentDirector == null)
                    {
                        MessageBox.Show("Поле Director - пустое");
                    }
                    else
                    {
                        resultStr = dataWorker.CreateDepartment(DepartmentName,DepartmentDirector);
                        OperationWindow.UpdateAllDataView();
                        OperationWindow.ShowMessageToUser(resultStr);
                        SetNullValuesToProperties();
                        wnd.Close();
                    }
                }
                );
            }
        }
        #endregion

        #region COMMAND TO OPEN EDIT DEPARTMENT WINDOW

        private RelayCommand openEditDepartmentItemWnd;
        public RelayCommand OpenEditDepartmentItemWnd
        {
            get
            {
                return openEditDepartmentItemWnd ?? new RelayCommand(obj =>
                {
                    string resultStr = "Ничего не выбрано";
                    if (SelectedItem.SelectedTabItem.Name == "DepartmentTab" && SelectedDepartment != null)
                    {
                        OpenEditDepartmentWindowMethod(SelectedDepartment);
                    }
                    else
                    {
                        OperationWindow.ShowMessageToUser(resultStr);
                    }
                }
                    );
            }
        }

        #endregion

        #region COMMAND TO EDIT DEPARTMENT

        private RelayCommand editDepartment;
        public RelayCommand EditDepartment
        {
            get
            {
                return editDepartment ?? new RelayCommand(obj =>
                {
                    Window window = obj as Window;
                    string resultStr = "Не выбран отдел";
                    string noDirectorStr = "Не выбран директор";
                    if (SelectedDepartment != null)
                    {
                        if (DepartmentDirector != null)
                        {
                            resultStr = dataWorker.EditDepartment(SelectedDepartment, DepartmentName, DepartmentDirector);
                            OperationWindow.UpdateAllDataView();
                            SetNullValuesToProperties();
                            OperationWindow.ShowMessageToUser(resultStr);
                            window.Close();
                        }
                        else OperationWindow.ShowMessageToUser(noDirectorStr);
                    }
                    else OperationWindow.ShowMessageToUser(resultStr);

                }
                );
            }
        }


        #endregion

        #region COMMAND TO DELETE DEPARTMENT

        private RelayCommand deleteDepartment;

        public RelayCommand DeleteDepartment
        {
            get
            {
                return deleteDepartment ?? new RelayCommand(obj =>
                {
                    string resultStr = "Ничего не выбрано";
                    //если сотрудник
                    SelectedItem selectedItem = new SelectedItem();
                    if (SelectedItem.SelectedTabItem.Name == "DepartmentTab" && SelectedDepartment != null)
                    {
                        resultStr = dataWorker.DeleteDepartment(SelectedDepartment);
                        OperationWindow.UpdateAllDataView();
                    }
                    //обновление
                    SetNullValuesToProperties();
                    OperationWindow.ShowMessageToUser(resultStr);
                }
                    );
            }
        }

        #endregion

        #region METHODS DEPARTMENT
        //Department


        private void OpenEditDepartmentWindowMethod(Department Department)
        {
            EditDepartmentWindow editDepartmentWindow = new EditDepartmentWindow(Department);
            OperationWindow.SetCenterPositionAndOpen(editDepartmentWindow);
        }
        public void SetNullValuesToProperties()
        {
            //for Department
            DepartmentName = null;
            Director = 0;
            DepartmentDirector = null;
        }
        public void OpenAddDepartentWindow()
        {
            AddNewDepartmentWindow addNewDepartmentWindow = new AddNewDepartmentWindow();
            OperationWindow.SetCenterPositionAndOpen(addNewDepartmentWindow);
        }
        public void OpenDepartentWindow()
        {
            OpenDepartmentWindow addNewDepartmentWindow = new OpenDepartmentWindow();
            OperationWindow.SetCenterPositionAndOpen(addNewDepartmentWindow);
        }

        #endregion
          

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
