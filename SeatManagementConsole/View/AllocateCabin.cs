using SeatManagement.DTO;
using SeatManagement.Models;
using SeatManagementConsole.Implementation;
using SeatManagementConsole.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace SeatManagementConsole.View
{
    internal class AllocateCabin : IDoWork
    {
        public int WorkType => 13;

        public void DoWork()
        {
            Console.WriteLine("-----------------Cabin Allocation System-----------------");
            IEntityManager<ViewAllocationDTO> ViewAllocationManager = new EntityManager<ViewAllocationDTO>("api/Report?type=cabin&&action=ViewUnAllocatedCabin");
            IEntityManager <Employee> EmployeeManager = new EntityManager<Employee>("api/Employee");
            IEntityManager<AllocateDTO> CabinManager = new EntityManager<AllocateDTO>("api/cabin?action=allocate");

            var UnAllocatedEmployeeList = EmployeeManager.Get().Where(e => e.IsAllocated == false);
            Console.WriteLine("Showing Unallocated employees");
            foreach (var item in UnAllocatedEmployeeList)
            {
                Console.WriteLine($"{item.EmployeeId}  {item.EmployeeName}");
            }
            while (true)
            {
                AllocateDTO AllocatingSeat1 = new AllocateDTO()
                {
                    SeatId = 5,
                    EmployeeId = 100
                };
                CabinManager.Update(AllocatingSeat1);

                Console.Write("Enter the employee ID of the employee whom you want allocate cabin: ");
                int EmployeeId = Convert.ToInt32(Console.ReadLine());
                if (UnAllocatedEmployeeList.Any(e => e.EmployeeId == EmployeeId))
                {
                    var UnAllocatedCabinList = ViewAllocationManager.Get();
                    Console.WriteLine("Showing available seats");
                    Console.WriteLine("Seat Id  Seat Name");
                    foreach (var item in UnAllocatedCabinList)
                    {
                        Console.WriteLine($"{item.SeatId}    {item.CityAbbrevation}-{item.BuildingAbbrevation}-{item.FacilityFloor}-{item.FacilityName}-{item.SeatNo}");
                    }
                    while (true)
                    {
                        Console.Write("Choose your cabin by entering the cabin id: ");
                        int CabinId = Convert.ToInt32(Console.ReadLine());
                        if (UnAllocatedCabinList.Any(e => e.SeatId == CabinId))
                        {
                            AllocateDTO AllocatingSeat = new AllocateDTO()
                            {
                                SeatId = CabinId,
                                EmployeeId = EmployeeId
                            };
                            CabinManager.Update(AllocatingSeat);
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Enter correct cabin Id");
                        }
                    }
                    break;
                }
                else
                {
                    Console.WriteLine("Enter correct employee Id");
                }
            }
            Console.WriteLine("Press enter to continue");
            Console.ReadLine();
        }
    }
}
