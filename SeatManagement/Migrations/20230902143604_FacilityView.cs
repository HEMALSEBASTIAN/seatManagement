using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SeatManagement.Migrations
{
    public partial class FacilityView : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"CREATE OR ALTER VIEW FacilityView AS
                    SELECT F.FacilityId, C.CityAbbrevation, B.BuildingAbbrevation, F.FacilityFloor, F.FacilityName
                    FROM Facilities F JOIN City C ON F.CityId=C.CityId
				                      JOIN Building B ON F.BuildingId=B.BuildingId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"DROP VIEW FacilityView");
        }
    }
}
