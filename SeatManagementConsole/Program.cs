using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using SeatManagement.Models;
using SeatManagement.DTO;
using SeatManagementConsole.Implementation;
using SeatManagementConsole.Interface;
using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;
using SeatManagementConsole.View;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;

public class Program
{
    public static void Main(string[] args)
    {

        IAPICall<Seat> seatManager = new APICall<Seat>("api/seat/pagination?pagenumber=1&&pageSize=10");
        List<Seat> seatList=seatManager.GetAll();





        var builder = new HostBuilder();
        builder.ConfigureServices(services =>
        {
            services.AddScoped<IDoWork, AddFacility>();
            services.AddScoped<IDoWork, AddSeat>();
            services.AddScoped<IDoWork, AllocateSeat>();
            services.AddScoped<IDoWork, DeallocateSeat>();
            services.AddScoped<IDoWork, AddEmployee>();
            services.AddScoped<IDoWork, ExitSeatManagement>();
            services.AddScoped<IDoWork, AddEmployeeBulk>();
            services.AddScoped<IDoWork, AddDepartment>();
            services.AddScoped<IDoWork, AddCabin>();
            services.AddScoped<IDoWork, AddMeetingRoom>();
            services.AddScoped<IDoWork, AddMeetingRoomAsset>();
            services.AddScoped<IDoWork, ReportGeneration>();
            services.AddScoped<IDoWork, AllocateCabin>();
            services.AddScoped<IDoWork, DeallocateCabin>();
            services.AddScoped<IDoWork, AllocateMeetingRoomAsset>();
            services.AddScoped<IAddEmployeeBulk, AddEmployeeBulkConsole>();
            services.AddScoped<StartPage>();
        });










        var serviceProvider = builder.Build().Services;
        var startPage = serviceProvider.GetRequiredService<StartPage>();
        startPage.DoWork();



        //IAPICall<Seat> seatManager = new APICall<Seat>("api/Seat");
        //var seat = seatManager.GetById(1);











        















        //IAPICall<LookUpCity> aPICall = new APICall<LookUpCity>("api/City");
        //var response = aPICall.GetAll();
        //foreach (var item in response)
        //{
        //    Console.WriteLine(item.CityName);
        //}


        //var obj = new LookUpCity()
        //{
        //    CityName = "Sample3",
        //    CityAbbrevation = "SM3"
        //};

        //***************************************************************************
        //SeatAllocateDTO obj = new SeatAllocateDTO()
        //{
        //    SeatId = 4,
        //    EmployeeId = 2,
        //};
        ////IAPICall<SeatAllocateDTO> seatAPI = new APICall<SeatAllocateDTO>("api/Seat");
        ////seatAPI.Update(obj);



        //IAllocationManager<SeatAllocateDTO> seatManager = new AllocationManager<SeatAllocateDTO>("api/Seat");
        //seatManager.Allocate(obj);

        //IEntityManager<Seat> SeatEntity = new EntityManager<Seat>("api/Seat");
        //foreach(var item in SeatEntity.Get())
        //{
        //    Console.WriteLine($"{item.SeatId}  {item.FacilityId}");
        //}
        //****************************************************************



        //IEntityManager<LookUpBuilding> entityManager = new EntityManager<LookUpBuilding>("api/Building");
        //var buildObj = new LookUpBuilding()
        //{
        //    BuildingName = "TRANSASIA",
        //    BuildingAbbrevation = "TRA"
        //};
        //int response = entityManager.Add(buildObj);
        //Console.WriteLine(response);

        //foreach(var item in entityManager.Get())
        //{
        //    Console.WriteLine($"{item.BuildingId}  {item.BuildingName}");
        //}








        //var r = aPICall.Adding(obj);
        //Console.WriteLine(r);

        //aPICall.Update(obj);

        //var response1 = aPICall.GetById(1);
        //Console.WriteLine(response1.CityName);

        //response = aPICall.GetById(1);
        //Console.WriteLine(response[0].CityName);


        //Console.WriteLine("dwsd");

    }
}