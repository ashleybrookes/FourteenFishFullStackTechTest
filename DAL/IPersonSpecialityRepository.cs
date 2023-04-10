using Models;

namespace DAL;

public interface IPersonSpecialityRepository
{
    Task<List<PersonSpeciality>> ListAllAsync();

    Task<List<PersonSpeciality>> ListByPersonIdAsync(int personId);

    //Task<PersonSpeciality> GetByIdAsync(int personSpecialityId);

    //Task<PersonSpeciality> GetByPersonIdAsync(int personId);

    //Task SaveAsync(PersonSpeciality personSpeciality);

    Task DeleteBySpecialityIdAsync(int specialityId);

    //Task DeleteByPersonIdAsync(int personId);

    //Task<int> InsertAsync(PersonSpeciality personSpeciality);
}