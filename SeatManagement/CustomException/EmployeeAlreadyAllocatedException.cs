namespace SeatManagement.CustomException
{
    public class EmployeeAlreadyAllocatedException: Exception
    {
        public EmployeeAlreadyAllocatedException()
        {
        }

        public EmployeeAlreadyAllocatedException(string message) :base(message) { }
    }
}
