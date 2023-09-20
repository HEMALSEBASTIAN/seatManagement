using SeatManagement.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace SeatManagement.DTO
{
    public class EmployeeDTO
    {
        public string? EmployeeName { get; set; }
        public int DepartmentId { get; set; }
    }
}
