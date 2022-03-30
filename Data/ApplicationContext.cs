using Microsoft.EntityFrameworkCore;
using MVVMTEST.Model.DepartmentModel;
using MVVMTEST.Model.EmployeeModel;
using MVVMTEST.Model.OrderModel;
using System;
using static MVVMTEST.Model.EmployeeModel.Employee;

namespace MVVMTEST.Data
{
    class ApplicationContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Order> Orders { get; set; }
        public ApplicationContext()
        {
            //Database.EnsureDeleted();
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=WaterCarrier;Trusted_Connection=True;");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            DateTime date = new DateTime(1997, 7, 20);

            // определяем отделы
            Department cadrAdmin = new Department
            {
                Id = 1,
                DepartmentName = "Отдел кадрового администрирования",
                Director = 1 

            };
            Department systemAdmin = new Department
            {
                Id = 2,
                DepartmentName = "Группа системного администрирования",
                Director = 3
            };

            // определяем сотрудников
            Employee nataly = new Employee
            {
                Id = 1,
                Name = "Наталья",
                Surname = "Зинченко",
                Patronymic = "Константиновна",
                Birthday = date,
                Gender = (TypesGender)1,
                DepartmentId = cadrAdmin.Id
            };
            Employee vladimir = new Employee
            {
                Id = 2,
                Name = "Владимир",
                Surname = "Жарков",
                Patronymic = "Алексеевич",
                Birthday = date.AddMonths(3),
                Gender = 0,
                DepartmentId = cadrAdmin.Id
            };
            Employee nadejda = new Employee 
            { 
                Id = 3,
                Name = "Надежда",
                Surname = "Макарова",
                Patronymic = "Викторовна",
                Birthday = date.AddYears(2),
                Gender = (TypesGender)1,
                DepartmentId = systemAdmin.Id
            };

            Order water1 = new Order
            {
                Id = 1,
                Number = 1,
                OrderName = "Вода 5 л",
                EmployeeId = vladimir.Id
            };

            Order water2 = new Order
            {
                Id = 2,
                Number = 2,
                OrderName = "Вода 1,5 л",
                EmployeeId = vladimir.Id
            };

            Order water3 = new Order
            {
                Id = 3,
                Number = 3,
                OrderName = "Вода 0,5 л",
                EmployeeId = vladimir.Id
            };

            Order water4 = new Order
            {
                Id = 4,
                Number = 2,
                OrderName = "Вода 1,5 л",
                EmployeeId = nataly.Id
            };

            // добавляем данные для обеих сущностей
            modelBuilder.Entity<Employee>().HasData(nataly, vladimir, nadejda);
            modelBuilder.Entity<Department>().HasData(cadrAdmin, systemAdmin);
            modelBuilder.Entity<Order>().HasData(water1, water2, water3, water4);
        }
    }
}
