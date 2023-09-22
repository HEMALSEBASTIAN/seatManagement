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
    public class StartPage
    {
        private readonly IEnumerable<IDoWork> _doWorkStrategy;

        public StartPage(IEnumerable<IDoWork> doWorkStrategy)
        {
            _doWorkStrategy = doWorkStrategy;
        }

        public void DoWork()
        { 
            while (true)
            {
                Console.Clear();
                Console.WriteLine("-------------------Welcome to Seat Management System-------------------");
                Console.Write("Menu\n" +
                    "1.Add Facility\n" +
                    "2.Add Seat\n" +
                    "3.Allocate Seat\n" +
                    "4.Deallocate Seat\n" +
                    "5.Add Employee\n" +
                    "6.Report Generation\n" +
                    "7.Exit\n" +
                    "8.Add Employees from external sources\n" +
                    "9.Add department\n" +
                    "10.Add Cabin\n" +
                    "11.Add Meeting Room\n" +
                    "12.Add Asset\n" +
                    "13.Allocate cabin\n" +
                    "14.Deallocate cabin\n" +
                    "15.Allocate meeting room asset\n" +
                    "Enter your choice : ");
                int choice = Convert.ToInt32(Console.ReadLine());
                Console.Clear();
                var strategy = _doWorkStrategy.Where(x => x.WorkType == choice).FirstOrDefault();
                strategy?.DoWork();
            }
        }
    }
}
