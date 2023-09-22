using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeatManagementConsole.Interface
{
    public interface IAPICall<T> where T : class
    {
        public List<T>? GetAll();
        public T? GetById(int id);
        //public void Add(T item);
        public int Add(T item);
        public void BulkAdd(List<T> items);
        public void Update(T item);
        public int Update();
    }
}
