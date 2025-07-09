using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class ReportsController : Controller
{
    private readonly ApplicationDbContext _context;

    public ReportsController(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var supplierStats = await _context.Vehicles
            .GroupBy(v => v.Supplier.Name)
            .Select(g => new { Supplier = g.Key, Count = g.Count() })
            .ToListAsync();

        var branchStats = await _context.Vehicles
            .GroupBy(v => v.Branch.Name)
            .Select(g => new { Branch = g.Key, Count = g.Count() })
            .ToListAsync();

        var clientStats = await _context.Vehicles
            .GroupBy(v => v.Client.Name)
            .Select(g => new { Client = g.Key, Count = g.Count() })
            .ToListAsync();

        var manufacturerStats = await _context.Vehicles
            .GroupBy(v => v.Make)
            .Select(g => new { Make = g.Key, Count = g.Count() })
            .ToListAsync();

        ViewBag.SupplierStats = supplierStats;
        ViewBag.BranchStats = branchStats;
        ViewBag.ClientStats = clientStats;
        ViewBag.ManufacturerStats = manufacturerStats;

        return View();
    }
}

