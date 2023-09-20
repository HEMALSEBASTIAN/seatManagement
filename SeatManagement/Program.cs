using Microsoft.EntityFrameworkCore;
using SeatManagement;
using SeatManagement.Interface;
using SeatManagement.Implementation;
using SeatManagement.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IRepositary<LookUpAsset>, Repositary<LookUpAsset>>();
builder.Services.AddScoped<IRepositary<LookUpBuilding>, Repositary<LookUpBuilding>>();
builder.Services.AddScoped<IRepositary<LookUpCity>, Repositary<LookUpCity>>();
builder.Services.AddScoped<IRepositary<Department>, Repositary<Department>>();
builder.Services.AddScoped<IRepositary<Facility>, Repositary<Facility>>();
builder.Services.AddScoped<IRepositary<Employee>, Repositary<Employee>>();
builder.Services.AddScoped<IRepositary<Seat>, Repositary<Seat>>();
builder.Services.AddScoped<IRepositary<Cabin>, Repositary<Cabin>>();
builder.Services.AddScoped<IRepositary<MeetingRoom>, Repositary<MeetingRoom>>();
builder.Services.AddScoped<IRepositary<MeetingRoomAsset>, Repositary<MeetingRoomAsset>>();
builder.Services.AddScoped<IRepositary<VUnAllocatedSeat>, Repositary<VUnAllocatedSeat>>();
builder.Services.AddScoped<IRepositary<VFacility>, Repositary<VFacility>>();

builder.Services.AddScoped<IAssetService, AssetService>();
builder.Services.AddScoped<IBuildingService, BuildingService>();
builder.Services.AddScoped<ICityService, CityService>();
builder.Services.AddScoped<IDepartmentService, DepartmentService>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<ISeatService, SeatService>();
builder.Services.AddScoped<IFacilityService, FacilityService>();
builder.Services.AddScoped<IMeetingRoomService, MeetingRoomService>();
builder.Services.AddScoped<IMeetingRoomAssetService, MeetingRoomAssetService>();
builder.Services.AddScoped<ICabinService, CabinService>();
builder.Services.AddScoped<IReportService, ReportService>();

builder.Services.AddMemoryCache();
builder.Services.AddDbContext<SeatManagementContext>(options=>
options.UseSqlServer("name=ConnectionStrings:DefaultConnection"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
