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
    public class LocationRepository : ICRUDRepository<Location>
    {
        private readonly string _connectionString;
        public LocationRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        public void Create(Location @object)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var sqlQuery = $"INSERT INTO Location (latitude, longitude, country, city) " +
                    "VALUES(@Latitude, @Longitude, @Country, @City)";
                db.Execute(sqlQuery, @object);
            }
        }

        public void Delete(Location @object)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var sqlQuery = "DELETE FROM Location WHERE latitude = @Latitude AND longitude = @Longitude";
                db.Execute(sqlQuery, @object);
            }
        }

        public Location Get(Location @object)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var itemList = db.Query("SELECT * FROM Location WHERE latitude = @Latitude AND longitude = @Longitude", @object);

                return (from item in itemList
                        select new Location
                        {
                            Latitude = item.latitude,
                            Longitude = item.longitude,
                            Country = item.country,
                            City = item.city
                        }).FirstOrDefault();
            }
        }

        public List<Location> GetObjects()
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var itemList = db.Query("SELECT * FROM Location");

                return (from item in itemList
                        select new Location
                        {
                            Latitude = item.latitude,
                            Longitude = item.longitude,
                            Country = item.country,
                            City = item.city
                        }).ToList();
            }
        }

        public void Update(Location @object, Key key)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var sqlQuery = $"UPDATE Location SET " +
                    $"latitude = @Latitude, " +
                    $"longitude = @Longitude, " +
                    $"country = @Country, " +
                    $"city = @City " +
                    $"WHERE latitude = {key.First} AND longitude = {key.Second}";
                db.Execute(sqlQuery, new
                {
                    @object.Latitude,
                    @object.Longitude,
                    @object.Country,
                    @object.City
                });
            }
        }
    }
}
