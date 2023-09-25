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
    internal class AllocateCabin : IDoWork
    {
        public int WorkType => 13;

        public void DoWork()
        {
            Console.WriteLine("-----------------Cabin Allocation System-----------------");
            IEntityManager<ViewAllocationDTO> ViewAllocationManager = new EntityManager<ViewAllocationDTO>("api/cabin/report");
            IEntityManager <Employee> EmployeeManager = new EntityManager<Employee>("api/Employee");
            IAllocationManager<Cabin> CabinManager = new AllocationManager<Cabin>("api/cabin");

            var UnAllocatedEmployeeList = EmployeeManager.Get().Where(e => e.IsAllocated == false);
            Console.WriteLine("Showing Unallocated employees");
            foreach (var item in UnAllocatedEmployeeList)
            {
                Console.WriteLine($"{item.EmployeeId}  {item.EmployeeName}");
            }
            Console.Write("Enter the employee ID of the employee whom you want allocate cabin: ");
            int EmployeeId = Convert.ToInt32(Console.ReadLine());


            var UnAllocatedCabinList = ViewAllocationManager.Get();
            Console.WriteLine("Showing available seats");
            Console.WriteLine("Seat Id  Seat Name");
            foreach (var item in UnAllocatedCabinList)
            {
                Console.WriteLine($"{item.SeatId}    {item.CityAbbrevation}-{item.BuildingAbbrevation}-{item.FacilityFloor}-{item.FacilityName}-{item.SeatNo}");
            }
            Console.Write("Choose your cabin by entering the cabin id: ");
            int CabinId = Convert.ToInt32(Console.ReadLine());
            
            int response=CabinManager.Allocate(CabinId, EmployeeId);
            if (response == 0)
                Console.WriteLine("Cabin allocation successful");
            else
                Console.WriteLine("Cabin allocation unsuccessful");
            Console.WriteLine("Press enter to continue");
            Console.ReadLine();
        }
    }
}
