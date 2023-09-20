namespace SeatManagement.CustomException
{
    public class AllocationException: Exception
    {
        public AllocationException(string message): base(message) { }
    }
}
