using SeatManagement.DTO;
using SeatManagement.Models;
using SeatManagementConsole.Implementation;
using SeatManagementConsole.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeatManagementConsole.View
{
    public class AddEmployee : IDoWork
    {
        public int WorkType => 5;

        public void DoWork()
        {
            IEntityManager<Department> DepartmentManager = new EntityManager<Department>("api/Department");
            IEntityManager<Employee> EmployeeManager = new EntityManager<Employee>("api/Employee");
            Console.Write("Enter the employee name : ");
            string employeeName=Console.ReadLine();
            Console.Clear();

            Console.Write("Menu\n" +
                "1.Add to existing department\n" +
                "2.Add to new department\n" +
                "Enter your choice: ");
            int choice=Convert.ToInt32(Console.ReadLine());
            int departmentId = 0;
            switch (choice)
            {
                case 1:
                    Console.WriteLine("Available departments");
                    var departmentList = DepartmentManager.Get();
                    foreach(var  department in departmentList)
                    {
                        Console.WriteLine($"{department.DepartmentId}  {department.DepartmentName}");
                    }
                    while(true)
                    {
                        Console.Write("Enter the department Id: ");
                        departmentId = Convert.ToInt32(Console.ReadLine());

                        if (departmentList.Any(x => x.DepartmentId == departmentId))
                        {
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Enter correct department Id");
                        }
                    }
                    break;
                case 2:
                    Console.Write("Enter the new department name: ");
                    string departmentName=Console.ReadLine();
                    Department newDepartment = new Department()
                    {
                        DepartmentName = departmentName,
                    };
                    departmentId=DepartmentManager.Add(newDepartment);
                    break;       
            }
            List<Employee> newEmployee = new List<Employee>();
            newEmployee.Add(new Employee()
            {
                EmployeeName=employeeName,
                DepartmentId=departmentId,
            });
            EmployeeManager.BulkAdd(newEmployee);

        }
    }
}
