using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeatManagementConsole.Interface
{
    public interface IAllocationManager<T> where T : class
    {
        public int Allocate(int assetId, int employeeId);
        public int Deallocate(int assetId);
    }
}
