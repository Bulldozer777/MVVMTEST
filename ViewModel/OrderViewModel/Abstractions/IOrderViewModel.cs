using MVVMTEST.Model;
using MVVMTEST.Model.OrderModel;
using MVVMTEST.Model.OrderModel.Abstractions;
using System.Collections.Generic;

namespace MVVMTEST.ViewModel.OrderService.Abstractions
{
    public interface IOrderViewModel : IOrder
    {
        public RelayCommand EditOrder { get; }
        public RelayCommand OpenEditOrderItemWnd { get; }
        public RelayCommand DeleteOrder { get; }
        public RelayCommand OpenOrderWnd{ get; }
        public List<Order> AllOrders { get; set; }
        public RelayCommand OpenAddNewOrderWnd { get; }
        public RelayCommand AddNewOrder { get; }
    }
}
