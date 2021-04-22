using System;
using System.Collections.Generic;
using System.Data;
using System.Formats.Asn1;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Data.SqlClient;
using RocketSite.Common.Interfaces;
using RocketSite.Common.Models;
using RocketSite.Common.Options;

namespace RocketSite.Common.Repositories
{
    public class RocketSiteRepository : IRocketSiteRepository
    {
        private readonly string _connectionString;
        public RocketSiteRepository(string connectionString)
        {
            this._connectionString = connectionString;
        }
        public List<SpaceMission> Get1(string name, string type, string startDate, string endDate)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var itemList = db.Query(
                    "SELECT cr.name as crname, cr.type as crtype, cr.weight as crweight, cr.weight as crweight, " +
                    "cr.emaunt as cremaunt, cr.spaceMissionName, cr.customerName, cr.customerCountry, " +
                    "sm.*, ro.name as roname, ro.version as roversion, ro.weight as roweight, ro.height as roheight, ro.diameter as rodiameter, " +
                    "ro.cost as rocost, ro.stages as rostages, ro.massToLEO as romassToLEO, ro.massToGTO as romassToGTO, ro.engineType as roengineType, " +
                    "co.name as coname, co.timezone as cotimezone, lo.latitude as lolatitude, lo.longitude as lolongitude, " +
                    "lo.country as locountry, lo.city as locity " +
                    "FROM Cargo as cr " +
                    "INNER JOIN SpaceMission as sm ON cr.spaceMissionName = sm.name " +
                    "INNER JOIN Rocket as ro ON sm.rocketName = ro.name " +
                    "AND sm.rocketVersion = ro.version " +
                    "INNER JOIN Cosmodrome as co ON sm.cosmodromeName = co.name " +
                    "LEFT JOIN Location as lo ON co.latitude = lo.latitude " +
                    "AND co.longitude = lo.longitude " +
                    "WHERE cr.name = @Name AND cr.type = @Type AND sm.startDate = @StartDate AND sm.endDate = @EndDate",
                    param: new { Name = name, Type = Enum.Parse<CargoOption>(type), StartDate = startDate, EndDate = endDate });

