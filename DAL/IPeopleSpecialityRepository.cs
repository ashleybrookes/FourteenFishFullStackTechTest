using Models;

namespace DAL;

public interface IPeopleSpecialityRepository
{
    Task<List<PeopleSpeciality>> ListAllAsync();
    Task<PeopleSpeciality> GetByIdAsync(int peopleSpecialityId);

    //Task<PeopleSpeciality> GetByPersonIdAsync(int peopleId);

    //Task SaveAsync(PeopleSpeciality peopleSpeciality);

    //Task DeleteAsync(PeopleSpeciality peopleSpeciality);

    //Task DeleteByPersonIdAsync(int peopleId);

    //Task<int> InsertAsync(PeopleSpeciality peopleSpeciality);
}