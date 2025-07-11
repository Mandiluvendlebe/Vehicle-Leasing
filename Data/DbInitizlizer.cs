//using Bogus;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.DependencyInjection;
//using System;
//using System.Linq;
////using VehicleLeasingApp.Data; // <-- Replace with your actual namespace
//using VehicleLeasingApplication.Models;

//public static class DbInitializer
//{
//    public static void Seed(IServiceProvider serviceProvider)
//    {
//        using var context = new ApplicationDbContext(
//            serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>());

//        var random = new Random();

//        // Gauteng suburbs/cities
//        var gautengCities = new[]
//        {
//            "Johannesburg", "Midrand", "Sandton", "Soweto", "Pretoria", "Centurion",
//            "Randburg", "Tembisa", "Kempton Park", "Benoni", "Roodepoort",
//            "Alberton", "Boksburg", "Germiston", "Hatfield", "Fourways",
//            "Bryanston", "Bedfordview", "Morningside", "Diepsloot"
//        };

//        // ---------- SUPPLIERS ----------
//        // Clear old suppliers (optional, only for dev/testing)
//        //context.Suppliers.RemoveRange(context.Suppliers);
//        //context.SaveChanges();

//        int suppliersToAdd = 20 - context.Suppliers.Count();
//        if (suppliersToAdd > 0)
//        {
//            var carSuppliers = new[] {
//                "Toyota", "Ford", "Volkswagen", "Hyundai", "Kia", "Nissan", "Mazda", "BMW",
//                "Mercedes-Benz", "Audi", "Isuzu", "Renault", "Chevrolet", "Suzuki", "Peugeot"
//            };

//            var supplierFaker = new Faker<Supplier>()
//                .RuleFor(s => s.Name, f => $"{f.PickRandom(carSuppliers)}")
//                .RuleFor(s => s.Contact, f => f.Phone.PhoneNumber("011-#######"))
//                .RuleFor(s => s.Location, f => f.PickRandom(gautengCities));

//            var suppliers = supplierFaker.Generate(suppliersToAdd);
//            context.Suppliers.AddRange(suppliers);
//            context.SaveChanges();
//        }

//        // ---------- BRANCHES ----------
//        int branchesToAdd = 20 - context.Branches.Count();
//        if (branchesToAdd > 0)
//        {
//            var branchFaker = new Faker<Branch>()
//                .RuleFor(b => b.Name, f => $"{f.PickRandom(gautengCities)} ")
//                .RuleFor(b => b.Location, f => f.PickRandom(gautengCities));

//            var branches = branchFaker.Generate(branchesToAdd);
//            context.Branches.AddRange(branches);
//            context.SaveChanges();
//        }

//        // ---------- CLIENTS ----------
//        int clientsToAdd = 20 - context.Clients.Count();
//        if (clientsToAdd > 0)
//        {
//            var industries = new[]
//            {
//                "Mining", "Construction", "Logistics", "Telecoms", "Security",
//                "Healthcare", "IT", "Retail", "Transport", "Energy"
//            };

//            var clientFaker = new Faker<Client>()
//                .RuleFor(c => c.Name, f => $"{f.Company.CompanyName()} {f.PickRandom(industries)}")
//                .RuleFor(c => c.Email, (f, c) => f.Internet.Email(provider: c.Name.Replace(" ", "").ToLower()))
//                .RuleFor(c => c.Contact, f => f.Phone.PhoneNumber("082#######"));

//            var clients = clientFaker.Generate(clientsToAdd);
//            context.Clients.AddRange(clients);
//            context.SaveChanges();
//        }

//        // ---------- DRIVERS ----------
//        int driversToAdd = 20 - context.Drivers.Count();
//        if (driversToAdd > 0)
//        {
//            var driverFaker = new Faker<Driver>("en_ZA")
//                .RuleFor(d => d.FullName, f => f.Name.FullName())
//                .RuleFor(d => d.LicenseNumber, f => $"SA{f.Random.Number(100000, 999999)}")
//                .RuleFor(d => d.Contact, f => f.Phone.PhoneNumber("071#######"));

//            var drivers = driverFaker.Generate(driversToAdd);
//            context.Drivers.AddRange(drivers);
//            context.SaveChanges();
//        }

//        // ---------- VEHICLES ----------
//        int existingVehicleCount = context.Vehicles.Count();
//        int vehiclesToAdd = 50 - existingVehicleCount;

//        if (vehiclesToAdd > 0)
//        {
//            var supplierIDs = context.Suppliers.Select(s => s.SupplierID).ToList();
//            var branchIDs = context.Branches.Select(b => b.BranchID).ToList();
//            var clientIDs = context.Clients.Select(c => c.ClientID).ToList();
//            var driverIDs = context.Drivers.Select(d => d.DriverID).ToList();

//            var saSuffixes = new[] { "GP", "NW", "MP", "FS", "L", "EC" };

//            var vehicleFaker = new Faker<Vehicle>()
//                .RuleFor(v => v.Make, f => f.Vehicle.Manufacturer())
//                .RuleFor(v => v.Model, f => f.Vehicle.Model())
//                .RuleFor(v => v.Year, f => f.Date.Past(10).Year)
//                .RuleFor(v => v.RegistrationNumber, f => $"{f.Random.String2(3, "ABCDEFGHIJKLMNOPQRSTUVWXYZ")}-{f.Random.Number(1000, 9999)}-{f.PickRandom(saSuffixes)}")
//                .RuleFor(v => v.SupplierID, f => f.PickRandom(supplierIDs))
//                .RuleFor(v => v.BranchID, f => f.PickRandom(branchIDs))
//                .RuleFor(v => v.ClientID, f => f.PickRandom(clientIDs))
//                .RuleFor(v => v.DriverID, f => f.PickRandom(driverIDs));

//            var vehicles = vehicleFaker.Generate(vehiclesToAdd);
//            context.Vehicles.AddRange(vehicles);
//            context.SaveChanges();
//        }
//    }

//}

