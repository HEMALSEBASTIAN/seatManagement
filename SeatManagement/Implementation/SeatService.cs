using SeatManagement.CustomException;
using SeatManagement.DTO;
using SeatManagement.Interface;
using SeatManagement.Models;

namespace SeatManagement.Implementation
{
    public class SeatService : ISeatService
    {
        private readonly IRepositary<Seat> _seatRepositary;
        private readonly IRepositary<Employee> _employeeRepositary;

        public SeatService(IRepositary<Seat> seatRepositary, IRepositary<Employee> employeeRepositary)
        {
            _seatRepositary = seatRepositary;
            _employeeRepositary = employeeRepositary;
        }
        public List<Seat> Get()
        {
            var item = _seatRepositary.GetAll();
            
            return item
                .Skip(10)
                .Take(10)
                .ToList();
        }
        public void AddSeat(SeatDTO seatDTO)
        {
            var seat = new Seat()
            {
                SeatNo = seatDTO.SeatNo,
                FacilityId = seatDTO.FacilityId,
                EmployeeId=null
            };
            _seatRepositary.Add(seat);
        }

        public void AddSeat(List<SeatDTO> seatDTOList)
        {
            List<Seat> seatList = new List<Seat>();
            foreach (var seatDTO in seatDTOList)
            {
                seatList.Add(new Seat()
                {
                    SeatNo = seatDTO.SeatNo,
                    FacilityId = seatDTO.FacilityId,
                    EmployeeId = null
                });
            }
            _seatRepositary.Add(seatList);
        }

        public Seat AllocateSeat(AllocateDTO seat)
        {
            var item = _seatRepositary.GetById(seat.SeatId);
            var employee = _employeeRepositary.GetById((int)seat.EmployeeId);

            if (item == null)  
                throw new NoDataException("Seat does not exist");
            else if(employee == null)
                    throw new NoDataException("Employee does not exist");
            else if (employee.IsAllocated == true)
                throw new EmployeeAlreadyAllocatedException("Employee already allocated");
            else if(item.EmployeeId!=null)
                throw new AllocationException("Seat already allocated");

            item.EmployeeId = seat.EmployeeId;
            employee.IsAllocated = true;
            _employeeRepositary.Update();
            _seatRepositary.Update();
            return item;
        }

        public Seat DeAllocateSeat(AllocateDTO seat)
        {
            var item = _seatRepositary.GetById(seat.SeatId);
            var employee = _employeeRepositary.GetById((int)seat.EmployeeId);

            if (item == null)
                throw new NoDataException("Seat does not exist");
            else if (employee == null)
                throw new NoDataException("Employee does not exist");
            else if (employee.IsAllocated == false)
                throw new EmployeeAlreadyAllocatedException("Employee is not yet allocated!");
            else if (item.EmployeeId == null)
                throw new AllocationException("Seat is not yet allocated!"); 


            item.EmployeeId = null;
            employee.IsAllocated = false;
            _employeeRepositary.Update();
            _seatRepositary.Update();
            return item;
        }

        public Seat GetById(int id)
        {
            return _seatRepositary.GetById(id);
        }

        public List<Seat> Get(int pageNumber, int pageSize)
        {
            return _seatRepositary.GetAll()
                .Skip((pageNumber-1)*pageSize)
                .Take(pageSize)
                .ToList();
        }
    }
}
