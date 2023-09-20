using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeatManagementConsole.Interface
{
    public interface IEntityManager<T>
    {
        public int Add(T item);
        public void BulkAdd(List<T> items);
        public List<T> Get();
        public T GetById(int id);
        public void Update(T item);
    }
}
