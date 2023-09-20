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

        public List<Employee> Add()
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
                foreach(var department in departmentList)
                {
                    Console.WriteLine($"{department.DepartmentId}  {department.DepartmentName}");
                }
                while(true)
                {
                    Console.Write("Enter the depaartment Id : ");
                    int departmentId = Convert.ToInt32(Console.ReadLine());
                    if (departmentList.Any(x => x.DepartmentId == departmentId))
                    {
                        employeeList.Add(new Employee()
                        {
                            EmployeeName = employeeName,
                            DepartmentId = departmentId
                        });
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Ente correct department Id");
                    }
                }

                Console.WriteLine("Do you want to add more employee (0/1): ");
                choice=Convert.ToInt32(Console.ReadLine());

            } while (choice == 1);
            return employeeList;
        }
    }
}
