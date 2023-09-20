using SeatManagementConsole.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeatManagementConsole.Implementation
{
    internal class AllocationManager<T> : IAllocationManager<T> where T : class
    {
        private readonly string _endPoint;

        public AllocationManager(string endPoint)
        {
            _endPoint=endPoint;
        }
        public void Allocate(T obj)
        {
            IAPICall<T> aPICall = new APICall<T>(_endPoint + "/allocate");
            aPICall.Update(obj);
        }

        public void Deallocate(T obj)
        {
            IAPICall<T> aPICall = new APICall<T>(_endPoint + "/deallocate");
            aPICall.Update(obj);
        }
    }
}
