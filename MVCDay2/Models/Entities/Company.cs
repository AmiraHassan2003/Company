using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace MVCDay2.Models.Entities;

public class Company
{
    [Key]
    public int Id { get; set; }

    [StringLength(50)]
    [Required]
    public string Name { get; set; }

    public string? Type { get; set; }

    [DataType(DataType.Date)]
    public DateTime? DateStart { get; set; }

    public string? Address { get; set; }

    public virtual ICollection<Employee>? employees { get; set; }

}
