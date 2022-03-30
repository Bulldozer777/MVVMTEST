using MVVMTEST.Model.OrderModel;
using MVVMTEST.ViewModel.OrderService;
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
    /// Логика взаимодействия для EditOrderWindow.xaml
    /// </summary>
    public partial class EditOrderWindow : Window
    {
        public EditOrderWindow(Order order)
        {
            InitializeComponent();
            OrderViewModel.SelectedOrder = order;
            OrderViewModel orderViewModel = new OrderViewModel();
            orderViewModel.Number = order.Number;
            orderViewModel.OrderName = order.OrderName;
        }
    }
}
