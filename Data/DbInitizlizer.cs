using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
//using VehicleLeasingApp.Data; // <-- Replace with your actual namespace
using VehicleLeasingApplication.Models;

public static class DbInitializer
{
    public static void Seed(IServiceProvider serviceProvider)
    {
        using var context = new ApplicationDbContext(
            serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>());

        // Skip if DB already has data
        if (context.Vehicles.Any())
            return;

        // Add suppliers
        var suppliers = new[]
        {
            new Supplier { Name = "Toyota Dealer", Contact = "011-1234567", Location = "Sandton" },
            new Supplier { Name = "Ford SA", Contact = "011-7654321", Location = "Midrand" }
        };
        context.Suppliers.AddRange(suppliers);
        context.SaveChanges();

        // Add branches
        var branches = new[]
        {
            new Branch { Name = "Johannesburg Central", Location = "JHB" },
            new Branch { Name = "Pretoria East", Location = "PTA" }
        };
        context.Branches.AddRange(branches);
        context.SaveChanges();

        // Add clients
        var clients = new[]
        {
            new Client { Name = "ABC Mining", Email = "info@abcmining.co.za", Contact = "0821234567" },
            new Client { Name = "XYZ Logistics", Email = "contact@xyzlogistics.co.za", Contact = "0837654321" }
        };
        context.Clients.AddRange(clients);
        context.SaveChanges();

        // Add drivers
        var drivers = new[]
        {
            new Driver { FullName = "James Ndlovu", LicenseNumber = "JN123456", Contact = "0721112222" },
            new Driver { FullName = "Thando Mokoena", LicenseNumber = "TM654321", Contact = "0713334444" }
        };
        context.Drivers.AddRange(drivers);
        context.SaveChanges();

        // Add vehicles
        var vehicles = new[]
        {
            new Vehicle {
                Make = "Toyota", Model = "Hilux", Year = 2021, RegistrationNumber = "JHB123GP",
                SupplierID = suppliers[0].SupplierID, BranchID = branches[0].BranchID,
                ClientID = clients[0].ClientID, DriverID = drivers[0].DriverID
            },
            new Vehicle {
                Make = "Ford", Model = "Ranger", Year = 2022, RegistrationNumber = "PTA456GP",
                SupplierID = suppliers[1].SupplierID, BranchID = branches[1].BranchID,
                ClientID = clients[1].ClientID, DriverID = drivers[1].DriverID
            }
        };
        context.Vehicles.AddRange(vehicles);
        context.SaveChanges();
    }
}

