namespace SeatManagement.DTO
{
    public class ViewAllocationDTO //View displaying Seat with facility details
    {
        public int SeatId { get; set; }
        public string SeatNo { get; set; }
        public string CityAbbrevation { get; set; }
        public string BuildingAbbrevation { get; set; }
        public int FacilityFloor { get; set; }
        public string FacilityName { get; set; }
        public string EmployeeName { get; set; }
        public int EmployeeId { get; set; }
        public int TotalSeat { get; set; }
    }
}
