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
        private readonly string connectionString;
        public LocationRepository(string _connectionString)
        {
            connectionString = _connectionString;
        }
        public void Create(Location @object)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = $"INSERT INTO Location (latitude, longitude, Country, City) " +
                    "VALUES(@Latitude, @Longitude, @Country, @City)";
                db.Execute(sqlQuery, @object);
            }
        }

        public void Delete(Location @object)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = "DELETE FROM Location WHERE latitude = @Latitude AND longitude = @Longitude";
                db.Execute(sqlQuery, @object);
            }
        }

        public Location Get(Location @object)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                return db.Query<Location>("SELECT * FROM Location WHERE latitude = @Latitude AND longitude = @Longitude", @object).FirstOrDefault();
            }
        }

        public List<Location> GetObjects()
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                return db.Query<Location>("SELECT * FROM Location").ToList();
            }
        }

        public void Update(Location @object, Key key)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = $"UPDATE Location SET " +
                    $"latitude = @Latitude, " +
                    $"longitude = @Longitude, " +
                    $"Country = @Country, " +
                    $"City = @City " +
                    $"WHERE latitude = {key.First} AND longitude = {key.Second}";
                db.Execute(sqlQuery, @object);
            }
        }
    }
}
