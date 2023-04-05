using System.Data;
using System.Reflection.PortableExecutable;
using System.Text;
using Models;
using MySql.Data.MySqlClient;

namespace DAL;

public class SpecialityRepository : ISpecialityRepository
{
    public async Task<List<Speciality>> ListAllAsync()
    {
        var specialityList = new List<Speciality>();
        
        var sql = new StringBuilder();
        sql.AppendLine("SELECT * FROM speciality");

        await using (var connection = new MySqlConnection(Config.DbConnectionString))
        {
            await connection.OpenAsync();
            
            var command = new MySqlCommand(sql.ToString(), connection);
            
            var reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                specialityList.Add(PopulateSpeciality(reader));
            }
        }

        return specialityList;
    }

    public async Task<Speciality> GetByIdAsync(int specialityId)
    {
        var speciality = new Speciality();
        
        var sql = new StringBuilder();
        sql.AppendLine("SELECT * FROM speciality");
        sql.AppendLine("WHERE Id = @specialityId");

        await using (var connection = new MySqlConnection(Config.DbConnectionString))
        {
            await connection.OpenAsync();

            var command = new MySqlCommand(sql.ToString(), connection);
            command.Parameters.AddWithValue("specialityId", specialityId);

            var reader = await command.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                speciality = PopulateSpeciality(reader);
            }
        }

        return speciality;
    }


    public async Task SaveAsync(Speciality speciality)
    {
        var sql = new StringBuilder();
        sql.AppendLine("UPDATE speciality SET");
        sql.AppendLine("SpecialityName = @SpecialityName");
        sql.AppendLine("WHERE Id = @specialityId");
        
        await using (var connection = new MySqlConnection(Config.DbConnectionString))
        {
            await connection.OpenAsync();

            var command = new MySqlCommand(sql.ToString(), connection);
            command.Parameters.AddWithValue("SpecialityName", speciality.SpecialityName);
            command.Parameters.AddWithValue("specialityId", speciality.Id);

            await command.ExecuteNonQueryAsync();
        }
    }

    public async Task DeleteAsync(Speciality speciality)
    {
        var sql = new StringBuilder();
        sql.AppendLine("DELETE speciality");
        sql.AppendLine("WHERE Id = @specialityId");

        await using (var connection = new MySqlConnection(Config.DbConnectionString))
        {
            await connection.OpenAsync();

            var command = new MySqlCommand(sql.ToString(), connection);
            command.Parameters.AddWithValue("SpecialityName", speciality.SpecialityName);
            command.Parameters.AddWithValue("specialityId", speciality.Id);

            await command.ExecuteNonQueryAsync();
        }
    }

    public async Task<int> InsertAsync(Speciality speciality)
    {
        var sql = new StringBuilder();
        sql.AppendLine("Insert INTO speciality (`SpecialityName`) VALUES (");
        sql.AppendLine("@SpecialityName");
        sql.AppendLine(")");

        await using (var connection = new MySqlConnection(Config.DbConnectionString))
        {
            await connection.OpenAsync();

            var command = new MySqlCommand(sql.ToString(), connection);
            command.Parameters.AddWithValue("SpecialityName", speciality.SpecialityName);

            await command.ExecuteNonQueryAsync();

            //return the new ID
            return (int)command.LastInsertedId;
        }
    }

    private Speciality PopulateSpeciality(IDataRecord data)
    {
        var speciality = new Speciality
        {
            Id = int.Parse(data["Id"].ToString()),
            SpecialityName = data["SpecialityName"].ToString()
            
        };
        return speciality;
    }
}