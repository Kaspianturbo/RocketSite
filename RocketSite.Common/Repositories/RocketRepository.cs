using Dapper;
using Microsoft.Data.SqlClient;
using RocketSite.Common.Interfaces;
using RocketSite.Common.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RocketSite.Common.Repositories
{
    public class RocketRepository : ICRUDRepository<Rocket>
    {
        private readonly string connectionString;
        public RocketRepository(string _connectionString)
        {
            connectionString = _connectionString;
        }
        public void Create(Rocket user)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                //var sqlQuery = "INSERT INTO Rocket (Name, Age) VALUES(@Name, @Age)";
                //db.Execute(sqlQuery, user);

                // если мы хотим получить id добавленного пользователя
                //var sqlQuery = "INSERT INTO Users (Name, Age) VALUES(@Name, @Age); SELECT CAST(SCOPE_IDENTITY() as int)";
                //int? userId = db.Query<int>(sqlQuery, user).FirstOrDefault();
                //user.Id = userId.Value;
            }
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Rocket Get(string name)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                return db.Query<Rocket>("SELECT * FROM Rocket WHERE name = @name", new { name }).FirstOrDefault();
            }
        }

        public List<Rocket> GetUsers()
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                return db.Query<Rocket>("SELECT * FROM Rocket").ToList();
            }
        }

        public void Update(Rocket rocket)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = "UPDATE Rocket SET weight = @weight, height = @height WHERE name = @name";
                db.Execute(sqlQuery, rocket);
            }
        }
    }
}
