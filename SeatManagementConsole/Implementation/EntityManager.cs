using SeatManagement.Models;
using SeatManagementConsole.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeatManagementConsole.Implementation
{
    public class EntityManager<T> : IEntityManager<T> where T : class
    {
        private readonly IAPICall<T> aPICall;
        public EntityManager(string endPoint)
        {
            aPICall= new APICall<T>(endPoint);
        }
        public int Add(T item)
        {
            return aPICall.Add(item);
        }

        public void BulkAdd(List<T> items)
        {
            aPICall.BulkAdd(items);
        }

        public List<T> Get()
        {
            return aPICall.GetAll().ToList();
        }

        public T GetById(int id)
        {
            return aPICall.GetById(id);
        }

        public void Update(T item)
        {
            aPICall.Update(item);
        }
    }
}
