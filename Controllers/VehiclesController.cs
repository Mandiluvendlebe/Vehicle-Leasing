using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VehicleLeasingApplication.Models;

namespace VehicleLeasingApplication.Controllers
{
    public class VehiclesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public VehiclesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Vehicles
        public async Task<IActionResult> Index(string searchString, int? supplierId, int? branchId, int? clientId, int? driverId)
        {
            var vehiclesQuery = _context.Vehicles
                .Include(v => v.Supplier)
                .Include(v => v.Branch)
                .Include(v => v.Client)
                .Include(v => v.Driver)
                .AsQueryable();

            // Filtering logic
            if (!string.IsNullOrEmpty(searchString))
            {
                vehiclesQuery = vehiclesQuery.Where(v =>
                    v.Make.Contains(searchString) ||
                    v.Model.Contains(searchString) ||
                    v.RegistrationNumber.Contains(searchString));
            }

            if (supplierId.HasValue)
                vehiclesQuery = vehiclesQuery.Where(v => v.SupplierID == supplierId.Value);

            if (branchId.HasValue)
                vehiclesQuery = vehiclesQuery.Where(v => v.BranchID == branchId.Value);

            if (clientId.HasValue)
                vehiclesQuery = vehiclesQuery.Where(v => v.ClientID == clientId.Value);

            if (driverId.HasValue)
                vehiclesQuery = vehiclesQuery.Where(v => v.DriverID == driverId.Value);

            // Load dropdown data
            ViewBag.Suppliers = new SelectList(await _context.Suppliers.ToListAsync(), "SupplierID", "Name", supplierId);
            ViewBag.Branches = new SelectList(await _context.Branches.ToListAsync(), "BranchID", "Name", branchId);
            ViewBag.Clients = new SelectList(await _context.Clients.ToListAsync(), "ClientID", "Name", clientId);
            ViewBag.Drivers = new SelectList(await _context.Drivers.ToListAsync(), "DriverID", "FullName", driverId);

            return View(await vehiclesQuery.ToListAsync());
        }


        // GET: Vehicles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicle = await _context.Vehicles
                .Include(v => v.Branch)
                .Include(v => v.Client)
                .Include(v => v.Driver)
                .Include(v => v.Supplier)
                .FirstOrDefaultAsync(m => m.VehicleID == id);
            if (vehicle == null)
            {
                return NotFound();
            }

            return View(vehicle);
        }

        // GET: Vehicles/Create
        public IActionResult Create()
        {
            ViewData["BranchID"] = new SelectList(_context.Branches, "BranchID", "Name");
            ViewData["ClientID"] = new SelectList(_context.Clients, "ClientID", "Name");
            ViewData["DriverID"] = new SelectList(_context.Drivers, "DriverID", "FullName"); // or appropriate property
            ViewData["SupplierID"] = new SelectList(_context.Suppliers, "SupplierID", "Name");
            return View();
        }


        // POST: Vehicles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("VehicleID,Make,Model,Year,RegistrationNumber,SupplierID,BranchID,ClientID,DriverID")] Vehicle vehicle)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Add(vehicle);
                    await _context.SaveChangesAsync();

                    TempData["ModalMessage"] = "Vehicle created successfully!";
                    TempData["ModalType"] = "success";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    TempData["ModalMessage"] = $"Error saving vehicle: {ex.Message}";
                    TempData["ModalType"] = "error";
                }
            }
            else
            {
                TempData["ModalMessage"] = "Failed to create vehicle. Please correct the form.";
                TempData["ModalType"] = "error";
            }

            ViewData["BranchID"] = new SelectList(_context.Branches, "BranchID", "Name", vehicle.BranchID);
            ViewData["ClientID"] = new SelectList(_context.Clients, "ClientID", "Name", vehicle.ClientID);
            ViewData["DriverID"] = new SelectList(_context.Drivers, "DriverID", "FullName", vehicle.DriverID);
            ViewData["SupplierID"] = new SelectList(_context.Suppliers, "SupplierID", "Name", vehicle.SupplierID);

            return View(vehicle);
        }



        // GET: Vehicles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicle = await _context.Vehicles.FindAsync(id);
            if (vehicle == null)
            {
                return NotFound();
            }
            ViewData["BranchID"] = new SelectList(_context.Branches, "BranchID", "BranchID", vehicle.BranchID);
            ViewData["ClientID"] = new SelectList(_context.Clients, "ClientID", "ClientID", vehicle.ClientID);
            ViewData["DriverID"] = new SelectList(_context.Drivers, "DriverID", "DriverID", vehicle.DriverID);
            ViewData["SupplierID"] = new SelectList(_context.Suppliers, "SupplierID", "SupplierID", vehicle.SupplierID);
            return View(vehicle);
        }

        // POST: Vehicles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("VehicleID,Make,Model,Year,RegistrationNumber,SupplierID,BranchID,ClientID,DriverID")] Vehicle vehicle)
        {
            if (id != vehicle.VehicleID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vehicle);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VehicleExists(vehicle.VehicleID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["BranchID"] = new SelectList(_context.Branches, "BranchID", "BranchID", vehicle.BranchID);
            ViewData["ClientID"] = new SelectList(_context.Clients, "ClientID", "ClientID", vehicle.ClientID);
            ViewData["DriverID"] = new SelectList(_context.Drivers, "DriverID", "DriverID", vehicle.DriverID);
            ViewData["SupplierID"] = new SelectList(_context.Suppliers, "SupplierID", "SupplierID", vehicle.SupplierID);
            return View(vehicle);
        }

        // GET: Vehicles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicle = await _context.Vehicles
                .Include(v => v.Branch)
                .Include(v => v.Client)
                .Include(v => v.Driver)
                .Include(v => v.Supplier)
                .FirstOrDefaultAsync(m => m.VehicleID == id);
            if (vehicle == null)
            {
                return NotFound();
            }

            return View(vehicle);
        }

        // POST: Vehicles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var vehicle = await _context.Vehicles.FindAsync(id);
            if (vehicle != null)
            {
                _context.Vehicles.Remove(vehicle);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VehicleExists(int id)
        {
            return _context.Vehicles.Any(e => e.VehicleID == id);
        }
    }
}
