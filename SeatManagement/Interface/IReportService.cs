using SeatManagement.DTO;
using SeatManagement.Models;

namespace SeatManagement.Interface
{
    public interface IReportService
    {
		//public List<ViewFacilityDTO> GetFacilityList();
		public List<ViewAllocationDTO> GetSeatAllocatedView();
        public List<ViewAllocationDTO> GetSeatAllocatedView(int facilityId);
        
        public List<ViewAllocationDTO> GetCabinAllocatedView();
        
        public List<ViewAllocationDTO> GetCabinAllocatedView(int facilityId);
        //public List<ViewAllocationDTO> GetMeetingRoomView();

        
    }

}
