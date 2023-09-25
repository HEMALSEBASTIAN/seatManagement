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
    public class AddEmployeeBulk : IDoWork
    {
        public int WorkType => 8;
        private readonly IEnumerable<IAddEmployeeBulk> _EmployeeBulk;

        public AddEmployeeBulk(IEnumerable<IAddEmployeeBulk> EmployeeBulk)
        {
            _EmployeeBulk = EmployeeBulk;
        }

        public void DoWork()
        {
            Console.Write("Menu\n" +
                "1.Enter new employees manually\n" +
                "2.Enter employee from external sources\n" +
                "Enter your choice : ");
            int choice=Convert.ToInt32(Console.ReadLine());
            Console.Clear();


            List<Employee> employeeList = new List<Employee>(); 
            var strategy = _EmployeeBulk.Where(x => x.AddType == choice).FirstOrDefault();
            employeeList=strategy?.Add();

            if(employeeList==null)
            {
                Console.WriteLine("No employee to add");
                return;
            }


            IEntityManager<Employee> employeeManager = new EntityManager<Employee>("api/Employee");
            employeeManager.BulkAdd(employeeList);

        }
    }
}
