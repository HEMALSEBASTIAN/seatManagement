namespace SeatManagement.CustomException
{
    public class EmployeeAlreadyAllocatedException: Exception
    {
        public EmployeeAlreadyAllocatedException(string message) :base(message) { }
    }
}
