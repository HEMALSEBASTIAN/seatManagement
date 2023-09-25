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
    public class AddSeat: IDoWork
    {
        public int WorkType => 2;

        public void DoWork()
        {
            Console.Clear();
            IEntityManager<ViewFacilityDTO> ViewFacilityManager = new EntityManager<ViewFacilityDTO>("api/Facility");
            IEntityManager<SeatDTO> seatManager = new EntityManager<SeatDTO>("api/Seat");

            Console.WriteLine("Available Office Locations");
            var FacilityList = ViewFacilityManager.Get();
            foreach (var facility in FacilityList)
            {
                Console.WriteLine($"{facility.FacilityId}  {facility.FacilityName}  " +
                    $"{facility.FacilityFloor}  {facility.BuildingName}  {facility.CityName}");
            }
            Console.Write("Enter the facility ID: ");
            int FacilityId = Convert.ToInt32(Console.ReadLine());

            Console.Write("Enter the number of seats : ");
            int AddtionalSeatCount = Convert.ToInt32(Console.ReadLine());

            var seatDTO = new SeatDTO()
            {
                FacilityId = FacilityId,
                Capacity = AddtionalSeatCount
            };
            
            seatManager.Add(seatDTO);
            Console.WriteLine("Press enter to continue");
            Console.ReadLine();
        }
    }
}
