using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
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
    public class AllocateMeetingRoomAsset : IDoWork
    {
        public int WorkType => 15;

        public void DoWork()
        {
            Console.Clear();


            IEntityManager<ViewAllocationDTO> ViewMeetingRoomManager = new EntityManager<ViewAllocationDTO>("api/MeetingRoom");

            Console.WriteLine("Available Meeting room");
            var MeetingRoomList = ViewMeetingRoomManager.Get();
            foreach(var MeetingRoom in MeetingRoomList)
            {
                Console.WriteLine($"{MeetingRoom.SeatId}    {MeetingRoom.CityAbbrevation}-{MeetingRoom.BuildingAbbrevation}-{MeetingRoom.FacilityFloor}-{MeetingRoom.FacilityName}-{MeetingRoom.SeatNo}");
            }
            Console.Write("Choose your meeting room by entering the meet room id: ");
            int MeetingRoomId = Convert.ToInt32(Console.ReadLine());
            
            Console.Clear();
            IEntityManager<LookUpAsset> AssetManager = new EntityManager<LookUpAsset>("api/Asset");
            var AssetList = AssetManager.Get();
            if (AssetList.Count() == 0)
            {
                Console.WriteLine("Press enter to continue");
                Console.ReadLine();
                return;
            }
            Console.WriteLine("Available resources");
            foreach (var Asset in AssetList)
            {
                Console.WriteLine($"{Asset.AssetId}  {Asset.AssetName}");
            }
            Console.Write("Enter the asset id you want to add: ");
            int newAssetId = Convert.ToInt32(Console.ReadLine());
            Console.Write("Enter the quantity to add : ");

            int newAssetQuantity = Convert.ToInt32(Console.ReadLine());
            var newAssetAllocation = new MeetingRoomAssetDTO()
            {
                AssetQuantity = newAssetQuantity,
                AssetId = newAssetId,
            };

            IEntityManager<MeetingRoomAssetDTO> MeetingRoomManager = new EntityManager<MeetingRoomAssetDTO>("api/MeetingRoom/"+ MeetingRoomId);
            MeetingRoomManager.Add(newAssetAllocation);

            Console.WriteLine("Press enter to continue");
            Console.ReadLine();
            


        }
    }
}
