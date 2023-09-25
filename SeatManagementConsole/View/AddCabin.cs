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
    public class AddCabin : IDoWork
    {
        public int WorkType => 10;

        public void DoWork()
        {
            Console.Clear();
            IEntityManager<ViewFacilityDTO> ViewFacilityManager = new EntityManager<ViewFacilityDTO>("api/Facility");
            IEntityManager<CabinDTO> cabinManager = new EntityManager<CabinDTO>("api/Cabin");

            Console.WriteLine("Available Office Locations");
            var FacilityList = ViewFacilityManager.Get();
            foreach (var facility in FacilityList)
            {
                Console.WriteLine($"{facility.FacilityId}  {facility.FacilityName}  " +
                    $"{facility.FacilityFloor}  {facility.BuildingName}  {facility.CityName}");
            }
            Console.Write("Enter the facility ID: ");
            int FacilityId = Convert.ToInt32(Console.ReadLine());

            Console.Write("Enter the number of cabins : ");
            int AddtionalCabinCount = Convert.ToInt32(Console.ReadLine());

            var cabinDTO = new CabinDTO()
            {
                FacilityId = FacilityId,
                Capacity = AddtionalCabinCount
            };

            cabinManager.Add(cabinDTO);
            Console.WriteLine("Press enter to continue");
            Console.ReadLine();
        }
    }
}