                return (from item in itemList
                        let rocket = new Rocket 
                        { 
                            Name = item.roname, Version = item.roversion, Weight = item.roweight, Height = item.roheight, 
                            Diameter = item.rodiameter, Cost = item.rocost, Stages = item.rostages, MassToLEO = item.romassToLEO, 
                            MassToGTO = item.romassToGTO, EngineType = item.roengineType 
                        }
                        let location = new Location { City = item.locity, Country = item.locountry, Latitude = item.lolatitude, Longitude = item.lolongitude }
                        let cosmodrome = new Cosmodrome { Name = item.coname, Timezone = item.cotimezone, Location = location }
                        let cargo = new Cargo { Name = item.crname, Type = Enum.Parse<CargoOption>(item.crtype), Weight = item.crweight, Emaunt = item.cremaunt }
                        select new SpaceMission
                        {
                            Name = item.name,
                            Status = Enum.Parse<StatusOption>(item.status),
                            Cost = item.cost,
                            Altitude = item.altitude,
                            StartDate = item.startDate,
                            EndDate = item.endDate,
                            Rocket = rocket,
                            Cosmodrome = cosmodrome,
                            Cargoes = new List<Cargo> { cargo }
                        }).ToList();
            }
        }

        public List<Rocket> Get2(int mass, string eqName, string eqProducer)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var itemList = db.Query(
                    "SELECT ro.*, eq.name as eqname, eq.producer as eqproducer, eq.cost as eqcost " +
                    "FROM Rocket as ro " +
                    "INNER JOIN Equipment as eq ON eq.rocketName = ro.name " +
                    "AND eq.rocketVersion = ro.version " +
                    "WHERE eq.name = @Name AND eq.producer = @Producer AND (ro.massToGTO > @Mass OR ro.massToLEO > @Mass)",
                    new { Mass = mass, Name = eqName, Producer = eqProducer });

                return (from item in itemList
                    let equipment = new Equipment()
                    {
                        Name = item.eqname,
                        Producer = item.eqproducer,
                        Cost = item.eqcost
                    }
                    select new Rocket
                    {
                        Name = item.name,
                        Version = item.version,
                        Weight = item.weight,
                        Height = item.height,
                        Diameter = item.diameter,
                        Cost = item.cost,
                        Stages = item.stages,
                        MassToLEO = item.massToLEO,
                        MassToGTO = item.massToGTO,
                        EngineType = item.engineType,
                        Equipment = new List<Equipment> { equipment }
                    }).ToList();
            }
        }

        public List<Cosmodrome> Get3(string name, string status)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var itemList = db.Query(
                    "SELECT sm.*, co.name as coname, co.timezone as cotimezone, " +
                    "lo.latitude as lolatitude, lo.longitude as lolongitude, " +
                    "lo.country as locountry, lo.city as locity " +
                    "FROM SpaceMission as sm " +
                    "INNER JOIN Cosmodrome as co ON sm.cosmodromeName = co.name " +
                    "LEFT JOIN Location as lo ON co.latitude = lo.latitude " +
                    "AND co.longitude = lo.longitude " +
                    "WHERE sm.name = @Name AND sm.status = @Status",
                    new {Name = name, Status = Enum.Parse<StatusOption>(status)});

                return (from item in itemList
                    let spaceMission = new SpaceMission()
                    {
                        Name = item.name,
                        Status = Enum.Parse<StatusOption>(item.status),
                        Cost = item.cost,
                        Altitude = item.altitude,
                        StartDate = item.startDate,
                        EndDate = item.endDate
                    }
                    let location = new Location
                    {
                        City = item.locity, Country = item.locountry,
                        Latitude = item.lolatitude, Longitude = item.lolongitude
                    }
                    select new Cosmodrome
                    {
                        Name = item.name,
                        Timezone = item.cotimezone,
                        Location = location,
                        SpaceMissions = new List<SpaceMission> {spaceMission}
                    }).ToList();
            }
        }

        public List<Employee> Get4(string name, string coach, string area)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var itemList = db.Query(
                    "SELECT em.*, tp.name as tpname, tp.area as tparea, " +
                    "tp.coach as tpcoach, tp.cost as tpcost, tp.duration as tpduration " +
                    "FROM Employee as em " +
                    "INNER JOIN TrainingProgram as tp ON em.nameTrainingProgram = tp.name " +
                    "AND em.coachTrainingProgram = tp.coach " +
                    "WHERE tp.name = @Name AND tp.coach = @Coach AND tp.area = @Area",
                    new { Name = name, Coach = coach, Area = area });

                return (from item in itemList
                    let trainingProgram = new TrainingProgram()
                    {
                        Name = item.tpname,
                        Area = item.tparea,
                        Coach = item.tpcoach,
                        Cost = item.tpcost,
                        Duration = item.tpduration
                    }
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

        public List<TrainingProgram> Get5(string area, string duration)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                return db.Query<TrainingProgram>(
                    "SELECT * FROM TrainingProgram WHERE area = @Area AND duration = @Duration",
                    new { Area = area, Duration = duration }).ToList();
            }
        }

        public List<Cargo> Get6(string name, string status)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var itemList = db.Query(
                    "SELECT ca.*, sm.name as smname, sm.status as smstatus, " +
                    "sm.cost as smcost, sm.altitude as smaltitude, sm.startDate as smstartDate, " +
                    "sm.endDate as smendDate, cu.name as cuname, cu.country as cucountry, cu.totalWorth as cutotalWorth " +
                    "FROM Cargo as ca " +
                    "INNER JOIN SpaceMission as sm ON ca.spaceMissionName = sm.name " +
                    "INNER JOIN Customer as cu ON cu.name = ca.customerName " +
                    "AND cu.country = ca.customerCountry " +
                    "WHERE sm.name = @Name AND sm.status = @Status",
                    new { Name = name, Status = Enum.Parse<StatusOption>(status) });

                return (from item in itemList
                    let customer = new Customer() { Name = item.cuname, Country = item.cucountry, TotalWorth = item.cutotalWorth }
                    let spaceMission = new SpaceMission()
                    {
                        Name = item.smname,
                        Status = Enum.Parse<StatusOption>(item.smstatus),
                        Cost = item.smcost,
                        Altitude = item.smaltitude,
                        StartDate = item.smstartDate,
                        EndDate = item.smendDate
                    }
                        
                    select new Cargo
                    {
                        Name = item.name,
                        Type = Enum.Parse<CargoOption>(item.type),
                        Weight = item.weight,
                        Emaunt = item.emaunt,
                        Customer = customer,
                        SpaceMission = spaceMission
                    }).ToList();
            }
        }

        public List<Equipment> Get7(string name, string version)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var itemList = db.Query(
                    "SELECT ro.*, eq.name as eqname, eq.producer as eqproducer, eq.cost as eqcost " +
                    "FROM Equipment as eq " +
                    "INNER JOIN Rocket as ro ON eq.rocketName = ro.name " +
                    "AND eq.rocketVersion = ro.version " +
                    "WHERE ro.name = @Name AND ro.version = @Version",
                    new { Name = name, Version = version });

                return (from item in itemList
                        let rocket = new Rocket()
                        {
                            Name = item.name,
                            Version = item.version,
                            Weight = item.weight,
                            Height = item.height,
                            Diameter = item.diameter,
                            Cost = item.cost,
                            Stages = item.stages,
                            MassToLEO = item.massToLEO,
                            MassToGTO = item.massToGTO,
                            EngineType = item.engineType
                        }
                        select new Equipment
                        {
                            Name = item.eqname,
                            Producer = item.eqproducer,
                            Cost = item.eqcost,
                            Rocket = rocket
                        }).ToList();
            }
        }

        public List<Resources> Get8(string name, string country, string status)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var itemList = db.Query(
                    "SELECT re.*, cr.name as crname, cr.type as crtype, cr.weight as crweight, cr.weight as crweight, " +
                    "cr.emaunt as cremaunt, cr.spaceMissionName, cr.customerName, cr.customerCountry, " +
                    "cu.name as cuname, cu.country as cucountry, cu.totalWorth as cutotalWorth, " +
                    "sm.name as smname, sm.status as smstatus, sm.cost as smcost, sm.altitude as smaltitude, " +
                    "sm.startDate as smstartDate, sm.endDate as smendDate " +
                    "FROM Resources as re " +
                    "INNER JOIN SpaceMission as sm ON sm.name = re.spaceMissionName " +
                    "INNER JOIN Cargo as cr ON sm.name = cr.spaceMissionName " +
                    "INNER JOIN Customer as cu ON cr.customerName = cu.name " +
                    "AND cr.customerCountry = cu.country " +
                    "WHERE cu.name = @Name AND cu.country = @Country AND sm.status = @Status",
                    new { Name = name, Country = country, Status = Enum.Parse<StatusOption>(status) });

                return (from item in itemList
                        let customer = new Customer() { Name = item.cuname, Country = item.cucountry, TotalWorth = item.cutotalWorth }
                        let cargo = new Cargo 
                        { 
                            Name = item.crname, 
                            Type = Enum.Parse<CargoOption>(item.crtype), 
                            Weight = item.crweight, 
                            Emaunt = item.cremaunt, 
                            Customer = customer 
                        }
                        
                        let spaceMission = new SpaceMission()
                        {
                            Name = item.smname,
                            Status = Enum.Parse<StatusOption>(item.smstatus),
                            Cost = item.smcost,
                            Altitude = item.smaltitude,
                            StartDate = item.smstartDate,
                            EndDate = item.smendDate,
                            Cargoes = new List<Cargo> { cargo }
                        }
                        select new Resources
                        {
                            Name = item.name,
                            Type = Enum.Parse<ResourceOption>(item.type),
                            Emaunt = item.emaunt,
                            Cost = item.cost,
                            SpaceMission = spaceMission
                        }).ToList();
            }
        }

        public List<Customer> Get9(string date, string status)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var itemList = db.Query(
                    "SELECT cu.*, cr.name as crname, cr.type as crtype, cr.weight as crweight, cr.weight as crweight, " +
                    "cr.emaunt as cremaunt, cr.spaceMissionName, cr.customerName, cr.customerCountry, " +
                    "sm.name as smname, sm.status as smstatus, sm.cost as smcost, sm.altitude as smaltitude, " +
                    "sm.startDate as smstartDate, sm.endDate as smendDate " +
                    "FROM Customer as cu " +
                    "INNER JOIN Cargo as cr ON cu.name = cr.customerName " +
                    "AND cu.country = cr.customerCountry " +
                    "INNER JOIN SpaceMission as sm ON sm.name = cr.spaceMissionName " +
                    "WHERE sm.startDate = @Date AND sm.status = @Status",
                    new { Date = date, Status = Enum.Parse<StatusOption>(status) });

                return (from item in itemList
                        let spaceMission = new SpaceMission()
                        {
                            Name = item.smname,
                            Status = Enum.Parse<StatusOption>(item.smstatus),
                            Cost = item.smcost,
                            Altitude = item.smaltitude,
                            StartDate = item.smstartDate,
                            EndDate = item.smendDate
                        }
                        let cargo = new Cargo
                        {
                            Name = item.crname,
                            Type = Enum.Parse<CargoOption>(item.crtype),
                            Weight = item.crweight,
                            Emaunt = item.cremaunt,
                            SpaceMission = spaceMission
                        }
                        select new Customer
                        {
                            Name = item.name,
                            Country = item.country,
                            TotalWorth = item.totalWorth,
                            Cargos = new List<Cargo> { cargo }
                        }).ToList();
            }
        }

        public List<Purchase> Get10(string name)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var itemList = db.Query(
                    "SELECT pu.*, sm.name as smname, sm.status as smstatus, sm.cost as smcost, sm.altitude as smaltitude, " +
                    "sm.startDate as smstartDate, sm.endDate as smendDate " +
                    "FROM Purchase as pu " +
                    "INNER JOIN SpaceMission as sm ON sm.name = pu.spaceMissionName " +
                    "WHERE sm.name = @Name",
                    new { Name = name });

                return (from item in itemList
                        let spaceMission = new SpaceMission()
                        {
                            Name = item.smname,
                            Status = Enum.Parse<StatusOption>(item.smstatus),
                            Cost = item.smcost,
                            Altitude = item.smaltitude,
                            StartDate = item.smstartDate,
                            EndDate = item.smendDate
                        }
                        select new Purchase
                        {
                            Name = item.name,
                            Cost = item.cost,
                            SpaceMission = spaceMission
                        }).ToList();
            }
        }
    }
}
