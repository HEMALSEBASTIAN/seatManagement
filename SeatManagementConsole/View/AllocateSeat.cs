using Microsoft.EntityFrameworkCore.Metadata;
using SeatManagement.DTO;
using SeatManagement.Models;
using SeatManagementConsole.Implementation;
using SeatManagementConsole.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace SeatManagementConsole.View
{
    public class AllocateSeat: IDoWork
    {
        public int WorkType => 3;

        public void DoWork()
        {
            Console.WriteLine("-----------------Seat Allocation System-----------------");
            IEntityManager<ViewAllocationDTO> ViewAllocationManager = new EntityManager<ViewAllocationDTO>("api/seat/report");
            IEntityManager <Employee> EmployeeManager = new EntityManager<Employee>("api/Employee");
            IAllocationManager<Seat> SeatManager = new AllocationManager<Seat>("api/Seat");

            var UnAllocatedEmployeeList = EmployeeManager.Get().Where(e => e.IsAllocated == false);
            Console.WriteLine("Showing Employees with no seats");
            foreach(var item in UnAllocatedEmployeeList)
            {
                Console.WriteLine($"{item.EmployeeId}  {item.EmployeeName}");
            }
            Console.Write("Enter the employee ID of the employee whom you want allocate seat: ");
            int EmployeeId = Convert.ToInt32(Console.ReadLine());

            var UnAllocatedSeatList = ViewAllocationManager.Get();
            Console.WriteLine("Showing available seats");
            Console.WriteLine("Seat Id  Seat Name");
            foreach(var item in UnAllocatedSeatList)
            {
                Console.WriteLine($"{item.SeatId}    {item.CityAbbrevation}-{item.BuildingAbbrevation}-{item.FacilityFloor}-{item.FacilityName}-{item.SeatNo}");
            }
            Console.Write("Choose your seat by entering the seat id: ");
            int SeatId = Convert.ToInt32(Console.ReadLine());

            int response=SeatManager.Allocate(SeatId, EmployeeId);
            if (response == 0)
                Console.WriteLine("Seat allocation successfull");
            else
                Console.WriteLine("Seat allocation unsuccessfull");
                            
                 
            Console.WriteLine("Press enter to continue");
            Console.ReadLine();
        }
    }
}
