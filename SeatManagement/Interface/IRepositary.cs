namespace SeatManagement.Interface
{
    public interface IRepositary<T> where T : class
    {
        public void Add(T item);
        public void Add(List<T> items);
        public IQueryable<T>? GetAll();
        public T GetById(int id);
        public void Update();
    }
}
