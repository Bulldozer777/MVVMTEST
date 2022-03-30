using MVVMTEST.Data.DataWorker;
using MVVMTEST.Data.DataWorker.Abstractions;
using MVVMTEST.Model;
using MVVMTEST.Model.EmployeeModel;
using MVVMTEST.Model.OrderModel;
using MVVMTEST.View;
using MVVMTEST.ViewModel.Abstractions;
using MVVMTEST.ViewModel.OrderService.Abstractions;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;
using System.Windows;

namespace MVVMTEST.ViewModel.OrderService
{
    public class OrderViewModel : IOrderViewModel, INotifyPropertyChanged, ISelectedItem
    {
        private IDataWorker dataWorker;
        public OrderViewModel(IDataWorker Dataworker)
        {
            this.dataWorker = Dataworker;
        }
        public OrderViewModel()
        {

        }

        #region PROPERTIES ORDER


        public int Id { get ; set; }
        public int Number { get ; set ; }
        public string OrderName { get; set ; }
        public List<Employee> Employees { get ; set ; }

        [NotMapped]
        public int EmployeeId { get; set; }


        private List<Employee> orderEmployees;
        [NotMapped]
        public List<Employee> OrderEmployees
        {
            get { return orderEmployees; }
            set
            {
                orderEmployees = value;
                OnPropertyChanged("OrderEmployees");
            }
        }
        private Employee orderEmployee;
        [NotMapped]
        public Employee OrderEmployee
        {
            get { return orderEmployee; }
            set
            {
                orderEmployee = value;
                OnPropertyChanged("OrderEmployees");
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

        public static Order SelectedOrder { get; set; }

        #endregion

        #region COMMAND TO OPEN WINDOW ORDER

        private RelayCommand openOrderWnd;

        public RelayCommand OpenOrderWnd
        {
            get
            {
                return openOrderWnd ?? new RelayCommand(obj =>
                {
                    OpenOrderWindow();
                }
                );
            }
        }

        #endregion

        #region COMMAND TO OPEN ADD NEW ORDER WINDOW

        //Order
        private RelayCommand openAddNewOrderWnd;
        public RelayCommand OpenAddNewOrderWnd
        {
            get
            {
                return openAddNewOrderWnd ?? new RelayCommand(obj =>
                {
                    OpenAddOrderWindow();
                }
          );
            }
        }

        #endregion

        #region COMMAND TO ADD ORDER

        private RelayCommand addNewOrder;
        public RelayCommand AddNewOrder
        {
            get
            {
                return addNewOrder ?? new RelayCommand(obj =>
                {
                    Window wnd = obj as Window;
                    string resultStr = "";
                    if (Number == 0)
                    {
                        OperationWindow.SetRedBlockContorol(wnd, "NumberBlock");
                    }
                    if (OrderName == null || OrderName.Replace(" ", "").Length == 0)
                    {
                        OperationWindow.SetRedBlockContorol(wnd, "OrderNameBlock");
                    }
                    if (OrderEmployee == null)
                    {
                        MessageBox.Show("Укажите сотрудника");
                    }
                    else
                    {
                        resultStr = dataWorker.CreateOrder(Number, OrderName, OrderEmployee);
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

        #region COMMAND TO OPEN EDIT ORDER WINDOW

        private RelayCommand openEditOrderItemWnd;
        public RelayCommand OpenEditOrderItemWnd
        {
            get
            {
                return openEditOrderItemWnd ?? new RelayCommand(obj =>
                {
                    string resultStr = "Ничего не выбрано";
                    if (SelectedItem.SelectedTabItem.Name == "OrdersTab" && SelectedOrder != null)
                    {
                        OpenEditOrderWindowMethod(SelectedOrder);
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

        #region COMMAND TO EDIT ORDER

        private RelayCommand editOrder;
        public RelayCommand EditOrder
        {
            get
            {
                return editOrder ?? new RelayCommand(obj =>
                {
                    Window window = obj as Window;
                    string resultStr = "Не выбран сотрудник";
                    string noDepartmentStr = "Не выбран новый отдел";
                    if (SelectedOrder != null)
                    {
                        if (OrderEmployee != null)
                        {
                            resultStr = dataWorker.EditOrder(SelectedOrder, Number, OrderName, OrderEmployee);
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

        #region COMMAND TO DELETE ORDER

        private RelayCommand deleteDepartment;

        public RelayCommand DeleteOrder
        {
            get
            {
                return deleteDepartment ?? new RelayCommand(obj =>
                {
                    string resultStr = "Ничего не выбрано";
                    if (SelectedItem.SelectedTabItem.Name == "OrdersTab" && SelectedOrder != null)
                    {
                        resultStr = dataWorker.DeleteOrder(SelectedOrder);
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

        #region METHODS ORDER

        //Order

        private void OpenEditOrderWindowMethod(Order Order)
        {
            EditOrderWindow editEmployeeWindow = new EditOrderWindow(Order);
            OperationWindow.SetCenterPositionAndOpen(editEmployeeWindow);
        }
        private void OpenAddOrderWindow()
        {
            AddNewOrderWindow addNewDepartmentWindow = new AddNewOrderWindow();
            OperationWindow.SetCenterPositionAndOpen(addNewDepartmentWindow);
        }
        private void OpenOrderWindow()
        {
            OpenOrderWindow addNewDepartmentWindow = new OpenOrderWindow();
            OperationWindow.SetCenterPositionAndOpen(addNewDepartmentWindow);
        }
        public void SetNullValuesToProperties()
        {
            //for Order
            Number = 0;
            OrderName = null;
            Employees = null;
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
