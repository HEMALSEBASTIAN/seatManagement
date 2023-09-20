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
    public class AddDepartment : IDoWork
    {
        public int WorkType => 9;

        public void DoWork()
        {
            IEntityManager<Department> DepartmentManager = new EntityManager<Department>("api/Department");
            Console.Write("Enter the new department name: ");
            string departmentName = Console.ReadLine();
            Department newDepartment = new Department()
            {
                DepartmentName = departmentName,
            };
            int departmentId= DepartmentManager.Add(newDepartment);
        }
    }
}
