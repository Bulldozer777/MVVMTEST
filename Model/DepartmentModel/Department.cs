using MVVMTEST.Data.DataWorker;
using MVVMTEST.Model.DepartmentModel.Abstractions;
using MVVMTEST.Model.EmployeeModel;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVVMTEST.Model.DepartmentModel
{
    public class Department : IDepartment
    { 
        public int Id { get; set; }
        public string DepartmentName { get; set; }
        public int Director { get; set; }
        public List<Employee> DirectorEmployee { get; set; } = new();

        [NotMapped]

        public Employee DepartmentDirector
        {
            get
            {
                return StaticDataOperation.GetEmployeeById(Director);
            }
        }
    }
}