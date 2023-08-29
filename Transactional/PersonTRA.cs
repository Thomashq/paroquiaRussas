using Microsoft.EntityFrameworkCore;
using Npgsql;
using Microsoft.AspNetCore.Mvc;
using paroquiaRussas.Models;
using paroquiaRussas.Utility;
using static paroquiaRussas.Utility.Enum;

namespace paroquiaRussas.Transactional
{
    public class PersonTRA : Controller
    {
        private readonly AppDbContext _appDbContext;

        public PersonTRA(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        [HttpGet]
        public ActionResult<Person> GetPersonById(long personId = 1)
        {
            try
            {
                using (var connection = _appDbContext.Database.GetDbConnection() as NpgsqlConnection)
                {
                    connection.Open();

                    using (var command = new NpgsqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandText = "SELECT * FROM \"Person\" WHERE \"PersonId\" = @personId";
                        command.Parameters.AddWithValue("@personId", personId);

                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                Person person = new Person()
                                {
                                    Role = (Role)reader.GetInt32(reader.GetOrdinal("Role")),
                                    Username = reader.GetString(reader.GetOrdinal("Username")),
                                    Pwd = reader.GetString(reader.GetOrdinal("Pwd")),
                                };
                                connection.Close();
                                return person;
                            }
                            connection.Close();
                        }
                    }
                }

                return NotFound(); // Retorna NotFound se o usuário não for encontrado
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao recuperar usuário pelo Id: {ex.Message}");
            }
        }
    }
}
