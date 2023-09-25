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
builder.Services.AddSingleton<IRepository<LookUpAsset>, Repository<LookUpAsset>>();
builder.Services.AddSingleton<IRepository<LookUpBuilding>, Repository<LookUpBuilding>>();
builder.Services.AddSingleton<IRepository<LookUpCity>, Repository<LookUpCity>>();
builder.Services.AddSingleton<IRepository<Department>, Repository<Department>>();
builder.Services.AddSingleton<IRepository<Facility>, Repository<Facility>>();
builder.Services.AddSingleton<IRepository<Employee>, Repository<Employee>>();
builder.Services.AddSingleton<IRepository<Seat>, Repository<Seat>>();
builder.Services.AddSingleton<IRepository<Cabin>, Repository<Cabin>>();
builder.Services.AddSingleton<IRepository<MeetingRoom>, Repository<MeetingRoom>>();
builder.Services.AddSingleton<IRepository<MeetingRoomAsset>, Repository<MeetingRoomAsset>>();

//builder.Services.AddScoped<IRepository<VUnAllocatedSeat>, Repository<VUnAllocatedSeat>>();
//builder.Services.AddScoped<IRepository<VFacility>, Repository<VFacility>>();

builder.Services.AddSingleton<IAssetService, AssetService>();
builder.Services.AddSingleton<IBuildingService, BuildingService>();
builder.Services.AddSingleton<ICityService, CityService>();
builder.Services.AddSingleton<IDepartmentService, DepartmentService>();
builder.Services.AddSingleton<IEmployeeService, EmployeeService>();
builder.Services.AddSingleton<ISeatService, SeatService>();
builder.Services.AddSingleton<IFacilityService, FacilityService>();
builder.Services.AddSingleton<IMeetingRoomService, MeetingRoomService>();
builder.Services.AddSingleton<IMeetingRoomAssetService, MeetingRoomAssetService>();
builder.Services.AddSingleton<ICabinService, CabinService>();
//builder.Services.AddScoped<IReportService, ReportService>();

builder.Services.AddMemoryCache();
builder.Services.AddDbContext<SeatManagementContext>(options=>
options.UseSqlServer("name=ConnectionStrings:DefaultConnection"), ServiceLifetime.Singleton);

builder.Services.AddAuthentication("MyCookie").AddCookie("MyCookie", options =>
{
    options.Cookie.Name = "MyCookie";
    options.ExpireTimeSpan = TimeSpan.FromSeconds(300);
    options.Events.OnRedirectToLogin = context =>
    {
        context.Response.StatusCode = 401;
        return Task.CompletedTask;
    };
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("User", policy =>
    policy.RequireRole("User"));
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseAuthentication();
app.UseAuthorization();

app.UseExceptionHandler("/exception");

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
