using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace MVCDay2.Models.Entities;

public class Employee
{
    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = "Please Enter Your Name")]
    public string Name { get; set; }

    public int Age { get; set; }

    public string? Image { get; set; }

    public double Salary { get; set; }

    [ForeignKey(nameof(Company))]
    public int? CompanyId { get; set; }
    public virtual Company? Company { get; set; }

    public string? Address { get; set; }

    [Required(ErrorMessage = "Please Enter Your Email")]
    [DataType(DataType.EmailAddress)]
    [EmailAddress(ErrorMessage = "Invalid email format.")]
    public string Email { get; set; }


    [RegularExpression(@"^\d{11}$", ErrorMessage = "Phone number must be exactly 11 digits.")]
    public string? Phone { get; set; }

}
