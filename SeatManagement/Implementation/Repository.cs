using Microsoft.EntityFrameworkCore;
using SeatManagement.Interface;

namespace SeatManagement.Implementation
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly SeatManagementContext _context;
        private readonly DbSet<T> _DbSet;

        public Repository(SeatManagementContext context)
        {
            _context = context;
            _DbSet = _context.Set<T>();
        }
        public void Add(T item)
        { 
            _DbSet.Add(item);
            _context.SaveChanges();
        }
        public void Add(List<T> items)
        { 
            _DbSet.AddRange(items);
            _context.SaveChanges();
        }
        public IQueryable<T>? GetAll()
        {
            return _DbSet;
        }

        public T GetById(int id)
        {
            return _DbSet.Find(id);
        }

        public void Update()
        {
            _context.SaveChanges();
        }
    }
}
