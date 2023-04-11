using DAL;
using FullStackTechTest.Models.Shared;
using Models;

namespace FullStackTechTest.Models.Home;

public class DetailsViewModel
{
    public Person Person { get; set; }
    public Address Address { get; set; }
    
    public List<CheckBoxModel> SpecialityCheckBoxList { get; set; }

    public bool IsEditing { get; set; }

    public static async Task<DetailsViewModel> CreateAsync(int personId, bool isEditing, IPersonRepository personRepository, IAddressRepository addressRepository, IPersonSpecialityRepository personSpecialityRepository, ISpecialityRepository specialityRepository)
    {
        //create the checkbox list
        
        var model = new DetailsViewModel
        {
            Person = await personRepository.GetByIdAsync(personId),
            Address = await addressRepository.GetForPersonIdAsync(personId),
            SpecialityCheckBoxList = await CreateCheckBoxList(personId, personSpecialityRepository, specialityRepository),
            IsEditing = isEditing
        };
        return model;
    }

    public static async Task<List<CheckBoxModel>> CreateCheckBoxList(int personId, IPersonSpecialityRepository personSpecialityRepository, ISpecialityRepository specialityRepository)
    {
        List<CheckBoxModel> specialityCheckBoxList = new List<CheckBoxModel>();

        List<PersonSpeciality> personSpecialityList = await personSpecialityRepository.ListByPersonIdAsync(personId);

        List<Speciality> specialityList = await specialityRepository.ListAllAsync();

        //Get a list of the speciality and set isChecked based on person speciality row
        //this will need to return a row for each speciality regardless of there is an personspeciality row (the person has that speciality set for them)
        var querySpecialityCheckBoxList = from s in specialityList
                                      join ps in personSpecialityList on s.Id equals ps.SpecialityId into _s
                                      from subspeciality in _s.DefaultIfEmpty()
                                      orderby s.SpecialityName
                                      select new {
                                          Text = s.SpecialityName,
                                          Value = s.Id,
                                          IsChecked = subspeciality?.PersonId > 0 ? true : false
                                      };
        specialityCheckBoxList = querySpecialityCheckBoxList.Select(x => new CheckBoxModel { Value = x.Value , Text = x.Text , IsChecked = x.IsChecked }).ToList();


        return specialityCheckBoxList;

    }
}