using Dapper;
using Microsoft.Data.SqlClient;
using RocketSite.Common.Interfaces;
using RocketSite.Common.Models;
using RocketSite.Common.Options;
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
                var sqlQuery = $"INSERT INTO Employee (name, country, education, sex, profession, nameTrainingProgram, coachTrainingProgram) " +
                    "VALUES(@Name, @Country, @Education, @Sex, @Profession, @NameTrainingProgram, @CoachTrainingProgram)";
                db.Execute(sqlQuery,
                    new
                    {
                        Name = @object.Name, 
                        Country = @object.Country, 
                        Education = @object.Education, 
                        Sex = @object.Sex,
                        Profession = @object.Profession,
                        NameTrainingProgram = @object.TrainingProgram.Name,
                        CoachTrainingProgram = @object.TrainingProgram.Coach
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
                var itemList = db.Query("SELECT * FROM Employee WHERE name = @Name", @object);

                return (from item in itemList
                        let trainingProgram = new TrainingProgram { Name = item.nameTrainingProgram, Coach = item.coachTrainingProgram }
                        select new Employee
                        {
                            Name = item.name,
                            Country = item.country,
                            Education = item.education,
                            Sex = Enum.Parse<SexOption>(item.sex),
                            Profession = item.profession,
                            TrainingProgram = trainingProgram
                        }).FirstOrDefault();
            }
        }

        public List<Employee> GetObjects()
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var itemList = db.Query("SELECT * FROM Employee");

                return (from item in itemList
                        let trainingProgram = new TrainingProgram { Name = item.nameTrainingProgram, Coach = item.coachTrainingProgram }
                        select new Employee
                        {
                            Name = item.name,
                            Country = item.country,
                            Education = item.education,
                            Sex = Enum.Parse<SexOption>(item.sex),
                            Profession = item.profession,
                            TrainingProgram = trainingProgram
                        }).ToList();
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
                               $"profession = @Profession, " +
                               $"nameTrainingProgram = @NameTrainingProgram, " +
                               $"coachTrainingProgram = @CoachTrainingProgram ");
                builder.Append($"WHERE name = @Key1 AND country = @Key2");
                db.Execute(builder.ToString(), new
                {
                    @object.Name, 
                    @object.Country,
                    @object.Education, 
                    @object.Sex, 
                    @object.Profession,
                    NameTrainingProgram = @object.TrainingProgram.Name,
                    CoachTrainingProgram = @object.TrainingProgram.Coach,
                    Key1 = key.First, Key2 = key.Second
                });
            }
        }
    }
}
