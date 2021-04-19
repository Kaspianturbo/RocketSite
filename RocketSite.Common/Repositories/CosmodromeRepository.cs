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
    public class CosmodromeRepository : ICRUDRepository<Cosmodrome>
    {
        private readonly string _connectionString;
        public CosmodromeRepository(string connectionString)
        {
            this._connectionString = connectionString;
        }
        public void Create(Cosmodrome @object)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var sqlQuery = $"INSERT INTO Cosmodrome (name, timezone, latitude, longitude) " +
                    "VALUES(@Name, @Timezone, @Latitude, @Longitude)";
                db.Execute(sqlQuery,
                    new
                    {
                        Name = @object.Name,
                        Timezone = @object.Timezone,
                        Latitude = @object.Location.Latitude,
                        Longitude = @object.Location.Longitude
                    });
            }
        }

        public void Delete(Cosmodrome @object)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var sqlQuery = "DELETE FROM Cosmodrome WHERE name = @Name";
                db.Execute(sqlQuery, @object);
            }
        }

        public Cosmodrome Get(Cosmodrome @object)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var itemList = db.Query("SELECT * FROM Cosmodrome WHERE name = @Name", @object);

                return (from item in itemList
                        let location = new Location { Latitude = item.latitude, Longitude = item.longitude }
                        select new Cosmodrome
                        {
                            Name = item.name,
                            Timezone = item.timezone,
                            Location = location
                        }).FirstOrDefault();
            }
        }

        public List<Cosmodrome> GetObjects()
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var itemList = db.Query("SELECT * FROM Cosmodrome");

                return (from item in itemList
                        let location = new Location { Latitude = item.latitude, Longitude = item.longitude }
                        select new Cosmodrome
                        {
                            Name = item.name,
                            Timezone = item.timezone,
                            Location = location
                        }).ToList();
            }
        }
 
        public void Update(Cosmodrome @object, Key key)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var sqlQuery = $"UPDATE Cosmodrome SET " +
                    $"name = @Name, " +
                    $"timezone = @Timezone, " +
                    $"latitude = @Latitude, " +
                    $"longitude = @Longitude " +
                    $"WHERE name = @Key1";
                db.Execute(sqlQuery, new
                {
                    Name = @object.Name,
                    Timezone = @object.Timezone,
                    Latitude = @object.Location.Latitude,
                    Longitude = @object.Location.Longitude,
                    Key1 = key.First
                });
            }
        }
    }
}
