using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Models;

public class Speciality
{
    [Required]
    public int Id { get; set; }
    
    [Required]
    [MaxLength(200)]
    public string SpecialityName { get; set; }
}