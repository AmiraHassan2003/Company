using MVCDay2.Models.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MVCDay2.ViewModel
{
    public class EmployeeViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Age { get; set; }

        public IFormFile Image { get; set; }

        public double Salary { get; set; }

        public int? CompanyId { get; set; }
        public string? Address { get; set; }

        public string Email { get; set; }

        public string? Phone { get; set; }
    }
}
