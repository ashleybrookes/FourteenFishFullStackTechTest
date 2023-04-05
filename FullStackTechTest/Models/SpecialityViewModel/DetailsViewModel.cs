using DAL;
using Models;

namespace FullStackTechTest.Models.SpecialityViewModel;

public class DetailsViewModel
{
    public Speciality speciality { get; set; }
    public bool IsEditing { get; set; }
    public bool IsInserting { get; set; }
    public static async Task<DetailsViewModel> CreateAsync(int specialityId, bool isEditing, bool isInserting, ISpecialityRepository specialityRepository)
    {
        var model = new DetailsViewModel { };
        if (specialityId > 0)
        {
            model = new DetailsViewModel
            {
                speciality = await specialityRepository.GetByIdAsync(specialityId),
                IsEditing = isEditing,
                IsInserting = isInserting
            };
        } else
        {
            model = new DetailsViewModel
            {
                speciality = new Speciality(),
                IsEditing = isEditing,
                IsInserting = isInserting
            };

        }
        
        return model;
    }
}