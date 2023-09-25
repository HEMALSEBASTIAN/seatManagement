using SeatManagement.Models;
using SeatManagementConsole.Implementation;
using SeatManagementConsole.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SeatManagementConsole.View
{
    public class AddEmployeeBulkConsole : IAddEmployeeBulk
    {
        public int AddType => 1;

        public List<Employee>? Add()
        {
            int choice;
            IEntityManager<Department> departmentManager = new EntityManager<Department>("api/Department");
            List<Employee> employeeList = new List<Employee>();
            do
            {
                Console.Write("Enter the employee name : ");
                string employeeName=Console.ReadLine();

                Console.WriteLine("Available department");
                var departmentList = departmentManager.Get();
                if(!departmentList.Any())
                {
                    Console.WriteLine("Press enter to continue");
                    Console.ReadLine();
                    return default;
                }
                foreach (var department in departmentList)
                {
                    Console.WriteLine($"{department.DepartmentId}  {department.DepartmentName}");
                }

                Console.Write("Enter the depaartment Id : ");
                int departmentId = Convert.ToInt32(Console.ReadLine());
                employeeList.Add(new Employee()
                {
                    EmployeeName = employeeName,
                    DepartmentId = departmentId
                });

                Console.WriteLine("Do you want to add more employee (0/1): ");
                choice=Convert.ToInt32(Console.ReadLine());

            } while (choice == 1);
            return employeeList;
        }
    }
}
