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
    public class TrainingProgramRepository : ICRUDRepository<TrainingProgram>
    {
        private readonly string _connectionString;
        public TrainingProgramRepository(string connectionString)
        {
            this._connectionString = connectionString;
        }
        public void Create(TrainingProgram @object)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var sqlQuery = $"INSERT INTO TrainingProgram (name, area, coach, cost, duration) " +
                    "VALUES(@Name, @Area, @Coach, @Cost, @Duration)";
                db.Execute(sqlQuery, @object);
            }
        }

        public void Delete(TrainingProgram @object)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var sqlQuery = "DELETE FROM TrainingProgram WHERE name = @Name AND coach = @Coach";
                db.Execute(sqlQuery, @object);
            }
        }

        public TrainingProgram Get(TrainingProgram @object)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                return db.Query<TrainingProgram>("SELECT * FROM TrainingProgram WHERE name = @Name AND coach = @Coach", @object).FirstOrDefault();
            }
        }

        public List<TrainingProgram> GetObjects()
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                return db.Query<TrainingProgram>("SELECT * FROM TrainingProgram").ToList();
            }
        }

        public void Update(TrainingProgram @object, Key key)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var sqlQuery = $"UPDATE TrainingProgram SET " +
                    $"name = @Name, " +
                    $"area = @Area, " +
                    $"coach = @Coach, " +
                    $"cost = @Cost, " +
                    $"duration = @Duration " +
                    $"WHERE name = \'{key.First}\' AND coach = \'{key.Second}\'";
                db.Execute(sqlQuery, @object);
            }
        }
    }
}
