using Microsoft.Extensions.DependencyInjection;
using MVVMTEST.Data.DataWorker;
using MVVMTEST.Data.DataWorker.Abstractions;
using MVVMTEST.Model.DepartmentModel.Abstractions;
using MVVMTEST.ViewModel;
using MVVMTEST.ViewModel.Abstractions;
using MVVMTEST.ViewModel.DepartmentService;
using MVVMTEST.ViewModel.DepartmentService.Abstractions;
using MVVMTEST.ViewModel.EmployeeService;
using MVVMTEST.ViewModel.EmployeeService.Abstractions;
using MVVMTEST.ViewModel.OrderService;
using MVVMTEST.ViewModel.OrderService.Abstractions;

namespace MVVMTEST.DependencyInjection
{
    public class ViewModelLocator
    {
        private static ServiceProvider _provider;
        public static void Init()
        {
            var services = new ServiceCollection();

            services.AddScoped<IDepartmentViewModel, DepartmentViewModel>();
            services.AddScoped<IEmployeeViewModel, EmployeeViewModel>();
            services.AddScoped<IDepartment, DepartmentViewModel>();
            services.AddTransient<IDataWorker, DataWorker>();
            services.AddScoped<IOrderViewModel, OrderViewModel>();
            services.AddSingleton<GetAllEntities>();
            services.AddSingleton<OperationWindow>();
            services.AddTransient<ISelectedItem, SelectedItem>();
            services.AddTransient<SelectedItem>();

            _provider = services.BuildServiceProvider();

            foreach (var item in services)
            {
                _provider.GetRequiredService(item.ServiceType);
            }
        }

        public SelectedItem SelectedItem => _provider.GetRequiredService<SelectedItem>();
        public IDepartmentViewModel DepartmentViewModel => _provider.GetRequiredService<IDepartmentViewModel>();
        public IEmployeeViewModel EmployeeViewModel => _provider.GetRequiredService<IEmployeeViewModel>();
        public IDepartment DepartmentModel => _provider.GetRequiredService<IDepartment>();
        public IDataWorker DataWorker => _provider.GetRequiredService<IDataWorker>();
        public IOrderViewModel OrderViewModel => _provider.GetRequiredService<IOrderViewModel>();
        public ISelectedItem ISelectedItem => _provider.GetRequiredService<ISelectedItem>();
    }
}
