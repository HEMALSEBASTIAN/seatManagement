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

            //IEntityManager<MeetingRoomAsset> MeetingRoomAssetManager1 = new EntityManager<MeetingRoomAsset>("api/MeetingRoomAsset");


            //var updateAllocation1 = new MeetingRoomAsset()
            //{
            //    AssetQuantity = 6,
            //    AssetId = 2,
            //    MeetingRoomId = 5,
            //    MeetingRoomAssetId = 1

            //};
            //MeetingRoomAssetManager1.Update(updateAllocation1);







            IEntityManager<ViewAllocationDTO> ViewMeetingRoomManager = new EntityManager<ViewAllocationDTO>("api/Report/GetMeetingRoomView");

            Console.WriteLine("Available Meeting room");
            var MeetingRoomList = ViewMeetingRoomManager.Get();
            foreach(var MeetingRoom in MeetingRoomList)
            {
                Console.WriteLine($"{MeetingRoom.SeatId}    {MeetingRoom.CityAbbrevation}-{MeetingRoom.BuildingAbbrevation}-{MeetingRoom.FacilityFloor}-{MeetingRoom.FacilityName}-{MeetingRoom.SeatNo}");
            }
            while (true)
            {
                Console.Write("Choose your meeting room by entering the meet room id: ");
                int MeetingRoomId = Convert.ToInt32(Console.ReadLine());
                if (MeetingRoomList.Any(e => e.SeatId == MeetingRoomId))
                {
                    Console.Clear();
                    IEntityManager<MeetingRoomAssetNameDTO> MeetingRoomAllocationManager = 
                        new EntityManager<MeetingRoomAssetNameDTO>("api/MeetingRoomAsset/MeetingRoomId?MeetingRoomId=" + MeetingRoomId);
                    
                    IEntityManager<LookUpAsset> AssetManager = new EntityManager<LookUpAsset>("api/Asset");


                    IEntityManager<MeetingRoomAsset> MeetingRoomAssetManager = new EntityManager<MeetingRoomAsset>("api/MeetingRoomAsset"); //for adding or updating

                    var AllocatedList = MeetingRoomAllocationManager.Get();
                    Console.WriteLine("Allocated Resources ");
                    foreach(var asset in AllocatedList)
                    {
                        Console.WriteLine($"{asset.MeetingRoomAssetId} {asset.AssetName}  {asset.AssetQuantity}");
                    }

                    Console.Write("Menu\n" +
                        "1.Add new resource\n" +
                        "2.Update existing resource\n" +
                        "Enter your choice : ");

                    int choice = Convert.ToInt32(Console.ReadLine());
                    if(choice == 1)
                    {
                        var AssetList = AssetManager.Get();
                        Console.WriteLine("Available resources");
                        foreach (var Asset in AssetList)
                        {
                            Console.WriteLine($"{Asset.AssetId}  {Asset.AssetName}");
                        }
                        while (true)
                        {
                            Console.Write("Enter the asset id you want to add: ");
                            int newAssetId = Convert.ToInt32(Console.ReadLine());

                            if (AssetList.Any(x => x.AssetId == newAssetId))
                            {
                                Console.Write("Enter the quantity to add : ");
                                int newAssetQuantity = Convert.ToInt32(Console.ReadLine());
                                var newAllocation = new MeetingRoomAsset()
                                {
                                    AssetQuantity = newAssetQuantity,
                                    AssetId = newAssetId,
                                    MeetingRoomId = MeetingRoomId
                                };
                                MeetingRoomAssetManager.Add(newAllocation);
                                break;
                            }
                            else
                                Console.WriteLine("Enter correct asset Id : ");
                        }
                    }
                    else if(choice == 2)
                    {
                        
                        while(true)
                        {
                            Console.Write("Enter the id of the asset you want to add : ");
                            int meetingRoomAssetId = Convert.ToInt32(Console.ReadLine());
                            if (AllocatedList.Any(x=>x.MeetingRoomAssetId==meetingRoomAssetId))
                            {
                                Console.Write("Enter the new quantity : ");
                                int newAssetQuantity = Convert.ToInt32(Console.ReadLine());
                                var updateAllocation = new MeetingRoomAsset()
                                {
                                    AssetQuantity = newAssetQuantity,
                                    AssetId = AllocatedList.Where(x=>x.MeetingRoomAssetId==meetingRoomAssetId).Select(x=>x.AssetId).FirstOrDefault(),
                                    MeetingRoomId = MeetingRoomId,
                                    MeetingRoomAssetId=meetingRoomAssetId

                                };
                                MeetingRoomAssetManager.Update(updateAllocation);
                                break;
                            }
                            else
                            {
                                Console.WriteLine("Enter correct id: ");
                            }
                        }

                    }





















                    break;
                }
                else
                {
                    Console.WriteLine("Enter correct meet room Id");
                }
            }



        }
    }
}
