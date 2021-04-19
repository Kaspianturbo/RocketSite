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
    public class CustomerRepository : ICRUDRepository<Customer>
    {
        private readonly string _connectionString;
        public CustomerRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        public void Create(Customer @object)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var sqlQuery = $"INSERT INTO Customer (name, country, totalWorth) " +
                    "VALUES(@Name, @Country, @TotalWorth)";
                db.Execute(sqlQuery, @object);
            }
        }

        public void Delete(Customer @object)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var sqlQuery = "DELETE FROM Customer WHERE name = @Name AND country = @Country";
                db.Execute(sqlQuery, @object);
            }
        }

        public Customer Get(Customer @object)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var itemList = db.Query("SELECT * FROM Customer WHERE name = @Name AND country = @Country", @object);

                return (from item in itemList
                        select new Customer
                        {
                            Name = item.name,
                            Country = item.country,
                            TotalWorth = item.totalWorth
                        }).FirstOrDefault();
            }
        }

        public List<Customer> GetObjects()
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var itemList = db.Query("SELECT * FROM Customer");

                return (from item in itemList
                        select new Customer
                        {
                            Name = item.name,
                            Country = item.country,
                            TotalWorth = item.totalWorth
                        }).ToList();
            }
        }

        public void Update(Customer @object, Key key)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var sqlQuery = $"UPDATE Customer SET " +
                    $"name = @Name, " +
                    $"country = @Country, " +
                    $"totalWorth = @TotalWorth " +
                    $"WHERE name = @Key1 AND country = @Key2";
                db.Execute(sqlQuery, new { @object.Name, @object.Country, TotalWorth = @object.TotalWorth, Key1 = key.First, Key2 = key.Second});
            }
        }
    }
}
