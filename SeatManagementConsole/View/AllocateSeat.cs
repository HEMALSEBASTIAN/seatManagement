﻿using Microsoft.EntityFrameworkCore.Metadata;
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
    public class AllocateSeat: IDoWork
    {
        public int WorkType => 3;

        public void DoWork()
        {
            Console.WriteLine("-----------------Seat Allocation System-----------------");
            IEntityManager<ViewAllocationDTO> ViewAllocationManager = new EntityManager<ViewAllocationDTO>("api/Report?type=seat&&action=ViewUnAllocatedSeat");
            IEntityManager <Employee> EmployeeManager = new EntityManager<Employee>("api/Employee");
            IEntityManager<AllocateDTO> SeatManager = new EntityManager<AllocateDTO>("api/Seat?action=allocate");

            var UnAllocatedEmployeeList = EmployeeManager.Get().Where(e => e.IsAllocated == false);
            Console.WriteLine("Showing Employees with no seats");
            foreach(var item in UnAllocatedEmployeeList)
            {
                Console.WriteLine($"{item.EmployeeId}  {item.EmployeeName}");
            }
            while(true)
            {
                Console.Write("Enter the employee ID of the employee whom you want allocate seat: ");
                int EmployeeId = Convert.ToInt32(Console.ReadLine());
                if (UnAllocatedEmployeeList.Any(e => e.EmployeeId == EmployeeId))
                {
                    var UnAllocatedSeatList = ViewAllocationManager.Get();
                    Console.WriteLine("Showing available seats");
                    Console.WriteLine("Seat Id  Seat Name");
                    foreach(var item in UnAllocatedSeatList)
                    {
                        Console.WriteLine($"{item.SeatId}    {item.CityAbbrevation}-{item.BuildingAbbrevation}-{item.FacilityFloor}-{item.FacilityName}-{item.SeatNo}");
                    }
                    while(true)
                    {
                        Console.Write("Choose your seat by entering the seat id: ");
                        int SeatId = Convert.ToInt32(Console.ReadLine());
                        if (UnAllocatedSeatList.Any(e => e.SeatId == SeatId))
                        {
                            AllocateDTO AllocatingSeat = new AllocateDTO() 
                            { 
                                SeatId= SeatId,
                                EmployeeId= EmployeeId
                            };
                            SeatManager.Update(AllocatingSeat);
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Enter correct seat Id");
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
