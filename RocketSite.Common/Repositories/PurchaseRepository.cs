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
                var sqlQuery = $"INSERT INTO Purchase (name, resourceName, resourceType, cost, spaceMissionName) " +
                    "VALUES(@Name, @ResourceName, @ResourceType, @Cost, @SpaceMissionName)";
                db.Execute(sqlQuery,
                    new
                    {
                        @object.Name,
                        ResourceName = @object.Resources.Name,
                        ResourceType = @object.Resources.Type,
                        @object.Cost,
                        SpaceMissionName = @object.SpaceMission.Name,
                    });
            }
        }

        public void Delete(Purchase @object)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var sqlQuery = "DELETE FROM Purchase WHERE name = @Name AND cost = @Cost";
                db.Execute(sqlQuery, @object);
            }
        }

        public Purchase Get(Purchase @object)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var itemList = db.Query("SELECT * FROM Purchase WHERE name = @Name AND cost = @Cost", @object);

                return (from item in itemList
                        let spaceMission = new SpaceMission { Name = item.spaceMissionName }
                        let resource = new Resources { Name = item.resourceName, Type = Enum.Parse<ResourceOption>(item.resourceType) }
                        select new Purchase
                    {
                        Name = item.name,
                        Resources = resource,
                        Cost = item.cost,
                        SpaceMission = spaceMission
                    }).FirstOrDefault();
            }
        }

        public List<Purchase> GetObjects()
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var itemList = db.Query("SELECT * FROM Purchase");

                return (from item in itemList
                    let spaceMission = new SpaceMission { Name = item.spaceMissionName }
                    let resource = new Resources { Name = item.resourceName, Type = Enum.Parse<ResourceOption>(item.resourceType) }
                    select new Purchase
                    {
                        Name = item.name,
                        Resources = resource,
                        Cost = item.cost,
                        SpaceMission = spaceMission
                    }).ToList();
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
                    $"spaceMissionName = @SpaceMissionName " +
                    $"WHERE name = @Key1 AND cost = @Key2";
                db.Execute(sqlQuery, new
                {
                    @object.Name,
                    ResourceName = @object.Resources.Name,
                    ResourceType = @object.Resources.Type,
                    @object.Cost,
                    SpaceMissionName = @object.SpaceMission.Name,
                    Key1 = key.First, Key2 = key.Second
                });
            }
        }
    }
}
