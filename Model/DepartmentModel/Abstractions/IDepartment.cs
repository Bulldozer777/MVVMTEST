using MVVMTEST.Model.EmployeeModel;

namespace MVVMTEST.Model.DepartmentModel.Abstractions
{
    public interface IDepartment
    {
        public int Id { get; set; }
        public string DepartmentName { get; set; }
        public int Director { get; set; }
        public Employee DepartmentDirector { get; }

    }
}
