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
    public class CargoRepository : ICRUDRepository<Cargo>
    {
        private readonly string connectionString;
        public CargoRepository(string _connectionString)
        {
            connectionString = _connectionString;
        }
        public void Create(Cargo @object)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = $"INSERT INTO Cargo (name, type, weight, emaunt) " +
                    "VALUES(@Name, @Type, @Weight, @Emaunt)";
                db.Execute(sqlQuery, @object);
            }
        }

        public void Delete(Cargo @object)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = "DELETE FROM Cargo WHERE name = @Name AND type = @Type";
                db.Execute(sqlQuery, @object);
            }
        }

        public Cargo Get(Cargo @object)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                return db.Query<Cargo>("SELECT * FROM Cargo WHERE name = @Name AND type = @Type", @object).FirstOrDefault();
            }
        }

        public List<Cargo> GetObjects()
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                return db.Query<Cargo>("SELECT * FROM Cargo").ToList();
            }
        }

        public void Update(Cargo @object, Key key)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = $"UPDATE Cargo SET " +
                    $"name = @Name, " +
                    $"type = @Type, " +
                    $"weight = @Weight, " +
                    $"emaunt = @Emaunt " +
                    $"WHERE name = \'{key.First}\' AND type = \'{key.Second}\'";
                db.Execute(sqlQuery, @object);
            }
        }
    }
}
