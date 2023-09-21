using SeatManagement.Models;
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
        public int Allocate(int assetId, int employeeId)
        {
            IAPICall<T> aPICall = new APICall<T>(_endPoint + $"/{assetId}?employeeId={employeeId}");
            return aPICall.Update();
        }
        public int Deallocate(int assetId)
        {
            IAPICall<T> aPICall = new APICall<T>(_endPoint + $"/{assetId}");
            return aPICall.Update();
        }
    }
}
