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
    public class AddMeetingRoom : IDoWork
    {
        public int WorkType => 11;

        public void DoWork()
        {
            Console.Clear();
            IEntityManager<ViewFacilityDTO> ViewFacilityManager = new EntityManager<ViewFacilityDTO>("api/Facility");
            IEntityManager<MeetingRoomDTO> meetManager = new EntityManager<MeetingRoomDTO>("api/MeetingRoom");

            Console.WriteLine("Available Office Locations");
            var FacilityList = ViewFacilityManager.Get();
            foreach (var facility in FacilityList)
            {
                Console.WriteLine($"{facility.FacilityId}  {facility.FacilityName}  " +
                    $"{facility.FacilityFloor}  {facility.BuildingName}  {facility.CityName}");
            }
            Console.Write("Enter the facility ID: ");
            int FacilityId = Convert.ToInt32(Console.ReadLine());

            

            Console.Write("Enter the number of meeting rooms : ");
            int AddtionalMeetingRoomCount = Convert.ToInt32(Console.ReadLine());

            int[] seatCount = new int[AddtionalMeetingRoomCount];
            for (int i = 0; i < AddtionalMeetingRoomCount; i++)
            {
                Console.Write("Enter meeting room capacity for "+(i+1)+": ");
                seatCount[i]=(Convert.ToInt32(Console.ReadLine()));
            }

            MeetingRoomDTO NewMeetingRooms = new MeetingRoomDTO()
            {
                FacilityId= FacilityId,
                MeetingRoomCount=AddtionalMeetingRoomCount,
                TotalSeat=seatCount
            };



            meetManager.Add(NewMeetingRooms);
            Console.WriteLine("Press enter to continue");
            Console.ReadLine();
        }
    }
}
