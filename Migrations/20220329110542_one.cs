using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MVVMTEST.Migrations
{
    public partial class one : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DepartmentName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Director = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Number = table.Column<int>(type: "int", nullable: false),
                    OrderName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmployeeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Surname = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Patronymic = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Birthday = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Gender = table.Column<int>(type: "int", nullable: false),
                    DepartmentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Employees_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeOrder",
                columns: table => new
                {
                    EmployeesId = table.Column<int>(type: "int", nullable: false),
                    OrdersId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeOrder", x => new { x.EmployeesId, x.OrdersId });
                    table.ForeignKey(
                        name: "FK_EmployeeOrder_Employees_EmployeesId",
                        column: x => x.EmployeesId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmployeeOrder_Orders_OrdersId",
                        column: x => x.OrdersId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Departments",
                columns: new[] { "Id", "DepartmentName", "Director" },
                values: new object[,]
                {
                    { 1, "Отдел кадрового администрирования", 1 },
                    { 2, "Группа системного администрирования", 3 }
                });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "Id", "EmployeeId", "Number", "OrderName" },
                values: new object[,]
                {
                    { 1, 2, 1, "Вода 5 л" },
                    { 2, 2, 2, "Вода 1,5 л" },
                    { 3, 2, 3, "Вода 0,5 л" },
                    { 4, 1, 2, "Вода 1,5 л" }
                });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "Birthday", "DepartmentId", "Gender", "Name", "Patronymic", "Surname" },
                values: new object[] { 1, new DateTime(1997, 7, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 1, "Наталья", "Константиновна", "Зинченко" });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "Birthday", "DepartmentId", "Gender", "Name", "Patronymic", "Surname" },
                values: new object[] { 2, new DateTime(1997, 10, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 0, "Владимир", "Алексеевич", "Жарков" });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "Birthday", "DepartmentId", "Gender", "Name", "Patronymic", "Surname" },
                values: new object[] { 3, new DateTime(1999, 7, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 1, "Надежда", "Викторовна", "Макарова" });

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeOrder_OrdersId",
                table: "EmployeeOrder",
                column: "OrdersId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_DepartmentId",
                table: "Employees",
                column: "DepartmentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmployeeOrder");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Departments");
        }
    }
}
