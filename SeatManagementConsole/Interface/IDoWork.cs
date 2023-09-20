using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeatManagementConsole.Interface
{
    public interface IDoWork
    {       
        public int WorkType { get; }
        public void DoWork();
    }
}
