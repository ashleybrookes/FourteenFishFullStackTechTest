using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Models;

public class PeopleSpeciality
{
    [Required]
    public int Id { get; set; }
    
    [Required]
    public int SpecialityId { get; set; }
    
    [Required]
    public string SpecialityName { get; set; }

    [Required]
    public int PersonId { get; set; }
}