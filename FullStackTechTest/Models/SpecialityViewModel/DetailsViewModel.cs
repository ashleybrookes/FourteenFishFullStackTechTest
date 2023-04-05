using DAL;
using Models;

namespace FullStackTechTest.Models.SpecialityViewModel;

public class DetailsViewModel
{
    public Speciality speciality { get; set; }
    public bool IsEditing { get; set; }

    public bool IsInserting { get; set; }

    public static async Task<DetailsViewModel> CreateAsync(int specialityId, bool isEditing, bool IsInserting, ISpecialityRepository specialityRepository)
    {
        var model = new DetailsViewModel
        {
            speciality = await specialityRepository.GetByIdAsync(specialityId),
            IsEditing = isEditing,
            IsInserting = IsInserting
        };
        return model;
    }
}