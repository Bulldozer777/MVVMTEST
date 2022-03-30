using MVVMTEST.Data.DataWorker;
using MVVMTEST.Data.DataWorker.Abstractions;
using MVVMTEST.Model;
using MVVMTEST.Model.DepartmentModel;
using MVVMTEST.Model.EmployeeModel;
using MVVMTEST.Model.OrderModel;
using MVVMTEST.View;
using MVVMTEST.ViewModel.EmployeeService.Abstractions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using static MVVMTEST.Model.EmployeeModel.Employee;

namespace MVVMTEST.ViewModel.EmployeeService
{
    public class EmployeeViewModel : IEmployeeViewModel, INotifyPropertyChanged
    {

        private IDataWorker dataWorker;

        public EmployeeViewModel(IDataWorker Dataworker)
        {
            this.dataWorker = Dataworker;
        }

        public EmployeeViewModel()
        {

        }

        #region PROPERTIES EMPLOYEE
        public int Id { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string Patronymic { get; set; }

        private DateTime birthday = DateTime.Now;
        public DateTime Birthday
        {
            get { return birthday; }
            set { birthday = value; }
        }
        public TypesGender Gender { get; set; }
        public int DepartmentId { get; set; }
        public Department Department { get; set; }
        public List<Order> Orders { get; set; }

        [NotMapped]
        public int OrdersId { get; set; }      
        private Department employeeDepartment;
        public Department EmployeeDepartment1
        {
            get { return employeeDepartment; }
            set
            {
                employeeDepartment = value;
                OnPropertyChanged("EmployeeDepartment1");
            }
        }
        public Department EmployeeDepartment
        {
            get { return employeeDepartment; }
            set
            {
                employeeDepartment = value;
                OnPropertyChanged("EmployeeDepartment");
            }
        }

        private List<TypesGender> allGender = AllGender();

        public List<TypesGender> GenderTypes
        {
            get { return allGender; }
            set
            {
                allGender = value;
                OnPropertyChanged("GenderTypes");
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
                OnPropertyChanged("AllEmployees");
            }
        }
        //All AllDepartments
        private List<Department> allDepartments = GetAllEntities.GetAllDepartments();
        public List<Department> AllDepartments
        {
            get { return allDepartments; }
            set
            {
                allDepartments = value;
                OnPropertyChanged("AllDepartments");
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
                OnPropertyChanged("AllEmployees");
            }
        }

        public static Employee SelectedEmployee { get; set; }

        #endregion

        #region COMMAND TO OPEN WINDOW EMPLOYEE

        private RelayCommand openEmployeeWnd;

        public RelayCommand OpenEmployeeWnd
        {
            get
            {
                return openEmployeeWnd ?? new RelayCommand(obj =>
                {
                    OpenEmployeeWindow();
                }
                );
            }
        }

        #endregion

        #region COMMAND TO OPEN ADD NEW EMPLOYEE WINDOW 

        //Employee

        private RelayCommand openAddNewEmployeeWnd;
        public RelayCommand OpenAddNewEmployeeWnd
        {
            get
            {
                return openAddNewEmployeeWnd ?? new RelayCommand(obj =>
                {
                    OpenAddEmployeeWindow();
                }
          );
            }
        }

        #endregion

        #region COMMAND TO ADD EMPLOYEE

        private RelayCommand addNewEmployee;

        public RelayCommand AddNewEmployee
        {
            get
            {
                return addNewEmployee ?? new RelayCommand(obj =>
                {
                    Window wnd = obj as Window;
                    string resultStr = "";
                    if (Surname == null || Surname.Replace(" ", "").Length == 0)
                    {
                        OperationWindow.SetRedBlockContorol(wnd, "SurnameBlock");
                    }
                    if (Name == null || Name.Replace(" ", "").Length == 0)
                    {
                        OperationWindow.SetRedBlockContorol(wnd, "NameBlock");
                    }
                    if (Patronymic == null || Patronymic.Replace(" ", "").Length == 0)
                    {
                        OperationWindow.SetRedBlockContorol(wnd, "PatronymicBlock");
                    }
                    if (Birthday == DateTime.Now)
                    {
                        MessageBox.Show("Укажите дату рождения");
                    } 
                    if (EmployeeDepartment == null)
                    {
                        MessageBox.Show("Укажите департамент");
                    }
                    else
                    {
                        resultStr = dataWorker.CreateEmployee(Name, Surname, Patronymic, Birthday, Gender, 1, EmployeeDepartment);
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

        #region COMMAND TO OPEN EDIT EMPLOYEE WINDOW

        private RelayCommand openEditEmployeeItemWnd;
        public RelayCommand OpenEditEmployeeItemWnd
        {
            get
            {
                return openEditEmployeeItemWnd ?? new RelayCommand(obj =>
                {
                    string resultStr = "Ничего не выбрано";
                    if (SelectedItem.SelectedTabItem.Name == "EmployeeTab" && SelectedEmployee != null)
                    {
                        OpenEditUserWindowMethod(SelectedEmployee);
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

        #region COMMAND TO EDIT EMPLOYEE

        private RelayCommand editEmployee;
        public RelayCommand EditEmployee
        {
            get
            {
                return editEmployee ?? new RelayCommand(obj =>
                {
                    Window window = obj as Window;
                    string resultStr = "Не выбран сотрудник";
                    string noDepartmentStr = "Не выбран новый отдел";
                    if (SelectedEmployee != null)
                    {
                        if (EmployeeDepartment != null)
                        {
                            resultStr = dataWorker.EditEmployee(SelectedEmployee, Surname, Name, Patronymic, Birthday, Gender, EmployeeDepartment);
                            OperationWindow.UpdateAllDataView();
                            SetNullValuesToProperties();
                            OperationWindow.ShowMessageToUser(resultStr);
                            window.Close();
                        }
                        else OperationWindow.ShowMessageToUser(noDepartmentStr);
                    }
                    else OperationWindow.ShowMessageToUser(resultStr);

                }
                );
            }
        }

        #endregion

        #region COMMAND TO DELETE EMPLOYEE

        private RelayCommand deleteEmployee;
        public RelayCommand DeleteEmployee
        {
            get
            {
                return deleteEmployee ?? new RelayCommand(obj =>
                {
                    string resultStr = "Ничего не выбрано";
                    if (SelectedItem.SelectedTabItem.Name == "EmployeeTab" && SelectedEmployee != null)
                    {
                        resultStr = dataWorker.DeleteEmployee(SelectedEmployee);
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

        #region METHODS EMPLOYEE

        private void OpenEditUserWindowMethod(Employee Employee)
        {
            EditEmployeeWindow editEmployeeWindow = new EditEmployeeWindow(Employee);
            OperationWindow.SetCenterPositionAndOpen(editEmployeeWindow);
        }
        public void OpenAddEmployeeWindow()
        {
            AddNewEmployeeWindow addNewDepartmentWindow = new AddNewEmployeeWindow();
            OperationWindow.SetCenterPositionAndOpen(addNewDepartmentWindow);
        }
        public void OpenEmployeeWindow()
        {
            OpenEmployeeWindow addNewDepartmentWindow = new OpenEmployeeWindow();
            OperationWindow.SetCenterPositionAndOpen(addNewDepartmentWindow);
        }
        public void SetNullValuesToProperties()
        {
            //for Employee

            Surname = null;
            Name = null;
            Patronymic = null;
            Birthday = DateTime.Now;
            Gender = 0;
            Department = null;
            DepartmentId = 0;
            EmployeeDepartment = null;

        }

        public static List<TypesGender> AllGender()
        {
            List<TypesGender> list = new List<TypesGender>();
            var EnumList = Enum.GetValues(typeof(TypesGender)).Cast<TypesGender>();
            foreach (var p in EnumList)
            {
                list.Add(p);
            }
            return list;
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
