using System.Data;
using System.Reflection.PortableExecutable;
using System.Text;
using Models;
using MySql.Data.MySqlClient;

namespace DAL;

public class PersonSpecialityRepository : IPersonSpecialityRepository
{
    public async Task<List<PersonSpeciality>> ListAllAsync()
    {
        var PersonSpecialityList = new List<PersonSpeciality>();
        
        var sql = new StringBuilder();
        sql.AppendLine("select personspeciality.* from personspeciality");

        await using (var connection = new MySqlConnection(Config.DbConnectionString))
        {
            await connection.OpenAsync();
            
            var command = new MySqlCommand(sql.ToString(), connection);
            
            var reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                PersonSpecialityList.Add(PopulatePersonSpeciality(reader));
            }
        }

        return PersonSpecialityList;
    }

    public async Task<List<PersonSpeciality>> ListByPersonIdAsync(int personId)
    {
        var personSpecialityList = new List<PersonSpeciality>();
        
        var sql = new StringBuilder();
        sql.AppendLine("select * from peoplespeciality");
        sql.AppendLine("WHERE peoplespeciality.PersonId = @personId");

        await using (var connection = new MySqlConnection(Config.DbConnectionString))
        {
            await connection.OpenAsync();

            var command = new MySqlCommand(sql.ToString(), connection);
            command.Parameters.AddWithValue("personId", personId);

            var reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                personSpecialityList.Add(PopulatePersonSpeciality(reader));
            }
            
        }

        return personSpecialityList;
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

    public async Task DeleteBySpecialityIdAsync(int specialityId)
    {
        var sql = new StringBuilder();
        sql.AppendLine("DELETE FROM peoplespeciality");
        sql.AppendLine("WHERE specialityId = @specialityId");

        await using (var connection = new MySqlConnection(Config.DbConnectionString))
        {
            await connection.OpenAsync();

            var command = new MySqlCommand(sql.ToString(), connection);
            command.Parameters.AddWithValue("specialityId", specialityId);

            await command.ExecuteNonQueryAsync();
        }
    }

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

    private PersonSpeciality PopulatePersonSpeciality(IDataRecord data)
    {
        var personSpeciality = new PersonSpeciality
        {
            Id = int.Parse(data["Id"].ToString()),
            SpecialityId = int.Parse(data["SpecialityId"].ToString()),
            PersonId = int.Parse(data["PersonId"].ToString()),

        };
        return personSpeciality;
    }
}