namespace SeatManagement.Models
{
    public class VUnAllocatedSeat
    {
        public int SeatId { get; set; }
        public string CityAbbrevation { get; set; }
        public string BuildingAbbrevation { get; set; }
        public int FacilityFloor { get; set; }
        public string FacilityName { get; set; }
        public string SeatNo { get; set; }
    }
}
