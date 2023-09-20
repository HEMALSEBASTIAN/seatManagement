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
    public class DeallocateSeat : IDoWork
    {
        public int WorkType => 4;

        public void DoWork()
        {
            IEntityManager<ViewAllocationDTO> ViewAllocationManager = new EntityManager<ViewAllocationDTO>("api/Report?type=seat&&action=ViewAllocatedSeat");
            IEntityManager<AllocateDTO> SeatAllocationManager = new EntityManager<AllocateDTO>("api/seat?action=deallocate");

            Console.WriteLine("-----------------Seat Allocation System-----------------");
            Console.WriteLine("Showing All Allocated Seats");
            Console.WriteLine("Seat Id  Seat Name");
            var AllocatedSeatList = ViewAllocationManager.Get();
            foreach(var item in  AllocatedSeatList)
            {
                Console.WriteLine($"{item.SeatId}    {item.CityAbbrevation}-{item.BuildingAbbrevation}-{item.FacilityFloor}-{item.FacilityName}-{item.SeatNo}   {item.EmployeeName}");
            }
            while(true)
            {
                Console.Write("Enter the seat id you want to deallocate: ");
                int SeatId = Convert.ToInt32(Console.ReadLine());
                if (AllocatedSeatList.Any(x => x.SeatId == SeatId))
                {
                    var AllocatedSeat=AllocatedSeatList.Where(x=>x.SeatId==SeatId).FirstOrDefault();
                    Console.WriteLine($"{AllocatedSeat.EmployeeName} has been deallocated from seat no: {AllocatedSeat.SeatNo}");

                    AllocateDTO seatAllocateDTO = new AllocateDTO()
                    {
                        SeatId = SeatId,
                        EmployeeId =AllocatedSeat.EmployeeId,
                    };
                    SeatAllocationManager.Update(seatAllocateDTO);
                    break;
                }
            }
            Console.WriteLine("Press enter to continue");
            Console.ReadLine();
        }
    }
}
