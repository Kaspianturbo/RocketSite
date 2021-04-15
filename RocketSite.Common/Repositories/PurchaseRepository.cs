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
    public class PurchaseRepository : ICRUDRepository<Purchase>
    {
        private readonly string _connectionString;
        public PurchaseRepository(string connectionString)
        {
            this._connectionString = connectionString;
        }
        public void Create(Purchase @object)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var sqlQuery = $"INSERT INTO Purchase (name, resourceName, resourceType, cost, spaceMissionName, employeeName, employeeCountry) " +
                    "VALUES(@Name, @ResourceName, @ResourceType, @Cost, @SpaceMissionName, @EmployeeName, @EmployeeCountry)";
                db.Execute(sqlQuery,
                    new
                    {
                        Name = @object.Name,
                        ResourceName = @object.Resources.Name,
                        ResourceType = @object.Resources.Type,
                        Cost = @object.Cost,
                        SpaceMissionName = @object.SpaceMission.Name,
                        EmployeeName = @object.Employee.Name,
                        EmployeeCountry = @object.Employee.Country
                    });
            }
        }

        public void Delete(Purchase @object)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var sqlQuery = "DELETE FROM Purchase WHERE name = @Name AND employeeName = @EmployeeName";
                db.Execute(sqlQuery, new { Name = @object.Name, EmployeeName = @object.Employee.Name});
            }
        }

        public Purchase Get(Purchase @object)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                return db.Query<Purchase>(
                    "SELECT * FROM Purchase WHERE name = @Name AND employeeName = @EmployeeName", 
                    new { Name = @object.Name, EmployeeName = @object.Employee.Name }
                    ).FirstOrDefault();
            }
        }

        public List<Purchase> GetObjects()
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                return db.Query<Purchase>("SELECT * FROM Purchase").ToList();
            }
        }

        public void Update(Purchase @object, Key key)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var sqlQuery = $"UPDATE Purchase SET " +
                    $"name = @Name, " +
                    $"resourceName = @ResourceName, " +
                    $"resourceType = @ResourceType, " +
                    $"cost = @Cost, " +
                    $"spaceMissionName = @SpaceMissionName, " +
                    $"employeeName = @EmployeeName, " +
                    $"employeeCountry = @EmployeeCountry " +
                    $"WHERE name = \'{key.First}\' AND employeeName = \'{key.Second}\'";
                db.Execute(sqlQuery, new
                {
                    Name = @object.Name,
                    ResourceName = @object.Resources.Name,
                    ResourceType = @object.Resources.Type,
                    Cost = @object.Cost,
                    SpaceMissionName = @object.SpaceMission.Name,
                    EmployeeName = @object.Employee.Name,
                    EmployeeCountry = @object.Employee.Country
                });
            }
        }
    }
}
