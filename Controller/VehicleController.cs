using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using pocket_service.Services.Interfaces;
using pocket_service.DTOs.Vehicle;
using pocket_service.Models;
using System.Security.Claims;

namespace pocket_service.Controller
{
    [Authorize]
    [ApiController]
    [Route("api/vehicle")]    
    public class VehicleController: ControllerBase
    {
        private readonly IVehicleService _vehicle;
        public VehicleController(IVehicleService vehicle)
        {
            _vehicle = vehicle;
        }
        
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] VehicleDto req)
        {
            try
            {
                var getUser = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var userRole = User.FindFirst(ClaimTypes.Role)?.Value;
                if(userRole != "User") return Unauthorized("Unable to add the vehicle");

                var vehicle = new Vehicle
                {
                    VehicleType = req.VehicleType,
                    Make = req.Make,
                    Model = req.Model,
                    Year = req.Year,
                    Vin = req.Vin,
                    LicensePlate = req.LicensePlate
                };
                await _vehicle.RegisterVehicle(Guid.Parse(getUser), vehicle);
                return Ok(vehicle);
            }catch(Exception ex)
            {
                return Conflict(new {message = ex.Message, ex});
            }
        }
    }
}