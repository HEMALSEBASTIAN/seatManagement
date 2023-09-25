using SeatManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeatManagementConsole.Interface
{
    public interface IAddEmployeeBulk
    {
        public int AddType { get; }
        public List<Employee>? Add();
    }
}
