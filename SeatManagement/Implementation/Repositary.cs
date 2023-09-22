using Microsoft.EntityFrameworkCore;
using SeatManagement.Interface;

namespace SeatManagement.Implementation
{
    public class Repositary<T> : IRepositary<T> where T : class
    {
        private readonly SeatManagementContext _context;
        private readonly DbSet<T> _DbSet;

        public Repositary(SeatManagementContext context)
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
            try
            {
                _DbSet.AddRange(items);
                _context.SaveChanges();
                Console.WriteLine("Added");
            }
            catch (DbUpdateException)
            {
                Console.WriteLine("Exception is thrown");
            }
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
