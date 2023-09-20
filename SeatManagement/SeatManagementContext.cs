using Microsoft.EntityFrameworkCore;
using SeatManagement.Models;

namespace SeatManagement
{
    public class SeatManagementContext: DbContext
    {
        public SeatManagementContext(DbContextOptions options) : base(options) { }

        public DbSet<LookUpBuilding> Building { get; set; }
        public DbSet<LookUpCity> City { get; set; }
        public DbSet<LookUpAsset> Asset { get; set; }
        public DbSet<Facility> Facilities { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee>  Employees { get; set; }
        public DbSet<Seat> Seats { get; set; }
        public DbSet<Cabin> Cabins { get; set; }
        public DbSet<MeetingRoom> MeetingRooms { get; set; }
        public DbSet<MeetingRoomAsset> MeetingRoomAssets { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder
                .Entity<VUnAllocatedSeat>()
                .ToView("UnAllocatedSeat")
                .HasKey(e => e.SeatId);

            base.OnModelCreating(modelBuilder);
            modelBuilder
                .Entity<VFacility>()
                .ToView("FacilityView")
                .HasKey(e => e.FacilityId);
        }
    }
}
