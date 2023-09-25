using SeatManagement.Implementation;
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
    public class AddMeetingRoomAsset : IDoWork
    {
        public int WorkType => 12;

        public void DoWork()
        {
            Console.Clear();
            IEntityManager<LookUpAsset> AssetManager = new EntityManager<LookUpAsset>("api/Asset");
            Console.WriteLine("-------------Adding Asset-------------");
            List<LookUpAsset> AssetList = new List<LookUpAsset>();
            int choice = 0;
            do
            {
                Console.Write("Enter the asset name : ");
                string AssetName=Console.ReadLine();
                AssetList.Add(new LookUpAsset()
                {
                   AssetName = AssetName,
                });

                Console.Write("Do you want to add more assets (0/1) : ");
                choice=Convert.ToInt32(Console.ReadLine());
            } while (choice == 1);

            AssetManager.BulkAdd(AssetList);

            Console.WriteLine("Press enter to continue");
            Console.ReadLine();
        }
    }
}
