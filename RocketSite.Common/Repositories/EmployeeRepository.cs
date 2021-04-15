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
    public class EmployeeRepository : ICRUDRepository<Employee>
    {
        private readonly string _connectionString;
        public EmployeeRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        public void Create(Employee @object)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var sqlQuery = $"INSERT INTO Employee (name, country, education, sex, profession, nameTrainingProgram, coachTrainingProgram, cosmodromeName) " +
                    "VALUES(@Name, @Country, @Education, @Sex, @Profession, @NameTrainingProgram, @CoachTrainingProgram, @CosmodromeName)";
                db.Execute(sqlQuery,
                    new
                    {
                        Name = @object.Name, 
                        Country = @object.Country, 
                        Education = @object.Education, 
                        Sex = @object.Sex,
                        Profession = @object.Profession,
                        NameTrainingProgram = @object.TrainingProgram.Name,
                        CoachTrainingProgram = @object.TrainingProgram.Coach,
                        CosmodromeName = @object.Cosmodrome.Name
                    });
            }
        }

        public void Delete(Employee @object)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var sqlQuery = "DELETE FROM Employee WHERE name = @Name AND country = @Country";
                db.Execute(sqlQuery, @object);
            }
        }

        public Employee Get(Employee @object)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                return db.Query<Employee>("SELECT * FROM Employee WHERE name = @Name AND country = @Country", @object).FirstOrDefault();
            }
        }

        public List<Employee> GetObjects()
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                

                return db.Query<Employee, TrainingProgram, Employee>(
                    "SELECT * FROM Employee as e LEFT JOIN TrainingProgram as t ON e.nameTrainingProgram = t.name AND e.coachTrainingProgram = t.coach",
                    (e, t) =>
                    {
                        Employee employee = e;
                        e.TrainingProgram = t;
                        e.Cosmodrome = new Cosmodrome();
                        return employee;
                    }, splitOn: "nameTrainingProgram").ToList(); ;
            }
        }

        public void Update(Employee @object, Key key)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                StringBuilder builder = new StringBuilder();
                builder.Append($"UPDATE Employee SET " +
                               $"name = @Name, " +
                               $"country = @Country, " +
                               $"education = @Education, " +
                               $"sex = @Sex, " +
                               $"profession = @Profession ");
                builder.Append($"WHERE name = \'{key.First}\' AND country = \'{key.Second}\'");
                db.Execute(builder.ToString(), new
                {
                    Name = @object.Name, 
                    Country = @object.Country,
                    Education = @object.Education, 
                    Sex = @object.Sex, 
                    Profession = @object.Profession
                });
            }
        }
    }
}
