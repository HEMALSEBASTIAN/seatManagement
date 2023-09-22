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
    public class AddFacility: IDoWork
    {
        public int WorkType => 1;

        public void DoWork()
        {
            IEntityManager<LookUpCity> CityManager = new EntityManager<LookUpCity>("api/City");
            IEntityManager<LookUpBuilding> BuildingManager = new EntityManager<LookUpBuilding>("api/Building");
            IEntityManager<Facility> FacilityManager = new EntityManager<Facility>("api/Facility");
            Console.Clear();
            Console.Write("Menu\n" +
                "1.Add to existing city\n" +
                "2.Add to new city\n" +
                "Enter your choice : ");
            int choice = Convert.ToInt32(Console.ReadLine());
            int CityId = 0;
            int BuildingId = 0;
            if (choice == 1)
            {
                var CityList = CityManager.Get();
                if(CityList.Count == 0) 
                {
                    Console.WriteLine("Please add city first\nPress Enter to continue");
                    Console.ReadLine();
                    return;
                }
                Console.WriteLine("City Id   City Name");
                foreach (var city in CityList)
                {
                    Console.WriteLine($"{city.CityId}      {city.CityName}");
                }
                Console.Write("Enter the city id for the city : ");
                CityId = Convert.ToInt32(Console.ReadLine());
            }
            else if (choice == 2)
            {
                Console.Write("Enter the City Name : ");
                string CityName = Console.ReadLine();
                Console.Write("enter the City abbrevation : ");
                string CityAbbrevation = Console.ReadLine();
                var NewCity = new LookUpCity()
                {
                    CityName = CityName,
                    CityAbbrevation = CityAbbrevation,
                };
                CityId = CityManager.Add(NewCity);
            }
            Console.WriteLine("Press Enter to continue");
            Console.ReadLine();
            Console.Clear();
            Console.Write("Menu\n" +
                "1.Add to existing building\n" +
                "2.Add to new building\n" +
                "Enter your choice : ");
            choice = Convert.ToInt32(Console.ReadLine());
            if (choice == 1)
            {
                var BuildingList = BuildingManager.Get();
                if (BuildingList.Count == 0)
                {
                    Console.WriteLine("Please add builidng first\nPress Enter to continue");
                    Console.ReadLine();
                    return;
                }
                Console.WriteLine("Builing Id   Building Name");
                foreach (var building in BuildingList)
                {
                    Console.WriteLine($"{building.BuildingId}      {building.BuildingName}");
                }
                Console.Write("Enter the building id for the building : ");
                BuildingId = Convert.ToInt32(Console.ReadLine());
            }
            else if (choice == 2)
            {
                Console.Write("Enter the Building Name : ");
                string BuildingName = Console.ReadLine();
                Console.Write("enter the Building abbrevation : ");
                string BuildingAbbrevation = Console.ReadLine();
                var NewBuilding = new LookUpBuilding()
                {
                    BuildingName = BuildingName,
                    BuildingAbbrevation = BuildingAbbrevation,
                };
                BuildingId = BuildingManager.Add(NewBuilding);
            }

            Console.Write("Enter the facility name: ");
            var FacilityName = Console.ReadLine();
            Console.Write("Enter the facility floor: ");
            var FacilityFloor = Convert.ToInt16(Console.ReadLine());
            var NewFacility = new Facility()
            {
                FacilityName = FacilityName,
                FacilityFloor = FacilityFloor,
                CityId = CityId,
                BuildingId = BuildingId
            };
            int FacilityId = FacilityManager.Add(NewFacility);
            Console.WriteLine("Press Enter to continue");
            Console.ReadLine();
        } 
    }
}
