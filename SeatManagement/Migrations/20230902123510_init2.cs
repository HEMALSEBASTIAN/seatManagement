using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SeatManagement.Migrations
{
    public partial class init2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"CREATE OR ALTER VIEW UnAllocatedSeat AS
                SELECT S.SeatId, C.CityAbbrevation, B.BuildingAbbrevation, F.FacilityFloor, F.FacilityName, S.SeatNo
                FROM Facilities F JOIN City C ON F.CityId=C.CityId
				                  JOIN Building B ON F.BuildingId=B.BuildingId
				                  JOIN Seats S ON F.FacilityId=S.FacilityId AND S.EmployeeId IS NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"DROP VIEW UnAllocatedSeat");
        }
    }
}
