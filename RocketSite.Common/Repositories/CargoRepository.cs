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
using RocketSite.Common.Options;

namespace RocketSite.Common.Repositories
{
    public class CargoRepository : ICRUDRepository<Cargo>
    {
        private readonly string _connectionString;
        public CargoRepository(string connectionString)
        {
            this._connectionString = connectionString;
        }
        public void Create(Cargo @object)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var sqlQuery = $"INSERT INTO Cargo (name, type, weight, emaunt, customerName, customerCountry, spaceMissionName) " +
                    "VALUES(@Name, @Type, @Weight, @Emaunt, @CustomerName, @CustomerCountry, @SpaceMissionName)";
                db.Execute(sqlQuery,
                    new
                    {
                        @object.Name, @object.Type, @object.Weight, @object.Emaunt,
                        CustomerName = @object.Customer.Name, CustomerCountry = @object.Customer.Country,
                        SpaceMissionName = @object.SpaceMission.Name
                    });
            }
        }

        public void Delete(Cargo @object)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var sqlQuery = "DELETE FROM Cargo WHERE name = @Name AND type = @Type";
                db.Execute(sqlQuery, @object);
            }
        }

        public Cargo Get(Cargo @object)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                return db.Query<Cargo, Customer, SpaceMission, Cargo>(
                    "SELECT * FROM Cargo as ca " +
                    "WHERE ca.name = @Name AND ca.type = @Type " +
                    "LEFT JOIN Customer as cu ON ca.customerName = cu.name " +
                    "AND ca.customerCountry = cu.country " +
                    "LEFT JOIN SpaceMission as sm ON ca.spaceMissionName = sm.name",
                    (ca, cu, sm) =>
                    {
                        ca.Customer = cu;
                        ca.SpaceMission = sm;
                        return ca;
                    }, @object).FirstOrDefault();
            }
        }

        public List<Cargo> GetObjects()
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var itemList =  db.Query(
                    "SELECT name, type, weight, emaunt, customerName, customerCountry, spaceMissionName " +
                    "FROM Cargo");

                return (from item in itemList
                    let customer = new Customer {Name = item.customerName, Country = item.customerCountry}
                    let mission = new SpaceMission {Name = item.spaceMissionName}
                    select new Cargo
                    {
                        Name = item.name,
                        Type = Enum.Parse<CargoOption>(item.type),
                        Weight = item.weight,
                        Emaunt = item.emaunt,
                        Customer = customer,
                        SpaceMission = mission
                    }).ToList();
            }
        }

        public void Update(Cargo @object, Key key)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                  var sqlQuery = $"UPDATE Cargo SET " +
                    $"name = @Name, " +
                    $"type = @Type, " +
                    $"weight = @Weight, " +
                    $"emaunt = @Emaunt, " +
                    $"customerName = @CustomerName, " +
                    $"customerCountry = @CustomerCountry, " +
                    $"spaceMissionName = @SpaceMissionName " +
                    $"WHERE name = \'{key.First}\' AND type = \'{key.Second}\'";
                  db.Execute(sqlQuery,
                      new
                      {
                          @object.Name, @object.Type, @object.Weight, @object.Emaunt,
                          CustomerName = @object.Customer.Name, CustomerCountry = @object.Customer.Country,
                          SpaceMissionName = @object.SpaceMission.Name
                      });
            }
        }
    }
}
