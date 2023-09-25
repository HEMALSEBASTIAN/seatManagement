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
    public class DeallocateCabin : IDoWork
    {
        public int WorkType => 14;

        public void DoWork()
        {
            IEntityManager<ViewAllocationDTO> ViewAllocationManager = new EntityManager<ViewAllocationDTO>("api/cabin/report?type=allocated");
            IAllocationManager<Cabin> CabinAllocationManager = new AllocationManager<Cabin>("api/cabin");

            Console.WriteLine("-----------------Cabin Allocation System-----------------");
            Console.WriteLine("Showing All Allocated Cabin");
            Console.WriteLine("Cabin Id  Cabin Name");
            var AllocatedCabinList = ViewAllocationManager.Get();
            foreach (var item in AllocatedCabinList)
            {
                Console.WriteLine($"{item.SeatId}    {item.CityAbbrevation}-{item.BuildingAbbrevation}-{item.FacilityFloor}-{item.FacilityName}-{item.SeatNo}   {item.EmployeeName}");
            }
            Console.Write("Enter the cabin id you want to deallocate: ");
            int cabinId = Convert.ToInt32(Console.ReadLine());


            int response=CabinAllocationManager.Deallocate(cabinId);
            if (response == 0)
            {
                var AllocatedCabin = AllocatedCabinList.Where(x => x.SeatId == cabinId).FirstOrDefault();
                Console.WriteLine($"{AllocatedCabin.EmployeeName} has been deallocated from cabin no: {AllocatedCabin.SeatNo}");
                Console.WriteLine("Cabin deallocation successful");
            } 
            else
                Console.WriteLine("cabin deallocation unsuccessful");

            Console.WriteLine("Press enter to continue");
            Console.ReadLine();
        }
    }
}
