using SeatManagementConsole.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeatManagementConsole.View
{
    public class ExitSeatManagement : IDoWork
    {
        public int WorkType => 7;

        public void DoWork()
        {
            Environment.Exit(0);
        }
    }
}
