namespace SeatManagement.CustomException
{
    public class ForeignKeyViolationException: Exception
    {
        public ForeignKeyViolationException(string message) : base(message)
        {
            
        }
    }
}
