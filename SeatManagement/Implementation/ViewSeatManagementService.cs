using Microsoft.EntityFrameworkCore;
using SeatManagement.DTO;
using SeatManagement.Interface;
using SeatManagement.Models;

namespace SeatManagement.Implementation
{
    public class ViewSeatManagementService : IViewSeatManagementService
    {
        private readonly IRepositary<Seat> _repositary;

        public ViewSeatManagementService(IRepositary<Seat> repositary)
        {
            _repositary = repositary;
        }

        public List<ViewAllocationDTO> ViewDeallocate()
        {
            throw new NotImplementedException();
        }
    }
}
