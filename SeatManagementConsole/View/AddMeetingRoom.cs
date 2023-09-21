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
            IEntityManager<MeetingRoom> meetManager = new EntityManager<MeetingRoom>("api/MeetingRoom");

            Console.WriteLine("Available Office Locations");
            var FacilityList = ViewFacilityManager.Get();
            foreach (var facility in FacilityList)
            {
                Console.WriteLine($"{facility.FacilityId}  {facility.FacilityName}  " +
                    $"{facility.FacilityFloor}  {facility.BuildingName}  {facility.CityName}");
            }
            Console.Write("Enter the facility ID: ");
            int FacilityId = Convert.ToInt32(Console.ReadLine());

            var MeetingRoomList = meetManager.Get();
            int PreviousMeetingRoomCount = MeetingRoomList.Where(x => x.FacilityId == FacilityId).Count();
            Console.WriteLine(PreviousMeetingRoomCount);

            Console.Write("Enter the number of meeting rooms : ");
            int AddtionalMeetingRoomCount = Convert.ToInt32(Console.ReadLine());
            List<MeetingRoom> NewMeetingRoomList = new List<MeetingRoom>();
            for (int i = 0; i < AddtionalMeetingRoomCount; i++)
            {
                Console.Write("Ente meeting room capacity for "+ string.Format("M{0:D3}", ++PreviousMeetingRoomCount) + ": ");
                int newMeetingRoomCapacity=Convert.ToInt32(Console.ReadLine());
                NewMeetingRoomList.Add(new MeetingRoom()
                {
                    MeetingRoomNo = string.Format("M{0:D3}", PreviousMeetingRoomCount),
                    TotalSeat = newMeetingRoomCapacity,
                    FacilityId = FacilityId,
                });
            }



            meetManager.BulkAdd(NewMeetingRoomList);
            Console.WriteLine("Press enter to continue");
            Console.ReadLine();
        }
    }
}
