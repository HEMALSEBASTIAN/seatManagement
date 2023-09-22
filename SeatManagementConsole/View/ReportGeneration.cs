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
    public class ReportGeneration : IDoWork
    {
        public int WorkType => 6;

        public void DoWork()
        {
            Console.Clear();

            Console.Write("FIlter by\n" +
                "1.By location\n" +
                "2.Seats\n" +
                "3.Cabin\n" +
                "4.By floor\n" +
                "Enter your choice : ");
            int choice = Convert.ToInt32(Console.ReadLine());
            Console.Clear();


            IEntityManager<ViewAllocationDTO> ViewUnAllocationSeatManager = null;
            IEntityManager<ViewAllocationDTO> ViewAllocationSeatManager = null;
            IEntityManager<ViewAllocationDTO> ViewUnAllocationCabinManager = null;
            IEntityManager<ViewAllocationDTO> ViewAllocationCabinManager = null;

            if (choice == 1)
            {
                IEntityManager<ViewFacilityDTO> ViewFacilityManager = new EntityManager<ViewFacilityDTO>("api/Facility");


                Console.WriteLine("Available Office Locations");
                var FacilityList = ViewFacilityManager.Get();
                foreach (var facility in FacilityList)
                {
                    Console.WriteLine($"{facility.FacilityId}  {facility.FacilityName}  " +
                        $"{facility.FacilityFloor}  {facility.BuildingName}  {facility.CityName}");
                }
                Console.Write("Enter the facility name: ");
                string facilityName = Console.ReadLine();

                var facilityDTO = FacilityList
                    .Where(x => string.Equals(x.FacilityName, facilityName, StringComparison.OrdinalIgnoreCase))
                    .FirstOrDefault();
                try
                {
                    //ViewUnAllocationSeatManager = new EntityManager<ViewAllocationDTO>("api/Report/ViewUnAllocatedSeat?facilityId=" + facilityDTO.FacilityId);
                    //ViewAllocationSeatManager = new EntityManager<ViewAllocationDTO>("api/Report/ViewAllocatedSeat?facilityId=" + facilityDTO.FacilityId);
                    //ViewUnAllocationSeatManager = new EntityManager<ViewAllocationDTO>("api/Report/seat?action=qViewUnAllocatedSeat&&facilityId=" + facilityDTO.FacilityId);
                    //ViewAllocationSeatManager = new EntityManager<ViewAllocationDTO>("api/Report/seat?action=ViewAllocatedSeat&&facilityId=" + facilityDTO.FacilityId);
                    //ViewAllocationSeatManager = new EntityManager<ViewAllocationDTO>("api/Report?type=seat&&action=ViewAllocatedSeat&&facilityId=" + facilityDTO.FacilityId);

                    ViewUnAllocationSeatManager = new EntityManager<ViewAllocationDTO>("api/seat/report?facilityId=" + facilityDTO.FacilityId);
                    Console.WriteLine($"\nUnallocated seats in {facilityDTO.FacilityName}");
                    foreach (var seat in ViewUnAllocationSeatManager.Get())
                    {
                        Console.WriteLine($"{seat.CityAbbrevation}-{seat.BuildingAbbrevation}-{seat.FacilityFloor}-{seat.FacilityName}-{seat.SeatNo}");
                    }

                    //Console.WriteLine($"\nAllocated seats in {facilityDTO.FacilityName}");
                    //foreach (var seat in ViewAllocationSeatManager.Get())
                    //{
                    //    Console.WriteLine($"{seat.CityAbbrevation}-{seat.BuildingAbbrevation}-{seat.FacilityFloor}-{seat.FacilityName}-{seat.SeatNo}");
                    //}
                    //ViewUnAllocationCabinManager = new EntityManager<ViewAllocationDTO>("api/Report/cabin?action=ViewUnAllocatedCabin&&facilityId=" + facilityDTO.FacilityId);
                    //ViewAllocationCabinManager = new EntityManager<ViewAllocationDTO>("api/Report/cabin?action=ViewAllocatedCabin&&facilityId=" + facilityDTO.FacilityId);
                    //ViewAllocationCabinManager = new EntityManager<ViewAllocationDTO>("api/Report?type=cabin&&action=ViewAllocatedCabin&&facilityId=" + facilityDTO.FacilityId);

                    ViewUnAllocationCabinManager = new EntityManager<ViewAllocationDTO>("api/cabin/report?facilityId=" + facilityDTO.FacilityId);
                    Console.WriteLine($"\nUnallocated cabin in {facilityDTO.FacilityName}");
                    foreach (var seat in ViewUnAllocationCabinManager.Get())
                    {
                        Console.WriteLine($"{seat.CityAbbrevation}-{seat.BuildingAbbrevation}-{seat.FacilityFloor}-{seat.FacilityName}-{seat.SeatNo}");
                    }

                    //Console.WriteLine($"\nAllocated cabin in {facilityDTO.FacilityName}");
                    //foreach (var seat in ViewAllocationCabinManager.Get())
                    //{
                    //    Console.WriteLine($"{seat.CityAbbrevation}-{seat.BuildingAbbrevation}-{seat.FacilityFloor}-{seat.FacilityName}-{seat.SeatNo}");
                    //}
                }
                catch(NullReferenceException e) //if enetered facility name is wrong, this exception catches it.
                {
                    Console.WriteLine($"The entered facility does not exist : {e.Message}");
                }
                

            }
            else if (choice == 2)
            {
                //ViewUnAllocationSeatManager = new EntityManager<ViewAllocationDTO>("api/Report/ViewUnAllocatedSeat");
                //ViewAllocationSeatManager = new EntityManager<ViewAllocationDTO>("api/Report/ViewAllocatedSeat");
                //ViewAllocationSeatManager = new EntityManager<ViewAllocationDTO>("api/Report?type=seat&&action=ViewAllocatedSeat");

                ViewUnAllocationSeatManager = new EntityManager<ViewAllocationDTO>("api/seat/report");
                Console.WriteLine($"Unallocated seats in all facilities");
                foreach (var seat in ViewUnAllocationSeatManager.Get())
                {
                    Console.WriteLine($"{seat.CityAbbrevation}-{seat.BuildingAbbrevation}-{seat.FacilityFloor}-{seat.FacilityName}-{seat.SeatNo}");
                }

                //Console.WriteLine($"Allocated seats in all facilities");
                //foreach (var seat in ViewAllocationSeatManager.Get())
                //{
                //    Console.WriteLine($"{seat.CityAbbrevation}-{seat.BuildingAbbrevation}-{seat.FacilityFloor}-{seat.FacilityName}-{seat.SeatNo}");
                //}
            }
            else if (choice == 3)
            {
                //ViewAllocationCabinManager = new EntityManager<ViewAllocationDTO>("api/Report?type=cabin&&action=ViewAllocatedCabin");

                ViewUnAllocationCabinManager = new EntityManager<ViewAllocationDTO>("api/cabin/report");
                Console.WriteLine($"Unallocated cabin in all facilities");
                foreach (var seat in ViewUnAllocationCabinManager.Get())
                {
                    Console.WriteLine($"{seat.CityAbbrevation}-{seat.BuildingAbbrevation}-{seat.FacilityFloor}-{seat.FacilityName}-{seat.SeatNo}");
                }

                //Console.WriteLine($"Allocated cabin in all facilities");
                //foreach (var seat in ViewAllocationCabinManager.Get())
                //{
                //    Console.WriteLine($"{seat.CityAbbrevation}-{seat.BuildingAbbrevation}-{seat.FacilityFloor}-{seat.FacilityName}-{seat.SeatNo}");
                //}
            }
            else if(choice == 4)
            {
                Console.Write("Enter the floor number: ");
                string floorNo = Console.ReadLine();

                ViewUnAllocationSeatManager = new EntityManager<ViewAllocationDTO>("api/seat/report?floorNo=" + floorNo);
                Console.WriteLine($"Unallocated seats in all facilities with floor number {floorNo}");
                foreach (var seat in ViewUnAllocationSeatManager.Get())
                {
                    Console.WriteLine($"{seat.CityAbbrevation}-{seat.BuildingAbbrevation}-{seat.FacilityFloor}-{seat.FacilityName}-{seat.SeatNo}");
                }

                ViewUnAllocationCabinManager = new EntityManager<ViewAllocationDTO>("api/cabin/report?floorNo=" + floorNo);
                Console.WriteLine($"\nUnallocated cabin in all facilities with floor number {floorNo}");
                foreach (var seat in ViewUnAllocationCabinManager.Get())
                {
                    Console.WriteLine($"{seat.CityAbbrevation}-{seat.BuildingAbbrevation}-{seat.FacilityFloor}-{seat.FacilityName}-{seat.SeatNo}");
                }

            }
            Console.WriteLine("Press enter to continue");
            Console.ReadLine();
        }
    }
}
