using SeatManagement.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeatManagementConsole.Interface
{
    public interface IReportFilter
    {
        public int FilterType { get; }
        public void Filter(ViewFacilityDTO facilityDTO);
    }
}
