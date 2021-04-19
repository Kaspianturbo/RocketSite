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
                var itemList = db.Query("SELECT * FROM Cargo WHERE name = @Name AND type = @Type", @object);
                
                return (from item in itemList
                        let spaceMission = new SpaceMission { Name = item.spaceMissionName }
                        let customer = new Customer { Name = item.customerName, Country = item.customerCountry }
                        select new Cargo
                        {
                            Name = item.name,
                            Type = Enum.Parse<CargoOption>(item.type),
                            Weight = item.weight,
                            Emaunt = item.emaunt,
                            SpaceMission = spaceMission,
                            Customer = customer
                        }).FirstOrDefault();
            }
        }

        public List<Cargo> GetObjects()
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var itemList =  db.Query(
                    "SELECT * FROM Cargo");

                return (from item in itemList
                        let spaceMission = new SpaceMission { Name = item.spaceMissionName }
                        let customer = new Customer { Name = item.customerName, Country = item.customerCountry }
                        select new Cargo
                        {
                            Name = item.name,
                            Type = Enum.Parse<CargoOption>(item.type),
                            Weight = item.weight,
                            Emaunt = item.emaunt,
                            SpaceMission = spaceMission,
                            Customer = customer
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
                    $"WHERE name = @Key1 AND type = @Key2";
                  db.Execute(sqlQuery,
                      new
                      {
                          @object.Name, @object.Type, @object.Weight, @object.Emaunt,
                          CustomerName = @object.Customer.Name, CustomerCountry = @object.Customer.Country,
                          SpaceMissionName = @object.SpaceMission.Name,
                          Key1 = key.First, Key2 = key.Second
                      });
            }
        }
    }
}
