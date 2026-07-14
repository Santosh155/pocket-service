using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using pocket_service.Services.Interfaces;
using pocket_service.DTOs.Vehicle;
using pocket_service.Models;
using System.Security.Claims;
using System.Data;

namespace pocket_service.Controller
{
    [Authorize]
    [ApiController]
    [Route("api/vehicle")]    
    public class VehicleController: ControllerBase
    {
        private readonly IVehicleService _vehicle;
        private readonly IAddressService _address;
        public VehicleController(IVehicleService vehicle, IAddressService address)
        {
            _vehicle = vehicle;
            _address = address;
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

        [HttpPost("request-service")]
        public async Task<IActionResult> RequestService([FromBody] RegisterVehicleDto carS)
        {
            try
            {
                var getUser = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var verifyVehicle = await _vehicle.GetVehicleById(Guid.Parse(getUser)) 
                    ?? throw new Exception("You don't have permission, please try again with different user account");
                if(!verifyVehicle.Any(v=> v.Id == carS.VehicleId))
                    return NotFound(new{message = "Vehicle not found or You don't have permission"});
                var carAddress = new Address
                {
                    HouseNumber = carS.HouseNumber,
                    StreetName = carS.StreetName,
                    PostCode = carS.PostCode,
                    Suburb = carS.Suburb,
                    State = carS.State,
                    Longitude = carS.Longitude,
                    Latitude = carS.Latitude
                };
                var registerAddress = await _address.CreateAddressAsync(carAddress);
                var carService = new CarService
                {
                    VehicleId = carS.VehicleId,
                    UserId = Guid.Parse(getUser),
                    AddressId = registerAddress.Id,
                    ServiceType = carS.ServiceType,
                    ServiceDescription = carS.ServiceDescription,
                    ServiceDate = DateTime.SpecifyKind(carS.ServiceDate, DateTimeKind.Utc)
                };
                var result = await _vehicle.RequestVehicleService(carService);
                return Ok(result);
            }catch(Exception ex)
            {
                return Conflict(new {message = ex.Message});
            }
        }
    }
}