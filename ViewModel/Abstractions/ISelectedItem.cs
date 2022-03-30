using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace MVVMTEST.ViewModel.Abstractions
{
    public interface ISelectedItem
    {
        public static TabItem SelectedTabItem { get; set; }
    }
}
