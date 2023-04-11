using Models;

namespace DAL;

public interface IPersonSpecialityRepository
{
    Task<List<PersonSpeciality>> ListAllAsync();

    Task<List<PersonSpeciality>> ListByPersonIdAsync(int personId);

    Task DeleteByPersonIdAsync(int personId);
    Task DeleteBySpecialityIdAsync(int specialityId);

    Task<int> InsertAsync(PersonSpeciality personSpeciality);
}