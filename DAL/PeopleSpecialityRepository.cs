using System.Data;
using System.Reflection.PortableExecutable;
using System.Text;
using Models;
using MySql.Data.MySqlClient;

namespace DAL;

public class PeopleSpecialityRepository : IPeopleSpecialityRepository
{
    public async Task<List<PeopleSpeciality>> ListAllAsync()
    {
        var PeopleSpecialityList = new List<PeopleSpeciality>();
        
        var sql = new StringBuilder();
        sql.AppendLine("select peoplespeciality.* , speciality.SpecialityName from peoplespeciality inner join speciality on peoplespeciality.specialityId = speciality.Id");

        await using (var connection = new MySqlConnection(Config.DbConnectionString))
        {
            await connection.OpenAsync();
            
            var command = new MySqlCommand(sql.ToString(), connection);
            
            var reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                PeopleSpecialityList.Add(PopulatePeopleSpeciality(reader));
            }
        }

        return PeopleSpecialityList;
    }

    public async Task<PeopleSpeciality> GetByIdAsync(int peopleSpecialityId)
    {
        var peopleSpeciality = new PeopleSpeciality();
        
        var sql = new StringBuilder();
        sql.AppendLine("select peoplespeciality.* , speciality.SpecialityName from peoplespeciality inner join speciality on peoplespeciality.specialityId = speciality.Id");
        sql.AppendLine("WHERE peoplespeciality.Id = @peoplespecialityId");

        await using (var connection = new MySqlConnection(Config.DbConnectionString))
        {
            await connection.OpenAsync();

            var command = new MySqlCommand(sql.ToString(), connection);
            command.Parameters.AddWithValue("peoplespecialityId", peopleSpecialityId);

            var reader = await command.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                peopleSpeciality = PopulatePeopleSpeciality(reader);
            }
        }

        return peopleSpeciality;
    }


    //public async Task SaveAsync(Speciality speciality)
    //{
    //    var sql = new StringBuilder();
    //    sql.AppendLine("UPDATE speciality SET");
    //    sql.AppendLine("SpecialityName = @SpecialityName");
    //    sql.AppendLine("WHERE Id = @specialityId");
        
    //    await using (var connection = new MySqlConnection(Config.DbConnectionString))
    //    {
    //        await connection.OpenAsync();

    //        var command = new MySqlCommand(sql.ToString(), connection);
    //        command.Parameters.AddWithValue("SpecialityName", speciality.SpecialityName);
    //        command.Parameters.AddWithValue("specialityId", speciality.Id);

    //        await command.ExecuteNonQueryAsync();
    //    }
    //}

    //public async Task DeleteAsync(Speciality speciality)
    //{
    //    var sql = new StringBuilder();
    //    sql.AppendLine("DELETE speciality");
    //    sql.AppendLine("WHERE Id = @specialityId");

    //    await using (var connection = new MySqlConnection(Config.DbConnectionString))
    //    {
    //        await connection.OpenAsync();

    //        var command = new MySqlCommand(sql.ToString(), connection);
    //        command.Parameters.AddWithValue("SpecialityName", speciality.SpecialityName);
    //        command.Parameters.AddWithValue("specialityId", speciality.Id);

    //        await command.ExecuteNonQueryAsync();
    //    }
    //}

    //public async Task<int> InsertAsync(Speciality speciality)
    //{
    //    var sql = new StringBuilder();
    //    sql.AppendLine("Insert INTO speciality (`SpecialityName`) VALUES (");
    //    sql.AppendLine("@SpecialityName");
    //    sql.AppendLine(")");

    //    await using (var connection = new MySqlConnection(Config.DbConnectionString))
    //    {
    //        await connection.OpenAsync();

    //        var command = new MySqlCommand(sql.ToString(), connection);
    //        command.Parameters.AddWithValue("SpecialityName", speciality.SpecialityName);

    //        await command.ExecuteNonQueryAsync();

    //        //return the new ID
    //        return (int)command.LastInsertedId;
    //    }
    //}

    private PeopleSpeciality PopulatePeopleSpeciality(IDataRecord data)
    {
        var peopleSpeciality = new PeopleSpeciality
        {
            Id = int.Parse(data["Id"].ToString()),
            SpecialityName = data["SpecialityName"].ToString(),
            SpecialityId = int.Parse(data["SpecialityId"].ToString()),
            PersonId = int.Parse(data["PersonId"].ToString()),

        };
        return peopleSpeciality;
    }
}