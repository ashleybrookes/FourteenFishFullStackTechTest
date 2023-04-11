using Models;

namespace DAL;

public interface ISpecialityRepository
{
    Task<List<Speciality>> ListAllAsync();
    Task<Speciality> GetByIdAsync(int specialityId);

    Task SaveAsync(Speciality speciality);

    Task DeleteByIdAsync(int specialityId);

    Task<int> InsertAsync(Speciality speciality);
}