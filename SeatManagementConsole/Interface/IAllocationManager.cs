using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeatManagementConsole.Interface
{
    public interface IAllocationManager<T> where T : class
    {
        public void Allocate(T obj);
        public void Deallocate(T obj);
    }
}
